using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;

namespace sqlclient.runtime.test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyDb;Integrated Security=True;";            

            var runner = new RunnerContext(new TextWriterAnnouncer(Console.Out))
            {
                Database = "SqlServer",
                Connection = connectionString,
                Targets = new[] { typeof(Program).Assembly.Location }
            };

            new TaskExecutor(runner).Execute();
            // BuildWebHost(args).Run();
        }

        // public static IWebHost BuildWebHost(string[] args) =>
        //     WebHost.CreateDefaultBuilder(args)
        //         .UseStartup<Startup>()
        //         .Build();
    }
}
