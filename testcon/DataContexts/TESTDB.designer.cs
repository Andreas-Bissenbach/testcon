﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace testcon.DataContexts
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="AutoPilot")]
	public partial class TESTDBDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertContact(Contact instance);
    partial void UpdateContact(Contact instance);
    partial void DeleteContact(Contact instance);
    #endregion
		
		public TESTDBDataContext() : 
				base(global::testcon.Properties.Settings.Default.AutoPilotConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public TESTDBDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TESTDBDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TESTDBDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TESTDBDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Contact> Contacts
		{
			get
			{
				return this.GetTable<Contact>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Contact")]
	public partial class Contact : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _User_Id;
		
		private System.Nullable<bool> _company_priority;
		
		private string _Name;
		
		private string _LastName;
		
		private string _FirstName;
		
		private string _Email;
		
		private string _MailingPostalCode;
		
		private System.Nullable<System.DateTime> _created_at;
		
		private System.Nullable<System.DateTime> _updated_at;
		
		private string _Status;
		
		private string _contact_id;
		
		private System.Nullable<System.DateTime> _First_visit_at;
		
		private string _LeadSource;
		
		private System.Nullable<bool> _Unsubscribed;
		
		private System.Nullable<long> _LatestSubscribe;
		
		private string _LastTimezone;
		
		private string _LastLocation;
		
		private System.Nullable<System.DateTime> _LastEngagement;
		
		private string _Phone;
		
		private string _Company;
		
		private System.Nullable<bool> _Api_originated;
		
		private string _Custom_fields;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnUser_IdChanging(int value);
    partial void OnUser_IdChanged();
    partial void Oncompany_priorityChanging(System.Nullable<bool> value);
    partial void Oncompany_priorityChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnLastNameChanging(string value);
    partial void OnLastNameChanged();
    partial void OnFirstNameChanging(string value);
    partial void OnFirstNameChanged();
    partial void OnEmailChanging(string value);
    partial void OnEmailChanged();
    partial void OnMailingPostalCodeChanging(string value);
    partial void OnMailingPostalCodeChanged();
    partial void Oncreated_atChanging(System.Nullable<System.DateTime> value);
    partial void Oncreated_atChanged();
    partial void Onupdated_atChanging(System.Nullable<System.DateTime> value);
    partial void Onupdated_atChanged();
    partial void OnStatusChanging(string value);
    partial void OnStatusChanged();
    partial void Oncontact_idChanging(string value);
    partial void Oncontact_idChanged();
    partial void OnFirst_visit_atChanging(System.Nullable<System.DateTime> value);
    partial void OnFirst_visit_atChanged();
    partial void OnLeadSourceChanging(string value);
    partial void OnLeadSourceChanged();
    partial void OnUnsubscribedChanging(System.Nullable<bool> value);
    partial void OnUnsubscribedChanged();
    partial void OnLatestSubscribeChanging(System.Nullable<long> value);
    partial void OnLatestSubscribeChanged();
    partial void OnLastTimezoneChanging(string value);
    partial void OnLastTimezoneChanged();
    partial void OnLastLocationChanging(string value);
    partial void OnLastLocationChanged();
    partial void OnLastEngagementChanging(System.Nullable<System.DateTime> value);
    partial void OnLastEngagementChanged();
    partial void OnPhoneChanging(string value);
    partial void OnPhoneChanged();
    partial void OnCompanyChanging(string value);
    partial void OnCompanyChanged();
    partial void OnApi_originatedChanging(System.Nullable<bool> value);
    partial void OnApi_originatedChanged();
    partial void OnCustom_fieldsChanging(string value);
    partial void OnCustom_fieldsChanged();
    #endregion
		
		public Contact()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_User_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int User_Id
		{
			get
			{
				return this._User_Id;
			}
			set
			{
				if ((this._User_Id != value))
				{
					this.OnUser_IdChanging(value);
					this.SendPropertyChanging();
					this._User_Id = value;
					this.SendPropertyChanged("User_Id");
					this.OnUser_IdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_company_priority", DbType="Bit")]
		public System.Nullable<bool> company_priority
		{
			get
			{
				return this._company_priority;
			}
			set
			{
				if ((this._company_priority != value))
				{
					this.Oncompany_priorityChanging(value);
					this.SendPropertyChanging();
					this._company_priority = value;
					this.SendPropertyChanged("company_priority");
					this.Oncompany_priorityChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(255)")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastName", DbType="VarChar(255)")]
		public string LastName
		{
			get
			{
				return this._LastName;
			}
			set
			{
				if ((this._LastName != value))
				{
					this.OnLastNameChanging(value);
					this.SendPropertyChanging();
					this._LastName = value;
					this.SendPropertyChanged("LastName");
					this.OnLastNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FirstName", DbType="VarChar(255)")]
		public string FirstName
		{
			get
			{
				return this._FirstName;
			}
			set
			{
				if ((this._FirstName != value))
				{
					this.OnFirstNameChanging(value);
					this.SendPropertyChanging();
					this._FirstName = value;
					this.SendPropertyChanged("FirstName");
					this.OnFirstNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Email", DbType="VarChar(255) NOT NULL", CanBeNull=false)]
		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				if ((this._Email != value))
				{
					this.OnEmailChanging(value);
					this.SendPropertyChanging();
					this._Email = value;
					this.SendPropertyChanged("Email");
					this.OnEmailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MailingPostalCode", DbType="VarChar(255)")]
		public string MailingPostalCode
		{
			get
			{
				return this._MailingPostalCode;
			}
			set
			{
				if ((this._MailingPostalCode != value))
				{
					this.OnMailingPostalCodeChanging(value);
					this.SendPropertyChanging();
					this._MailingPostalCode = value;
					this.SendPropertyChanged("MailingPostalCode");
					this.OnMailingPostalCodeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_created_at", DbType="DateTime2")]
		public System.Nullable<System.DateTime> created_at
		{
			get
			{
				return this._created_at;
			}
			set
			{
				if ((this._created_at != value))
				{
					this.Oncreated_atChanging(value);
					this.SendPropertyChanging();
					this._created_at = value;
					this.SendPropertyChanged("created_at");
					this.Oncreated_atChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_updated_at", DbType="DateTime2")]
		public System.Nullable<System.DateTime> updated_at
		{
			get
			{
				return this._updated_at;
			}
			set
			{
				if ((this._updated_at != value))
				{
					this.Onupdated_atChanging(value);
					this.SendPropertyChanging();
					this._updated_at = value;
					this.SendPropertyChanged("updated_at");
					this.Onupdated_atChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Status", DbType="VarChar(255)")]
		public string Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				if ((this._Status != value))
				{
					this.OnStatusChanging(value);
					this.SendPropertyChanging();
					this._Status = value;
					this.SendPropertyChanged("Status");
					this.OnStatusChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_contact_id", DbType="VarChar(255) NOT NULL", CanBeNull=false)]
		public string contact_id
		{
			get
			{
				return this._contact_id;
			}
			set
			{
				if ((this._contact_id != value))
				{
					this.Oncontact_idChanging(value);
					this.SendPropertyChanging();
					this._contact_id = value;
					this.SendPropertyChanged("contact_id");
					this.Oncontact_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_First_visit_at", DbType="DateTime2")]
		public System.Nullable<System.DateTime> First_visit_at
		{
			get
			{
				return this._First_visit_at;
			}
			set
			{
				if ((this._First_visit_at != value))
				{
					this.OnFirst_visit_atChanging(value);
					this.SendPropertyChanging();
					this._First_visit_at = value;
					this.SendPropertyChanged("First_visit_at");
					this.OnFirst_visit_atChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LeadSource", DbType="VarChar(255)")]
		public string LeadSource
		{
			get
			{
				return this._LeadSource;
			}
			set
			{
				if ((this._LeadSource != value))
				{
					this.OnLeadSourceChanging(value);
					this.SendPropertyChanging();
					this._LeadSource = value;
					this.SendPropertyChanged("LeadSource");
					this.OnLeadSourceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Unsubscribed", DbType="Bit")]
		public System.Nullable<bool> Unsubscribed
		{
			get
			{
				return this._Unsubscribed;
			}
			set
			{
				if ((this._Unsubscribed != value))
				{
					this.OnUnsubscribedChanging(value);
					this.SendPropertyChanging();
					this._Unsubscribed = value;
					this.SendPropertyChanged("Unsubscribed");
					this.OnUnsubscribedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LatestSubscribe", DbType="BigInt")]
		public System.Nullable<long> LatestSubscribe
		{
			get
			{
				return this._LatestSubscribe;
			}
			set
			{
				if ((this._LatestSubscribe != value))
				{
					this.OnLatestSubscribeChanging(value);
					this.SendPropertyChanging();
					this._LatestSubscribe = value;
					this.SendPropertyChanged("LatestSubscribe");
					this.OnLatestSubscribeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastTimezone", DbType="VarChar(255)")]
		public string LastTimezone
		{
			get
			{
				return this._LastTimezone;
			}
			set
			{
				if ((this._LastTimezone != value))
				{
					this.OnLastTimezoneChanging(value);
					this.SendPropertyChanging();
					this._LastTimezone = value;
					this.SendPropertyChanged("LastTimezone");
					this.OnLastTimezoneChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastLocation", DbType="VarChar(255)")]
		public string LastLocation
		{
			get
			{
				return this._LastLocation;
			}
			set
			{
				if ((this._LastLocation != value))
				{
					this.OnLastLocationChanging(value);
					this.SendPropertyChanging();
					this._LastLocation = value;
					this.SendPropertyChanged("LastLocation");
					this.OnLastLocationChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastEngagement", DbType="DateTime2")]
		public System.Nullable<System.DateTime> LastEngagement
		{
			get
			{
				return this._LastEngagement;
			}
			set
			{
				if ((this._LastEngagement != value))
				{
					this.OnLastEngagementChanging(value);
					this.SendPropertyChanging();
					this._LastEngagement = value;
					this.SendPropertyChanged("LastEngagement");
					this.OnLastEngagementChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Phone", DbType="VarChar(255)")]
		public string Phone
		{
			get
			{
				return this._Phone;
			}
			set
			{
				if ((this._Phone != value))
				{
					this.OnPhoneChanging(value);
					this.SendPropertyChanging();
					this._Phone = value;
					this.SendPropertyChanged("Phone");
					this.OnPhoneChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Company", DbType="VarChar(255)")]
		public string Company
		{
			get
			{
				return this._Company;
			}
			set
			{
				if ((this._Company != value))
				{
					this.OnCompanyChanging(value);
					this.SendPropertyChanging();
					this._Company = value;
					this.SendPropertyChanged("Company");
					this.OnCompanyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Api_originated", DbType="Bit")]
		public System.Nullable<bool> Api_originated
		{
			get
			{
				return this._Api_originated;
			}
			set
			{
				if ((this._Api_originated != value))
				{
					this.OnApi_originatedChanging(value);
					this.SendPropertyChanging();
					this._Api_originated = value;
					this.SendPropertyChanged("Api_originated");
					this.OnApi_originatedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Custom_fields", DbType="VarChar(MAX)")]
		public string Custom_fields
		{
			get
			{
				return this._Custom_fields;
			}
			set
			{
				if ((this._Custom_fields != value))
				{
					this.OnCustom_fieldsChanging(value);
					this.SendPropertyChanging();
					this._Custom_fields = value;
					this.SendPropertyChanged("Custom_fields");
					this.OnCustom_fieldsChanged();
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
