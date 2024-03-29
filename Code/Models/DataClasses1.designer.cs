﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.237
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bolla.Models
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Database1")]
	public partial class DbContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertLink(Link instance);
    partial void UpdateLink(Link instance);
    partial void DeleteLink(Link instance);
    partial void InsertAttempt(Attempt instance);
    partial void UpdateAttempt(Attempt instance);
    partial void DeleteAttempt(Attempt instance);
    #endregion
		
		public DbContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["Database1ConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DbContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DbContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DbContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DbContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Link> Links
		{
			get
			{
				return this.GetTable<Link>();
			}
		}
		
		public System.Data.Linq.Table<Attempt> Attempts
		{
			get
			{
				return this.GetTable<Attempt>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Links")]
	public partial class Link : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _Id;
		
		private string _ShortCode;
		
		private System.DateTime _DateCreated;
		
		private string _RedirectUrl;
		
		private long _VisitCount;
		
		private System.Nullable<System.DateTime> _DateOfLastVisit;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(System.Guid value);
    partial void OnIdChanged();
    partial void OnShortCodeChanging(string value);
    partial void OnShortCodeChanged();
    partial void OnDateCreatedChanging(System.DateTime value);
    partial void OnDateCreatedChanged();
    partial void OnRedirectUrlChanging(string value);
    partial void OnRedirectUrlChanged();
    partial void OnVisitCountChanging(long value);
    partial void OnVisitCountChanged();
    partial void OnDateOfLastVisitChanging(System.Nullable<System.DateTime> value);
    partial void OnDateOfLastVisitChanged();
    #endregion
		
		public Link()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		public System.Guid Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ShortCode", DbType="VarChar(10) NOT NULL", CanBeNull=false)]
		public string ShortCode
		{
			get
			{
				return this._ShortCode;
			}
			set
			{
				if ((this._ShortCode != value))
				{
					this.OnShortCodeChanging(value);
					this.SendPropertyChanging();
					this._ShortCode = value;
					this.SendPropertyChanged("ShortCode");
					this.OnShortCodeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateCreated", DbType="DateTime NOT NULL")]
		public System.DateTime DateCreated
		{
			get
			{
				return this._DateCreated;
			}
			set
			{
				if ((this._DateCreated != value))
				{
					this.OnDateCreatedChanging(value);
					this.SendPropertyChanging();
					this._DateCreated = value;
					this.SendPropertyChanged("DateCreated");
					this.OnDateCreatedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RedirectUrl", DbType="NVarChar(2000) NOT NULL", CanBeNull=false)]
		public string RedirectUrl
		{
			get
			{
				return this._RedirectUrl;
			}
			set
			{
				if ((this._RedirectUrl != value))
				{
					this.OnRedirectUrlChanging(value);
					this.SendPropertyChanging();
					this._RedirectUrl = value;
					this.SendPropertyChanged("RedirectUrl");
					this.OnRedirectUrlChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VisitCount", DbType="BigInt NOT NULL")]
		public long VisitCount
		{
			get
			{
				return this._VisitCount;
			}
			set
			{
				if ((this._VisitCount != value))
				{
					this.OnVisitCountChanging(value);
					this.SendPropertyChanging();
					this._VisitCount = value;
					this.SendPropertyChanged("VisitCount");
					this.OnVisitCountChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateOfLastVisit", DbType="DateTime")]
		public System.Nullable<System.DateTime> DateOfLastVisit
		{
			get
			{
				return this._DateOfLastVisit;
			}
			set
			{
				if ((this._DateOfLastVisit != value))
				{
					this.OnDateOfLastVisitChanging(value);
					this.SendPropertyChanging();
					this._DateOfLastVisit = value;
					this.SendPropertyChanged("DateOfLastVisit");
					this.OnDateOfLastVisitChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Attempts")]
	public partial class Attempt : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _Id;
		
		private string _IPAddress;
		
		private System.DateTime _DateOfLastAttempt;
		
		private long _SecondsUntilNextAttempt;
		
		private long _NumberOfAttemptsSinceLastSuccess;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(long value);
    partial void OnIdChanged();
    partial void OnIPAddressChanging(string value);
    partial void OnIPAddressChanged();
    partial void OnDateOfLastAttemptChanging(System.DateTime value);
    partial void OnDateOfLastAttemptChanged();
    partial void OnSecondsUntilNextAttemptChanging(long value);
    partial void OnSecondsUntilNextAttemptChanged();
    partial void OnNumberOfAttemptsSinceLastSuccessChanging(long value);
    partial void OnNumberOfAttemptsSinceLastSuccessChanged();
    #endregion
		
		public Attempt()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public long Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IPAddress", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string IPAddress
		{
			get
			{
				return this._IPAddress;
			}
			set
			{
				if ((this._IPAddress != value))
				{
					this.OnIPAddressChanging(value);
					this.SendPropertyChanging();
					this._IPAddress = value;
					this.SendPropertyChanged("IPAddress");
					this.OnIPAddressChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateOfLastAttempt", DbType="DateTime NOT NULL")]
		public System.DateTime DateOfLastAttempt
		{
			get
			{
				return this._DateOfLastAttempt;
			}
			set
			{
				if ((this._DateOfLastAttempt != value))
				{
					this.OnDateOfLastAttemptChanging(value);
					this.SendPropertyChanging();
					this._DateOfLastAttempt = value;
					this.SendPropertyChanged("DateOfLastAttempt");
					this.OnDateOfLastAttemptChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SecondsUntilNextAttempt", DbType="BigInt NOT NULL")]
		public long SecondsUntilNextAttempt
		{
			get
			{
				return this._SecondsUntilNextAttempt;
			}
			set
			{
				if ((this._SecondsUntilNextAttempt != value))
				{
					this.OnSecondsUntilNextAttemptChanging(value);
					this.SendPropertyChanging();
					this._SecondsUntilNextAttempt = value;
					this.SendPropertyChanged("SecondsUntilNextAttempt");
					this.OnSecondsUntilNextAttemptChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NumberOfAttemptsSinceLastSuccess", DbType="BigInt NOT NULL")]
		public long NumberOfAttemptsSinceLastSuccess
		{
			get
			{
				return this._NumberOfAttemptsSinceLastSuccess;
			}
			set
			{
				if ((this._NumberOfAttemptsSinceLastSuccess != value))
				{
					this.OnNumberOfAttemptsSinceLastSuccessChanging(value);
					this.SendPropertyChanging();
					this._NumberOfAttemptsSinceLastSuccess = value;
					this.SendPropertyChanged("NumberOfAttemptsSinceLastSuccess");
					this.OnNumberOfAttemptsSinceLastSuccessChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
