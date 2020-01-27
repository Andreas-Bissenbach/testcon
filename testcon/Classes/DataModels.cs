using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testcon.Classes
{
    public class Data_Models
    {
        public class ContactsList
        {
            public List<Dictionary<string, object>> Contacts { get; set; }
        }

        public class ContactObjects
        {
            public List<GetContacts> ContactData { get; set; }
        }

        public class GetContacts
        {

            public bool Company_priority { get; set; }
            public bool Unsubscribed { get; set; }

            public bool Api_originated { get; set; }

            public long LatestSubscribe { get; set; }

            public string Contact_id { get; set; }
            public string Name { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string Email { get; set; }
            public string Created_at { get; set; }
            public string Updated_at { get; set; }
            public string Status { get; set; }
            public string LeadSource { get; set; }
            public string First_visit_at { get; set; }
            public string LastLocation { get; set; }
            public string LastEngagement { get; set; }
            public string MailingPostalCode { get; set; }
            public string Phone { get; set; }
            public string Company { get; set; }
            public ArrayList Custom_fields { get; set; }

        }

        public class ContactName
        {
            public string Name { get; set; }
        }

        public class ContactInformation
        {
            public object Email { get; set; }
        }

        public class ContactsResponse
        {
            public int Total_contacts { get; set; }
            public string Bookmark { get; set; }
            public List<ContactName> Contacts { get; set; }
        }

        public class ContactData
        {
            public List<Dictionary<string, object>> Contacts { get; set; }
            public object Bookmark { get; set; }
        }

        public class ContactMails
        {
            public List<Dictionary<string,object>> Mail_received { get; set; }
            public List<Dictionary<string, object>> Mail_opened { get; set; }
            public List<Dictionary<string, object>> Mail_clicked { get; set; }
            public List<Dictionary<string, object>> Mail_bounced { get; set; }
            public List<Dictionary<string, object>> Mail_hardbounced { get; set; }
            public List<Dictionary<string, object>> Mail_unsubscribed { get; set; }
            public List<Dictionary<string, object>> Contacts { get; set; }
        }

        public class ContactID
        {
            public string Contact_id { get; set; }
        }
    }
}
