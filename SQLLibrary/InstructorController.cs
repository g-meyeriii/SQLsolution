using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace SQLLibrary {
    public class InstructorController {

        public static BcConnection bcConnection { get; set; }

        private static Instructor LoadInstructorInstance(SqlDataReader reader) {
            var instructor = new Instructor();
            instructor.Id = Convert.ToInt32(reader["Id"]);
            instructor.Firstname = reader["FirstName"].ToString();
            instructor.Lastname = reader["Lastname"].ToString();
            instructor.YearsExperience = Convert.ToInt32(reader["YearsExperience"]);
            instructor.IsTenure = Convert.ToBoolean(reader["IsTenured"]);
            return instructor;
        }


        public static List<Instructor> GetAllInstructors() {
            var sql = "Select* From Instructor";
            var command = new SqlCommand(sql, bcConnection.Connection)
            var reader = command.ExecuteReader();
                if (!reader.HasRows) {
                Console.WriteLine("GetAllInstructos has no rows");
                reader.Close();
                reader = null;
                return List<Instructor>;
            }
            var instructor = new List<Instructor>();
            while (reader.Read()) {
                var Instructor = LoadInstructorInstance(reader);
                
        }

    }
    
}
