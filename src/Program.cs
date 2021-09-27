using System;
using System.Diagnostics;


namespace FastBackup
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Не указан путь источника");
                Console.ReadLine();
            }

            string sourceName = @"d:\33";
            string targetName = @"d:\111.7z";

            // 1
            // Initialize process information.
            //
            ProcessStartInfo p = new ProcessStartInfo();
            p.FileName = "7z.exe";

            // 2
            // Use 7-zip
            // specify a=archive and -tgzip=gzip
            // and then target file in quotes followed by source file in quotes
            //
            p.Arguments = "a -t7z \"" + targetName + "\" \"" + sourceName + "\" -mx=9";
            //p.WindowStyle = ProcessWindowStyle.Hidden;

            // 3.
            // Start process and wait for it to exit
            //
            Process x = Process.Start(p);
            x.WaitForExit();

			Console.WriteLine(args[0]);
            Console.ReadLine();
        }
    }
}
