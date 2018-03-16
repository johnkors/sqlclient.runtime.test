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
            var connectionString = "Data Source=wackyserver\\MSSQLLocalDB;Initial Catalog=MyDb;Integrated Security=True;";            

            var runner = new RunnerContext(new TextWriterAnnouncer(Console.Out))
            {
                Database = "SqlServer",
                Connection = connectionString,
                Targets = new[] { typeof(Program).Assembly.Location }
            };

            try
            {
                new TaskExecutor(runner).Execute();
            }
            catch(SqlException)
            {
                Console.WriteLine("App finished.");
            }
            
        }
    }
}
