using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace SQLLibrary {
    public class  MajorController {

        public static BcConnection bcConnection { get; set; } //Contains the sql connection

        private static Major LoadMajorInstance(SqlDataReader reader) {
            var major = new Major();
            major.Id = Convert.ToInt32(reader["Id"]);
            major.Description = reader["Description"].ToString();
            major.MinSat = Convert.ToInt32(reader["MinSat"]);
            return major;
        }
        
        public static List<Major> GetAllMajors() {
            var sql = "SELECT* From Major;";
            var command = new SqlCommand(sql, bcConnection.Connection);
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                Console.WriteLine("No rows for GetAllMajor");
                reader.Close();
                reader = null;
                return new List<Major>();
            }
            var majors = new List<Major>();
            while (reader.Read()) {
                var major = LoadMajorInstance(reader); //Created the method created above to obsolete the code below
                //var major = new Major();
                //major.Id = Convert.ToInt32(reader["Id"]);
                //major.Description = reader["Description"].ToString();
                //major.MinSat = Convert.ToInt32(reader["MinSat"]);
                majors.Add(major);
            }
            reader.Close();
            reader = null;
            return majors;
        }
        public static Major GetMajorByPk(int id) {
            var sql = "Select* From Major where Id = @Id";
            var commmand = new SqlCommand(sql, bcConnection.Connection);
            commmand.Parameters.AddWithValue("@Id", id);
            var reader = commmand.ExecuteReader();
            if (!reader.HasRows) {
                reader.Close();
                reader = null;
                return null;
            }
            reader.Read();
            var major = LoadMajorInstance(reader);
            //var major = new Major();
            //major.Id = Convert.ToInt32(reader["Id"]);
            //major.Description = reader["Description"].ToString();
            //major.MinSat = Convert.ToInt32(reader["MinSat"]);

            reader.Close();
            reader = null;
            return major;
            
        }

        public bool InsertMajor(Major major) {
            var sql = $" Insert into Major (Id, Description, MinSat)" +
                $"Values(@Id,@Descripton,@MinSat)";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", major.Id);
            command.Parameters.AddWithValue("@Description", major.Description);
            command.Parameters.AddWithValue("@Minsat", major.MinSat);

            var recsAffected = command.ExecuteNonQuery();
            if (recsAffected != 1) {
                throw new Exception("Insert Failed");
            }
            return true;
        }

        public static bool UpdateMajor(Major major) {
            var sql = "Update Major Set" +
                        " Id = @Id " +
                        " Description = @Description " +
                        " MinSat = @MinSat ";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", major.Id);
            command.Parameters.AddWithValue("@Description", major.Description);
            command.Parameters.AddWithValue("@MinSat", major.MinSat);

            var resAffected = command.ExecuteNonQuery();
            if(resAffected != 1) {
                throw new Exception("Update Failed");
            }
            return true;

        }
        public bool DeleteMajor(Major major) {
            var sql = "Delete From Major where Id = @Id";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", major.Id);
            var recsAffected = command.ExecuteNonQuery();
            if (recsAffected != 1) {
                throw new Exception("Delete Failed");
            }
            return true;
        }
        //public static bool DeleteMajor(int id) {
        //    var sql = "Delete From Major where Id = @Id";
        //    var command = new SqlCommand(sql, bcConnection.Connection);
        //    var major = GetMajorByPk(id);
        //    if(major == null) {
        //        return false;
               
            

            //var success = DeleteMajor(major);
            //return true;
        
    
    }
}


