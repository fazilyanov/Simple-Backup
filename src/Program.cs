using System;
using System.Diagnostics;
using Microsoft.Win32;


namespace FastBackup
{
    class Program
    {


        static void Main(string[] args)
        {
            SetRegistry();

            //using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"HKEY_CLASSES_ROOT\*\shell"))
            //{
            //    key.SetValue("Title", $".NET Core Rocks! {DateTime.UtcNow:MM/dd/yyyy-HHmmss}");
            //}


            //         if (args.Length == 0)
            //         {
            //             Console.WriteLine("Не указан путь источника");
            //             Console.ReadLine();
            //         }

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

        private static void SetRegistry()
        {
            const string keyName = @"*\shell\FastBackup\";
            using (var key = Registry.ClassesRoot.CreateSubKey(keyName))
            {
                if (key != null)
                {
                    key.SetValue(null, "FastBackup");
                    using (var skey = Registry.ClassesRoot.CreateSubKey(keyName+"command"))
                    {
                        if (skey!=null)
                        {
                            skey.SetValue(null, @"'C:\Users\a.fazilyanov\AppData\Local\Programs\Microsoft VS Code\Code.exe' '%1'");
                        }
                        
                    }

                }
                else
                {
                    Console.WriteLine("fuuuu");
                }
            }

            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\edi_wang"))
            {
                key.SetValue("Title", $".NET Core Rocks! {DateTime.UtcNow:MM/dd/yyyy-HHmmss}");
            }
        }
    }
}
