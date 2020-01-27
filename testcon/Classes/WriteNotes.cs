using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace testcon.Classes
{
    public static class WriteNotes
    {
        private static readonly Stopwatch watch = new Stopwatch();
        private static string TimeElapse => $"{(double)watch.ElapsedMilliseconds / 1000} Seconds";
        public static string GetTimeSpend { get; set; }

        private static bool Append;
        private static readonly string TextFile = @"\Notes\QueryLog.txt";
        //private static readonly string[] files = File.ReadAllLines(path);

        private static readonly string workingDirectory = Environment.CurrentDirectory;

        private static readonly string path = Directory.GetParent(workingDirectory).Parent.FullName + TextFile;

        public static void WriteNote(string content)
        {

            using StreamWriter file = new StreamWriter(path,Append);

            file.WriteLine(content);
            Append = true;
        }

        public static void Initialize()
        {
            watch.Start();
        }

        public static void Final()
        {
            watch.Reset();
            GetTimeSpend = string.Empty;
        }

        public static void Time()
        {
            GetTimeSpend = TimeElapse;
            Console.WriteLine(GetTimeSpend);
            Final();
        }


    }
}
