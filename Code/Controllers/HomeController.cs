using System;
using System.Web.Mvc;
using Bolla.Models;

namespace Bolla.Controllers
{
    /// <summary>
    /// To create a link, run this Website and Browse to 
    /// ~/CreateLink?secretKey=secretkey&redirecturl=http://www.yahoo.com
    /// 
    /// When given the shortcode, browse to ~/{ShortCode} to see it in action
    /// </summary>
    public class HomeController : Controller
    {
        private MyBolla bolla = new MyBolla();

        /// <summary>
        /// This action is called to process the Short Code that is sent to the server and perform the redirect
        /// </summary>
        /// <param name="shortCode"></param>
        /// <returns></returns>
        public ActionResult GetPage(string shortCode)
        {
            try
            {
                if (String.IsNullOrEmpty(shortCode))
                    return RedirectPermanent(MyBolla.DefaultRedirect);

                string RedirectUrl = MyBolla.GetRedirectUrlFromShortCode(shortCode);
                if (!String.IsNullOrEmpty(RedirectUrl))
                    return RedirectPermanent(RedirectUrl);
                throw new Exception();
            }
            catch
            {
                ViewBag.Message = String.Format("This is not a valid short code: {0}", shortCode);
                return View();
            }
        }

        /// <summary>
        /// This action is called to create a new link in the database from a Url.
        /// </summary>
        /// <param name="secretKey">This key must match the stored web.config key to process the request</param>
        /// <param name="RedirectUrl">This url must be url encoded</param>
        /// <returns></returns>
        public JsonResult CreateLink(string secretKey, string RedirectUrl)
        {
            DateTime LastAttemptDate = DateTime.Now;
            DateTime? nextAttempt = DateTime.Now;
            bool KeyIsCorrect = false, Success = false;
            long SecondsToNextAttempt = 0;
            string Message = "",
                IPAddress = MyBolla.ReqestorIPAddress(),
                ShortCode = "";

            if (secretKey == MyBolla.SecretKey)
            {
                KeyIsCorrect = true;
                try
                {
                    ShortCode = MyBolla.AddLink(IPAddress, RedirectUrl);
                    Success = true;
                }
                catch
                {
                    Message = MyBolla.GenericError;
                }
            }
            else
            {
                Success = false;
                KeyIsCorrect = false;
                
                try
                {
                    MyBolla.LogAuthorizationError(IPAddress, out LastAttemptDate, out SecondsToNextAttempt);
                    var seconds = Convert.ToDouble(SecondsToNextAttempt);
                    nextAttempt = LastAttemptDate.AddSeconds(seconds);
                }
                catch
                {
                    Message = MyBolla.IncorrectKeyMessage;
                }
            }

            return 
                Json(new 
                {
                    Success = Success,
                    KeyIsCorrect = KeyIsCorrect,
                    ShortCode = ShortCode,
                    Message = Message,
                    LastAttemptDate = LastAttemptDate.ToString(),
                    NextPossibleAttemptDate = nextAttempt.HasValue == true? nextAttempt.Value.ToString() : ""
                }, 
                JsonRequestBehavior.AllowGet);
        }
    }
}
