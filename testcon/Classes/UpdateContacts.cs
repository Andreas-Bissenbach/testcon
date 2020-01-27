using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testcon.DataContexts;
using static testcon.Classes.JsonData;
using static testcon.Classes.Controls;
using System.Threading;

namespace testcon.Classes
{
    public static class UpdateContacts
    {
        public static TESTDBDataContext dbtest = new TESTDBDataContext();

        public static bool Contacts(Dictionary<KiaFields, string> data)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var n3 in Enum.GetValues(typeof(KiaFields)).Cast<KiaFields>().ToDictionary(t => t, t => t.ToString()))
            {
                if (!data.ContainsKey(n3.Key))
                {
                    data[n3.Key] = null;
                }
            }

            bool ContactExists = (from x in dbtest.Contacts
                                  where x.Email == data[KiaFields.Email] || x.contact_id == data[KiaFields.contact_id]
                                  select x.Email).Any();

            if (!ContactExists)
            {
                try
                {
                    Contact contact = new Contact
                    {
                        Name = data[KiaFields.Name],
                        LastName = data[KiaFields.LastName],
                        FirstName = data[KiaFields.FirstName],
                        Email = data[KiaFields.Email],
                        created_at = Convert.ToDateTime(data[KiaFields.created_at]),
                        updated_at = Convert.ToDateTime(data[KiaFields.updated_at]),
                        Status = data[KiaFields.Status],
                        LeadSource = data[KiaFields.LeadSource],
                        First_visit_at = Convert.ToDateTime(data[KiaFields.first_visit_at]),
                        LastTimezone = data[KiaFields.lastTimezone],
                        LastLocation = data[KiaFields.lastLocation],
                        LastEngagement = Convert.ToDateTime(data[KiaFields.lastEngagement]),
                        MailingPostalCode = data[KiaFields.MailingPostalCode],
                        Phone = data[KiaFields.Phone],
                        Company = data[KiaFields.Company],
                        LatestSubscribe = Convert.ToInt64(data[KiaFields.latestSubscribe]),
                        Api_originated = Convert.ToBoolean(data[KiaFields.api_originated]),
                        Unsubscribed = Convert.ToBoolean(data[KiaFields.unsubscribed]),
                        company_priority = Convert.ToBoolean(data[KiaFields.company_priority]),
                        contact_id = data[KiaFields.contact_id]
                    };
                    dbtest.Contacts.InsertOnSubmit(contact);
                    dbtest.SubmitChanges();
                    sb.AppendLine("user: " + data[KiaFields.Name] + " was added to DB");
                }
                catch (SqlException)
                {
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        public static async Task UpdateTask(Dictionary<KiaFields, string> D, bool ContactExists)
        {
            if (!ContactExists)
            {
                return;
            }
            else
            {
                string Q = UpdateQuery(D, "Contact");
                await UT(Q, dbtest.Connection.ConnectionString);
                Console.Write(Q);
            }
        }

        public static string UpdateQuery(Dictionary<KiaFields, string> KiaDictionary, string table)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"UPDATE {table} SET ");
            foreach (var n in KiaDictionary)
            {
                string s = n.Value?.ToString();
                bool isContactID = (n.Key.ToString() == "contact_id");
                bool isEmail = (n.Key.ToString() == "Email");
                if (isContactID) ContactID = s;

                if (!string.IsNullOrEmpty(s) && !isContactID && !isEmail)
                {
                    string val = RuleSet(s);
                    
                    sb.AppendLine(n.Key.ToString() + " = "+ val + ", ");
     
                    AutoPilot.CountFields++;
                    ResultInfo.Show();
                }
            }
            
            sb.Remove(sb.ToString().Length - 4, 4);
            sb.AppendLine();
            sb.AppendLine($"where contact_id = '{ContactID}' ");
            return sb.ToString();
        }

        public static async Task UT(string Q, string connnection)
        {
            using SqlConnection cn = new SqlConnection(connnection);
            using SqlCommand cmd = new SqlCommand(Q, cn);
            await cn.OpenAsync();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
        }

        public static async Task InsertInto(string Q)
        {
            using SqlConnection cn = new SqlConnection(dbtest.Connection.ConnectionString);
            using SqlCommand cmd = new SqlCommand(Q, cn);
            await cn.OpenAsync();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
        }

        public static string RuleSet(string source)
        {
            return source switch
            {
                "False" => "0",
                "True" => "1",
                _ => $"'{RMQ(source)}'",
            };
        }

        public static string RMQ(string txt)
        {
            if (txt.Contains("'"))
            {
                string[] charsToRemove = new string[] { "'" };
                foreach (var c in charsToRemove)
                {
                    txt = txt.Replace(c, "'" + "'");
                }
            }
            return txt;
        }
    }
}
