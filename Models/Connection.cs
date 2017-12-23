using System;
using System.Data.SqlClient;

namespace DINGSOMETHING.Models
{
    public class Connection
    {
        protected string ConnectionString {get;set;}

        public Connection(){
            this.ConnectionString = Helper.ConnectionHelper.GetSQLConnectionString();
        }

        public SqlConnection GetConnection() {
            return new SqlConnection(this.ConnectionString);
        }

    }

}