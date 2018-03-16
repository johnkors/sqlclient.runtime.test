using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;

namespace classlib
{
    public class Datab
    {
        public void MigrateDatabase(string connectionString, TextWriter output = null)
        {
            CreateDatabase(connectionString);

            var runner = new RunnerContext(new TextWriterAnnouncer(output ?? Console.Out))
            {
                Database = "SqlServer",
                Connection = connectionString,
                Targets = new[] { typeof(Datab).Assembly.Location }
            };

            //new TaskExecutor(runner).Execute();
        }

        public string GenerateUniqueDatabaseName(string originialConnectionString)
        {
            SqlConnectionStringBuilder connectionInfo = new SqlConnectionStringBuilder(originialConnectionString);
            connectionInfo.InitialCatalog = string.Format(CultureInfo.InvariantCulture,
                                                         "{0}_{1}",
                                                         connectionInfo.InitialCatalog,
                                                         Guid.NewGuid());
            return connectionInfo.ToString();
        }

        private void CreateDatabase(string connectionString)
        {
            var connectionInfo = new SqlConnectionStringBuilder(connectionString);

            var master = new SqlConnectionStringBuilder(connectionString)
            {
                InitialCatalog = "master"
            };

            using (var sqlConnection = new SqlConnection(master.ToString()))
            {
                sqlConnection.Open();

                using (SqlCommand createDbCommand = sqlConnection.CreateCommand())
                {
                    createDbCommand.CommandTimeout = 1;
                    var command = string.Format(
@"if db_id('{0}') is null
BEGIN
    CREATE DATABASE [{0}]
    ALTER DATABASE [{0}] SET ALLOW_SNAPSHOT_ISOLATION ON
    ALTER DATABASE [{0}] SET RECOVERY SIMPLE
END", connectionInfo.InitialCatalog);
                    createDbCommand.CommandText = command;
                    createDbCommand.ExecuteNonQuery();
                }

                sqlConnection.Close();
            }
        }

        public void DeleteDatabase(string connectionstring)
        {
            SqlConnectionStringBuilder connectionInfo = new SqlConnectionStringBuilder(connectionstring);

            SqlConnectionStringBuilder master = new SqlConnectionStringBuilder(connectionstring)
            {
                InitialCatalog = "master"
            };

            using (SqlConnection sqlConnection = new SqlConnection(master.ToString()))
            using (SqlCommand createDbCommand = sqlConnection.CreateCommand())
            {
                sqlConnection.Open();
                createDbCommand.CommandText = string.Format(
@"if db_id('{0}') is not null 
BEGIN 
    ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE 
    DROP DATABASE [{0}] 
END",
                connectionInfo.InitialCatalog);
                createDbCommand.ExecuteNonQuery();
            }
        }
    }
}
