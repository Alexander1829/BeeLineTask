using Calculus.DTO.Config;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Calculus.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateDbFile();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            ;

        /// <summary>
        /// Создать файлы для записи, если они не были созданы.
        /// </summary>
        private static void CreateDbFile()
        {
            string fileCurrent = AppConfig.FilePath_Current;            
            if (!File.Exists(fileCurrent))
            {
                File.Create(fileCurrent);
            }
            string fileItems = AppConfig.FilePath_Items;
            if (!File.Exists(fileItems))
            {
                File.Create(fileItems);
            }
        }
    }
}
