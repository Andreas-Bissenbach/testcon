using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace testcon.Classes
{
    public static class Formation
    {
        public static string DeserializeJstring(this string source)
        {
            return JsonConvert.DeserializeObject(source) as string;
        }

        public static bool IsJson(string s)
        {
            return ((s.StartsWith("{") && s.EndsWith("}")) || (s.StartsWith("[") && s.EndsWith("]"))) ? true : false;
        }

        public static string GetBetween(this string source, string strStart, string strEnd)
        {
            int Start = source.IndexOf(strStart, 0) + strStart.Length;
            int End = source.IndexOf(strEnd, Start);
            return (source.Contains(strStart) && source.Contains(strEnd)) ? source.Substring(Start, End - Start) : "";
        }

        public static string BytesToString(this byte[] buffer)
        {
            return Encoding.UTF8.GetString(buffer, 0, buffer.Length);
        }

        public static string DSLQueryCvr(string cvr)
        {
            cvr = "\"" + cvr + "\"";
            string result = "{\"from\" : 0, \"size\": \"1\", \"query\":{" + "\"query_string\":{\"query\":"+cvr+",\"fields\":[\"Vrvirksomhed.cvrNummer\"],\"lenient\":true,\"default_operator\":\"AND\"}},"+ "\"_source\":[\"Vrvirksomhed.navne.navn\"]" + "}";
            return result;
        }

        public static string DSLQuery(string search, params string[] fields)
        {
            string ExtractedFields = string.Empty;
            if (fields.Length.Equals(1))
            {
                ExtractedFields = "\""+fields[0]+"\""; 
            }
            else
            {
                for(int i = 0; i < fields.Length-1; i++)
                {
                    ExtractedFields += "\"" + fields[i] + "\",";
                }
                ExtractedFields += "\"" + fields[fields.Length-1] + "\"";
            }


            search = "\"" + search + "\"" + ",\"fields\":[" + ExtractedFields + "],\"lenient\":true,\"default_operator\":\"AND\"}}}";


            string result = "{\"query\":{\"query_string\":{\"query\":";

            return result + search;
        }

        public static string ArrayToString<T>(this T array)
        {
            Type t = array.GetType();
            int i;
            if (t == typeof(string[]))
            {
                string[] ArrayIdentified = array as string[];
                StringBuilder StringArrayValues = new StringBuilder(ArrayIdentified.Length);
                
                for (i = 0; i < ArrayIdentified.Length; i++)
                {
                    StringArrayValues.Append(ArrayIdentified[i]);
                }
                return StringArrayValues.ToString();
            }
            return "";
        }

        public static string RemoveWhiteSpace(this string source)
        {
            return Regex.Replace(source, @"\s+", "");
        }

        public static string RemoveChar(this string txt, char[] chars)
        {
            for (int i = 0; i < chars.Length; i++)
            {
                txt = txt.Replace(chars[i].ToString(), string.Empty);
            }
            return txt;
        }

        public static string SplitString(this string source, string characters)
        {
            StringBuilder sb = new StringBuilder(source.Length);
            string[] arr = source.Split(new string[] { characters }, StringSplitOptions.None);
            for (int i = 0; i < arr.Length; i++)
            {
                sb.AppendLine(arr[i]);
            }
            return sb.ToString();
        }

        public static string WithStringInfo(this string source)
        {
            string characters = $"Characters: {source.ToCharArray().Length} ";
            return source + "\r\n\r\n" + characters + source.CountJsonWords();
        }

        private static string CountJsonWords(this string source)
        {
            return FormatJson(source).CountWords();
        }

        private static string CountWords(this string source)
        {
            int wordCount = 0, index = 0;

            // skip whitespace until first word
            while (index < source.Length && char.IsWhiteSpace(source[index]))
                index++;

            while (index < source.Length)
            {
                // check if current char is part of a word
                while (index < source.Length && !char.IsWhiteSpace(source[index]))
                    index++;

                wordCount++;

                // skip whitespace until next word
                while (index < source.Length && char.IsWhiteSpace(source[index]))
                    index++;
            }
            return $"Words: {wordCount}";
        }

        public static string AsProperName(this string source)
        {
            if (source == null)
                return null;

            if (source.Length > 1)
                return char.ToUpper(source[0]) + source.Substring(1);

            return source.ToUpper();
        }

        public static string TryGetValues(Dictionary<string, object> dictionary, string searchkey)
        {
            bool check = dictionary.TryGetValue(searchkey, out string value);
            return CheckKeys(value, searchkey, check);
        }

        private static string CheckKeys(string value, string key, bool exists)
        {
            return exists ? $"{key}: {value}" : $"{key}: Null";
        }

        public static string DictionaryValues(this Dictionary<string, object> dictionary)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var n in dictionary)
            {
                sb.AppendLine(n.ToString());
            }
            return sb.ToString();
        }

        private static readonly string INDENT_STRING = "    ";
        public static string FormatJson(string source)
        {
            int indent = 0;
            bool quoted = false;
            StringBuilder sb = new StringBuilder(source.Length);
            for (var i = 0; i < source.Length; i++)
            {
                char ch = source[i];
                switch (ch)
                {
                    case '{':
                    case '[':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, ++indent).ToList().ForEach(item => sb.Append(INDENT_STRING));
                        }
                        break;
                    case '}':
                    case ']':
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, --indent).ToList().ForEach(item => sb.Append(INDENT_STRING));
                        }
                        sb.Append(ch);
                        break;
                    case '"':
                        sb.Append(ch);
                        bool escaped = false;
                        int index = i;
                        while (index > 0 && source[--index] == '\\')
                            escaped = !escaped;
                        if (!escaped)
                            quoted = !quoted;
                        break;
                    case ',':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, indent).ToList().ForEach(item => sb.Append(INDENT_STRING));
                        }
                        break;
                    case ':':
                        sb.Append(ch);
                        if (!quoted)
                            sb.Append(" ");
                        break;
                    default:
                        sb.Append(ch);
                        break;
                }
            }
            return sb.ToString();
        }
    }
}
