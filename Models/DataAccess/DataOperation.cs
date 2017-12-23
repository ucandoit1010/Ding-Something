using System;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;using Dapper;


namespace DINGSOMETHING.Models.DataAccess
{
    public class DataOperation : IDataOperation
    {
        public string ConnectionString { get; set; }

        public DataOperation(string connectionStr) {
            this.ConnectionString = connectionStr;
        }


        public int Execute(string sql, object param = null)
        {
            using(SqlConnection conn = new SqlConnection(this.ConnectionString)) {

                try
                {
                    conn.Open();
                    return conn.Execute(sql,param);
                }
                catch (Exception err)
                {
                    throw err;
                }
            }
        }

        public IEnumerable<T> Query<T>(string sql, object param = null)
        {
            using(SqlConnection conn = new SqlConnection(this.ConnectionString)) {
                
                try
                {
                    conn.Open();
                    return conn.Query<T>(sql,param).ToList();
                }
                catch (Exception err)
                {
                    throw err;
                }
            }
        }

    }


}