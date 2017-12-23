using System;


namespace DINGSOMETHING.Models.Helper
{
    public class ConnectionHelper
    {
        public static string GetSQLConnectionString(){
            return "Server=(local);Database=Ding;Trusted_Connection=True;";
        }

    }

}