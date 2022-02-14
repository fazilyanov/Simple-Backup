using System;
using System.IO;

namespace Fastbackup
{
    public static class Common
    {
        /// <summary>
        /// Get BackupFolder path.
        /// </summary>
        /// <returns>BackupFolder path.</returns>
        public static string GetDefaultBackupFolder()
        {
            var confFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "conf.ini");

            Console.Write("Configuration file: ");
            if (!File.Exists(confFilePath))
            {
                Console.WriteLine("file is missing!");
                Console.WriteLine("press any key to exit..");
                Console.ReadLine();
                return null;
            }

            Console.WriteLine("OK");

            string defaultBackupFolder = null;
            var confText = File.ReadAllLines(confFilePath);

            foreach (var line in confText)
            {
                var splitLine = line.Split('=');
                if (splitLine.Length == 2 && splitLine[0] == "Path")
                {
                    defaultBackupFolder = splitLine[1].Trim();
                    break;
                }
            }

            Console.Write("The default folder for backup: ");
            if (string.IsNullOrEmpty(defaultBackupFolder))
            {
                Console.WriteLine("is not specified");
                Console.WriteLine("please specify the path in the conf.ini file");
                Console.WriteLine("press any key to exit..");
                Console.ReadLine();
                return null;
            }

            Console.WriteLine(defaultBackupFolder);
            return defaultBackupFolder;
        }
    }
}