using System;

namespace testcon.Classes
{
    public static class ResultInfo
    {
        public static void Show()
        {
            Console.WriteLine(Data);
            Console.SetCursorPosition(101, 0);
            Console.WriteLine("Batch Nr: " + AutoPilot.CountResultSets);
            Console.SetCursorPosition(101, 1);
            Console.WriteLine("Result Nr: " + AutoPilot.CountResults);
            Console.SetCursorPosition(101, 2);
            Console.WriteLine("Field Nr: " + AutoPilot.CountFields);
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
        }
        public static string Data { get; set; }
    }
}
