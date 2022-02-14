using System;
using System.Diagnostics;
using Microsoft.Win32;


namespace FastBackup
{
    class Program
    {
        
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("1 - Activate");
                Console.WriteLine("2 - Deactivate");

                Console.ReadLine();
            }

            //         string sourceName = @"d:\33";
            //         string targetName = @"d:\111.7z";

            //         var p = new ProcessStartInfo
            //         {
            //             FileName = "7z.exe",
            //             Arguments = "a -t7z \"" + targetName + "\" \"" + sourceName + "\" -mx=9"
            //         };


            //         Process x = Process.Start(p);
            //         x.WaitForExit();

            //Console.WriteLine(args[0]);
            Console.ReadLine();
        }
        
    }
}
