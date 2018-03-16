using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;

namespace sqlclient.runtime.test
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var connectionInfo = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PaymentLocal;Integrated Security=True;";
            //var connectionInfo = "Data Source=someserver\\MSSQLLocalDB;Initial Catalog=PaymentLocal;Integrated Security=True;";
            MigrateDatabase(connectionInfo);            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }

        public void MigrateDatabase(string connectionString, TextWriter output = null)
        {
            var runner = new RunnerContext(new TextWriterAnnouncer(output ?? Console.Out))
            {
                Database = "SqlServer",
                Connection = connectionString,
                Targets = new[] { typeof(Startup).Assembly.Location }
            };

            new TaskExecutor(runner).Execute();
        }
    }
}
