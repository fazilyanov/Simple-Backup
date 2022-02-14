using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Fastbackup
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("file/path is not specified");
                Console.ReadLine();
                return;
            }

            var defaultBackupFolder = Common.GetDefaultBackupFolder();
            if (string.IsNullOrEmpty(defaultBackupFolder))
            {
                return;
            }

            var sourcePath = args[0];

            if (!File.Exists(sourcePath) && !Directory.Exists(sourcePath))
            {
                Console.WriteLine($"file/path '{sourcePath}' does not exist");
                Console.ReadLine();
                return;
            }

            var isFolder = (File.GetAttributes(sourcePath) & FileAttributes.Directory) == FileAttributes.Directory;

            var fileName =
                isFolder ?
                    Path.GetFileName(Path.GetDirectoryName(sourcePath)) :
                    Path.GetFileName(sourcePath);

            fileName = $"{fileName}_{DateTime.Now:yyyy_MM_dd_HH_mm_ss}.7z";

            // local c:\somefolder => c\somefolder
            var targetPath = sourcePath.Replace(":", string.Empty);

            // net \\pc\somefolder => pc\somefolder
            if (targetPath.IndexOf("\\\\", StringComparison.Ordinal) == 0)
            {
                targetPath = targetPath.Substring(2);
            }

            // shared \shared\somefolder => shared\somefolder
            if (targetPath.IndexOf("\\", StringComparison.Ordinal) == 0)
            {
                targetPath = targetPath.Substring(1);
            }

            targetPath = Path.Combine(defaultBackupFolder, targetPath, fileName);

            var p = new ProcessStartInfo
            {
                FileName = "7z.exe",
                Arguments = "a -t7z \"" + targetPath + "\" \"" + sourcePath + "\" -mx=9",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            try
            {
                Console.WriteLine("Running 7z..");
                var proc = Process.Start(p);

                while (!proc.StandardOutput.EndOfStream)
                {
                    Console.WriteLine(proc.StandardOutput.ReadLine());
                }

                var output = proc.StandardOutput.ReadToEnd();
                Console.WriteLine(output);
                var err = proc.StandardError.ReadToEnd();
                Console.WriteLine(err);

                proc.WaitForExit();

                if (proc.ExitCode == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nbackup has been created \n" + targetPath);
                    Thread.Sleep(2000);
                    return;
                }

                Console.WriteLine("press any key to exit..");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine($"7z.exe program execution error");
                Console.WriteLine(e.Message);
                Console.WriteLine("press any key to exit..");
                Console.ReadLine();
            }
        }
    }
}
