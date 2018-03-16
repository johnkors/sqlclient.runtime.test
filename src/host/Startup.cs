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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyDb;Integrated Security=True;";            

            var runner = new RunnerContext(new TextWriterAnnouncer(Console.Out))
            {
                Database = "SqlServer",
                Connection = connectionString,
                Targets = new[] { typeof(Startup).Assembly.Location }
            };

            new TaskExecutor(runner).Execute();
        }
    }
}
