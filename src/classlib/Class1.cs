using System;
using System.Data.SqlClient;

namespace classlib
{
    public class Class1
    {
        public Class1()
        {
            SqlConnectionStringBuilder connectionInfo = new SqlConnectionStringBuilder("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Stuff;");
        }
    }


}
