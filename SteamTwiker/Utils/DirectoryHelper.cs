using Microsoft.Win32;
using SteamTwiker.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace SteamTwiker.Utils
{
    public class DirectoryHelper
    {
        private static readonly string AppDataPath =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private readonly string AppPath = AppDataPath + "\\SteamTwiker\\";

        public List<Account> GetAllAccountsFromPath()
        {
            List<Account> files = new List<Account>();

            if (!Directory.Exists(AppPath))
                Directory.CreateDirectory(AppPath);

            foreach (string file in Directory.EnumerateFiles(AppPath, "*.bat", SearchOption.AllDirectories))
            {
                files.Add(new Account() { Name = file.Split("\\").Last().Replace(".bat", string.Empty) });
            }

            return files;
        }
        public void RemoveAccount(Account account)
        {
            if (File.Exists(AppPath + account.Name + ".bat"))
            {
                File.Delete(AppPath + account.Name + ".bat");
            }
        }
        public void AddAccount(Account account)
        {
            string? steamPath = Registry
                .GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Valve\Steam",
                "InstallPath",
                null)!.ToString();

            string batContent =
                    "@echo off\n" +
                    "taskkill /f /im steam.exe\n" +
                    "@echo off\n" +
                    $"start {steamPath}\\steam.exe -login {account.Name} {account.Password}\n" +
                    "exit 0";

            using (FileStream stream = File.Open(AppPath + account.Name + ".bat", FileMode.OpenOrCreate))
            {
                byte[] buffer = Encoding.UTF8.GetBytes(batContent);
                stream.Write(buffer);
            }
        }
        public void RunBatFile(string path)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.FileName = AppPath + path + ".bat";
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardInput = true;
            processInfo.RedirectStandardOutput = true;
            processInfo.UseShellExecute = false;
            processInfo.CreateNoWindow = true;

            Process process = new Process();
            process.StartInfo = processInfo;
            process.Start();
            process.WaitForExit();
        }
    }
}
