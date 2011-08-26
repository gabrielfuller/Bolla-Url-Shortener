using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace Bolla.Models
{
    public class MyBolla
    {
        #region Settings
        /// <summary>
        /// This is the default redirection if someone comes to the site but does not enter a short code
        /// </summary>
        public static string DefaultRedirect
        {
            get
            {
                return ConfigurationManager.AppSettings["DefaultRedirect"];
            }
        }

        /// <summary>
        /// The secret key which allows someone to Create a New Link in the database
        /// </summary>
        public static string SecretKey
        {
            get
            {
                return ConfigurationManager.AppSettings["SecretKey"];
            }
        }

        /// <summary>
        /// The Message displayed when the entered SecretKey does not match the SecretKey in web.config
        /// </summary>
        public static string IncorrectKeyMessage
        {
            get
            {
                return ConfigurationManager.AppSettings["IncorrectKeyMessage"];
            }
        }

        /// <summary>
        /// A generic error message
        /// </summary>
        public static string GenericError
        {
            get
            {
                return ConfigurationManager.AppSettings["GenericError"];
            }
        }

        /// <summary>
        /// How many times to try and create a shortcode, as well as trips 
        /// to the database to check for validation before failing
        /// </summary>
        public static int ShortCodeTriesBeforeFail
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["ShortCodeTriesBeforeFail"].ToString());
            }
        }

        /// <summary>
        /// The default shortcode length
        /// </summary>
        public static int ShortCodeLength
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["ShortCodeLength"].ToString());
            }
        }

        /// <summary>
        /// This item protects you against brute force attacks.  
        /// Everytime someone tries to create a link with the 
        /// wrong SecretKey, the system makes them wait [FailWaitExtenderLength] to the 
        /// power of [NumberOfFailedTriesSinceLastSuccess] seconds to try again
        /// </summary>
        public static double FailWaitExtenderLength
        {
            get
            {
                return Convert.ToDouble(ConfigurationManager.AppSettings["FailWaitExtenderLength"].ToString());
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Creates a link in the database based on the IPAddress of the requestor and the RedirectUrl
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="redirectUrl"></param>
        /// <returns></returns>
        public static string AddLink(string ipAddress, string redirectUrl)
        {
            try
            {
                DbContext dbContext = new DbContext();
                var newLink = new Link();
                newLink.Id = Guid.NewGuid();
                newLink.DateCreated = DateTime.Now;
                newLink.RedirectUrl = redirectUrl;
                newLink.ShortCode = CreateUnusedShortCode();
                newLink.VisitCount = 0;
                dbContext.Links.InsertOnSubmit(newLink);
                dbContext.SubmitChanges();
                return newLink.ShortCode;
            }
            catch
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Gets the ShortCode and tries to find a RedirectUrl based on that code
        /// </summary>
        /// <param name="shortCode"></param>
        /// <returns></returns>
        public static string GetRedirectUrlFromShortCode(string shortCode)
        {
            DbContext dbContext = new DbContext();
            var link = dbContext.Links.Where(l => l.ShortCode == shortCode).First();
            link.DateOfLastVisit = DateTime.Now;
            link.VisitCount++;
            dbContext.SubmitChanges();
            return link.RedirectUrl;
        }

        /// <summary>
        /// Logs an authentication error to the database to prevent future requests until the wait time has lapsed
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="lastAttemptDate"></param>
        /// <param name="SecondsUntilNextAttempt"></param>
        public static void LogAuthorizationError(string ipAddress, out DateTime lastAttemptDate, out long SecondsUntilNextAttempt)
        {
            Attempt a = null;
            DbContext dbContext = new DbContext();
            lastAttemptDate = DateTime.Now;

            var allAttempts = dbContext.Attempts.Where(i => i.IPAddress == ipAddress);

            if (allAttempts.Count() == 0)
            {
                a = new Attempt();
                a.DateOfLastAttempt = lastAttemptDate;
                a.IPAddress = ipAddress;
                a.NumberOfAttemptsSinceLastSuccess = 1;
                a.SecondsUntilNextAttempt = Convert.ToInt64(FailWaitExtenderLength);
                dbContext.Attempts.InsertOnSubmit(a);
            }
            else
            {
                a = allAttempts.First();
                a.DateOfLastAttempt = lastAttemptDate;
                a.NumberOfAttemptsSinceLastSuccess++;
                var power = Math.Pow(FailWaitExtenderLength, a.NumberOfAttemptsSinceLastSuccess);

                a.SecondsUntilNextAttempt = Convert.ToInt64(power);
            }
            dbContext.SubmitChanges();
            
            //return seconds until when they can try again
            SecondsUntilNextAttempt = a.SecondsUntilNextAttempt;
        }

        /// <summary>
        /// Gets the IP Address of the computer making the request
        /// </summary>
        /// <returns></returns>
        public static string ReqestorIPAddress()
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Tries to create an Unused Short Code by creating a Random AlphaNumeric 
        /// sequence based on the ShortCodeLenth within X number of tries given 
        /// by the ShortCodeTriesBeforeFail variable
        /// </summary>
        /// <returns></returns>
        private static string CreateUnusedShortCode()
        {
            DbContext dbContext = new DbContext();
            for (int x = 0; x < ShortCodeTriesBeforeFail; x++)
            {
                string newKey = CreateRandomAlphaNumericSequence(ShortCodeLength);
                if (dbContext.Links.Where(i => i.ShortCode == newKey).Count() == 0)
                    return newKey;
            }
            throw new Exception();
        }

        /// <summary>
        /// Creates a random sequence letters and numbers to be used for the Short Urls
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private static string CreateRandomAlphaNumericSequence(int length)
        {
            String _allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ1234567890";
            Byte[] randomBytes = new Byte[length];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);
            char[] chars = new char[length];
            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < length; i++)
            {
                chars[i] = _allowedChars[(int)randomBytes[i] % allowedCharCount];
            }
            return new string(chars);
        }
        #endregion
    }
}