using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testcon.Classes
{
    public class DataRetrieve
    {
        public enum Dataformat
        {
            Normal,
            Xml
        }


        public string AnyData(object obj, Dataformat format)
        {
            return format switch
            {
                Dataformat.Normal => NormalData(obj),
                Dataformat.Xml => XmlData(obj),
                _ => "",
            };
        }

        private string NormalData(object obj)
        {
            StringBuilder sb = new StringBuilder();

            if (obj.GetType() == typeof(List<Dictionary<string, object>>))
            {
                foreach (var n in obj as List<Dictionary<string, object>>)
                {
                    foreach (var n2 in n)
                    {
                        if (n2.Key.ToUpper().Equals("EMAIL"))
                        {
                            sb.AppendLine(Environment.NewLine);
                            sb.AppendLine(n2.Value.ToString());
                            sb.AppendLine(Environment.NewLine);
                        }

                        if (n.GetType() == typeof(Dictionary<string, object>))
                        {
                            foreach (var n3 in n)
                            {
                                if (n3.Value.GetType() == typeof(Dictionary<string, object>))
                                {
                                    foreach (var n4 in n3.Value as Dictionary<string, object>)
                                    {
                                        sb.AppendLine($"{n4.Key} : {n4.Value}");
                                    }
                                }
                            }
                        }
                        else if (n.GetType() == typeof(ArrayList))
                        {
                            foreach (var n3 in n)
                            {
                                foreach (var n4 in n3.Value as Dictionary<string, object>)
                                {
                                    sb.AppendLine($"{n4.Key} {n4.Value}");
                                }
                            }
                        }
                        else
                        {
                            sb.AppendLine($"{n2.Key} {n2.Value}");
                        }
                    }
                }
            }
            else if (obj.GetType() == typeof(Dictionary<string, object>))
            {
                foreach (var n in obj as Dictionary<string, object>)
                {
                    sb.AppendLine($"{n.Key} {n.Value}");
                }
            }
            else if (obj.GetType() == typeof(ArrayList))
            {
                foreach (var n in obj as ArrayList)
                {
                    foreach (var n2 in n as Dictionary<string, object>)
                    {
                        sb.AppendLine($"{n2.Key} {n2.Value}");
                    }
                }
            }
            else if (obj.GetType() == typeof(List<object>) || obj.GetType() == typeof(object[]))
            {
                foreach (var n in obj as object[])
                {
                    sb.AppendLine(n.ToString());
                }
            }
            else
            {
                return obj.ToString();
            }
            return sb.ToString();
        }

        private string XmlData(object obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<Contacts>");
            if (obj.GetType() == typeof(List<Dictionary<string, object>>))
            {
                foreach (var n in obj as List<Dictionary<string, object>>)
                {
                    foreach (var n2 in n)
                    {
                        //if (n2.Key.ToUpper().Equals("EMAIL")) mail = n2.Value.ToString();
                        if (n2.Key.ToUpper().Equals("EMAIL"))
                        {
                            
                            sb.AppendLine("\t<Node>");
                            sb.AppendLine($"\t\t<{n2.Key}> {n2.Value} </{n2.Key}>");
                            sb.AppendLine("\t</Node>");
                            
                        }

                        if (n.GetType() == typeof(Dictionary<string, object>))
                        {
                            foreach (var n3 in n)
                            {
                                if (n3.Value.GetType() == typeof(Dictionary<string, object>))
                                {
                                    sb.AppendLine("\t<Node>");
                                    foreach (var n4 in n3.Value as Dictionary<string, object>)
                                    {
                                        sb.AppendLine($"\t\t<{n4.Key}> {n4.Value} </{n4.Key}>");
                                    }
                                    sb.AppendLine("\t</Node>");
                                }
                            }
                        }
                        else if (n.GetType() == typeof(ArrayList))
                        {
                            foreach (var n3 in n)
                            {
                                sb.AppendLine("\t<Node>");
                                foreach (var n4 in n3.Value as Dictionary<string, object>)
                                {
                                    sb.AppendLine($"\t\t<{n4.Key}> {n4.Value} </{n4.Key}>");
                                }
                                sb.AppendLine("\t</Node>");
                            }
                        }
                        else
                        {
                            sb.AppendLine("\t<Node>");
                            sb.AppendLine($"\t\t<{n2.Key}> {n2.Value} </{n2.Key}>");
                            sb.AppendLine("\t</Node>");
                        }
                    }
                }
            }
            else if (obj.GetType() == typeof(Dictionary<string, object>))
            {
                foreach (var n in obj as Dictionary<string, object>)
                {
                    sb.AppendLine("\t<Node>");
                    sb.AppendLine($"\t\t<{n.Key}> {n.Value} </{n.Key}>");
                    sb.AppendLine("\t</Node>");
                }
            }
            else if (obj.GetType() == typeof(ArrayList))
            {
                foreach (var n in obj as ArrayList)
                {
                    sb.AppendLine("\t<Node>");
                    foreach (var n2 in n as Dictionary<string, object>)
                    {
                        sb.AppendLine($"\t\t<{n2.Key}> {n2.Value} </{n2.Key}>");
                    }
                    sb.AppendLine("\t</Node>");
                }
            }
            else if (obj.GetType() == typeof(List<object>) || obj.GetType() == typeof(object[]))
            {
                foreach (var n in obj as Dictionary<string,object>)
                {
                    sb.AppendLine("\t<Node>");
                    sb.AppendLine($"\t\t<{n.Key}> {n.Value} </{n.Key}>");
                    sb.AppendLine("\t</Node>");
                }
            }
            else
            {
                return obj.ToString();
            }
            sb.AppendLine("</Contacts>");
            return sb.ToString();
        }
    }
}
