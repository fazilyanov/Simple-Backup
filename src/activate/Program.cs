using System;
using System.IO;
using System.Threading;
using Fastbackup;
using Microsoft.Win32;

namespace Config
{
    internal static class Program
    {
        private const string AnyFileSubKey = @"*\shell\FastBackup\";
        private const string DirectorySubKey = @"Directory\shell\FastBackup\";

        private static void Main()
        {
            Console.WriteLine("1 - Activate");
            Console.WriteLine("2 - Deactivate");

            var num = -1;
            while (!(num == 1 || num == 2))
            {
                var key = Console.ReadKey();
                int.TryParse(key.KeyChar.ToString(), out num);
                Console.SetCursorPosition(0, 2);
            }

            if (num == 2)
            {
                if (Registry.ClassesRoot.OpenSubKey(DirectorySubKey) != null)
                    Registry.ClassesRoot.DeleteSubKeyTree(DirectorySubKey);

                if (Registry.ClassesRoot.OpenSubKey(AnyFileSubKey) != null)
                    Registry.ClassesRoot.DeleteSubKeyTree(AnyFileSubKey);

                Console.WriteLine("Successfully deactivated");
                Thread.Sleep(2000);
                return;
            }

            var defaultBackupFolder = Common.GetDefaultBackupFolder();

            if (string.IsNullOrEmpty(defaultBackupFolder))
                return;

            try
            {
                Console.Write("Folder ");
                if (Directory.Exists(defaultBackupFolder))
                {
                    Console.WriteLine($"'{defaultBackupFolder}' already exists");
                }
                else
                {
                    var createdDirectory = Directory.CreateDirectory(defaultBackupFolder);
                    Console.WriteLine($"'{createdDirectory.FullName}' was successfully created");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error creating a folder '{defaultBackupFolder}'");
                Console.WriteLine(e.Message);
                Console.WriteLine("press any key to exit..");
                Console.ReadLine();
                return;
            }
            
            Console.Write("Adding a command to the context menu: ");

            if (AddContextMenuCommand(AnyFileSubKey, "Backup file..") &&
                AddContextMenuCommand(DirectorySubKey, "Backup folder.."))
            {
                Console.WriteLine("Ok");
            }
            else
            {
                Console.WriteLine("error");
                Console.WriteLine("press any key to exit..");
                Console.ReadLine();
            }

            Console.WriteLine("Activated");
            Console.WriteLine("press any key to exit..");
            Console.ReadLine();
        }

        private static bool AddContextMenuCommand(string subKeyName, string caption)
        {
            try
            {
                using (var key = Registry.ClassesRoot.CreateSubKey(subKeyName))
                {
                    if (key == null)
                    {
                        return false;
                    }

                    key.SetValue(null, caption);
                    key.SetValue("Icon", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "icon.ico"));

                    using (var skey = Registry.ClassesRoot.CreateSubKey(subKeyName + "command"))
                    {
                        if (skey == null)
                        {
                            return false;
                        }

                        skey.SetValue(
                            null,
                            "\"" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "fastbackup.exe") + "\" \"%1\"");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }
    }
}
