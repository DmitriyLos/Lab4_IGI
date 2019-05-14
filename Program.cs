using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Lab4;
using Lab4.Data;

namespace Lab4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (STOContext db = new STOContext())
            {
                DbInitializer.Initialize(db);
            }
                CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
