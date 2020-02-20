using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace SQLLibrary {
    public class MajorController {

        public static BcConnection bcConnection { get; set; } //Contains the sql connection

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
                var major = new Major();
                major.Id = Convert.ToInt32(reader["Id"]);
                major.Description = reader["Description"].ToString();
                major.MinSat = Convert.ToInt32(reader["MinSat"]);
                majors.Add(major);
            }
            reader.Close();
            reader = null;
            return majors;
        }

        public static Major GetByMajorId(string Desciption) {
            var sql = "SELECT* From Major Where description = @description ";
            var command = new SqlCommand(sql, bcConnection.Connection);
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                Console.WriteLine("No rows for GetByMajorId");
                reader.Close();
                reader = null;
                return new List<Major>();
            }
            var majors = new List<Major>();
            while (reader.Read());
            var specmajor = new Major();
            major.Id = Convert.ToInt32(reader["Id"]);
            ma
            reader.Close();
            reader = null;
            return major;
            }

            

    }
}
