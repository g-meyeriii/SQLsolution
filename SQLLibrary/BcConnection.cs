using System;
using System.Data.SqlClient;

namespace SQLLibrary {
    
    public class BcConnection  {

        public SqlConnection Connection { get; set; } //SQL connection example

        //Connection string   first parameter @"server=localhost\sqlexpress; second parameter database=EdDb; third parameter trusted_connection=true;";

        public void Connect(string server, string database, string auth) { //This is how to connect make a connection to sql
            var connStr = ($"Server ={server}; database = {database};{auth}");
            Connection = new SqlConnection(connStr);
            Connection.Open(); //Trap catch or no conncetion can blow up the program.
            
            if(Connection.State != System.Data.ConnectionState.Open) { //How to know if your conncetion is good
                Console.WriteLine("Could not open the connection -- check connnection string");
                Connection = null;
                return;
            }
            Console.WriteLine("Connection opened");
        }
        public void Disconnect() { //Disconnect the sql connection and return it
            if(Connection == null) {
                return;
            }
            if (Connection.State == System.Data.ConnectionState.Open) {
                Connection.Close();
                Connection.Dispose();
                Connection = null;
            }
        }

    }
}
