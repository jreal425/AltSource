using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Banking.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (Environment.GetEnvironmentVariable("RUN_FRONTEND") == "true")
            {
                if (Environment.GetEnvironmentVariable("NPM_I") == "true")
                {
                    RunCommand($"/c npm i");
                }

                RunCommand($"/c npm run dev");
            }

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();


        // Could make this more dynamic, but for this project example 
        // I only need to pass in the console argurments.
        public static void RunCommand(string arugements)
        {
            var directory = $@"{Directory.GetCurrentDirectory()}\Banking.Frontend";

            var process = new ProcessStartInfo
            {
                UseShellExecute = false,
                WorkingDirectory = $@"{Directory.GetParent(Directory.GetCurrentDirectory())}\Banking.Frontend",
                FileName = @"C:\Windows\System32\cmd.exe",
                Arguments = arugements,
                WindowStyle = ProcessWindowStyle.Normal
            };

            Process.Start(process);
        }
    }
}
