using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace walkinthepark
{
    public class Program
    {
        //https://www.youtube.com/watch?v=-o2vZ13gIGU&list=PLpiOR7CBNvlo06U38G4rLbjE8cf18qzlO&index=2&t=885s
        // Really just a console application with libraries attached to it to allow Kestrel to run a web server
        // Program file runs the console application with libraries
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
