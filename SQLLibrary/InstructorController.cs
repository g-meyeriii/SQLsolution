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
            instructor.IsTenured = Convert.ToBoolean(reader["IsTenured"]);
            return instructor;
        }


        public static List<Instructor> GetAllInstructors() {
            var sql = "Select* From Instructor";
            var command = new SqlCommand(sql, bcConnection.Connection);
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                Console.WriteLine("GetAllInstructos has no rows");
                reader.Close();
                reader = null;
                return new List<Instructor>();
            }
            var instructors = new List<Instructor>();
            while (reader.Read()) {
                var instructor = LoadInstructorInstance(reader);
                instructors.Add(instructor);
            }
            reader.Close();
            reader = null;
            return instructors;
        }

        public  static Instructor GetInstructorById(int id) {
            var sql = "Select * From Instructor where Id = @Id";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", id);
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                reader.Close();
                reader = null;
                return null;
            }
            reader.Read();
            var instructor = LoadInstructorInstance(reader);
            reader.Close();
            reader = null;
            return instructor;
        }
        public bool InsertInstructor(Instructor instructor) {
           var sql = $" Insert into Instructor (Id, Firstname, Lastname, YearsExperience, IsTenured)" +
               $"Values(@Id, @Firstname, @Lastname, @YearsExperience, @IsTenured)";
           var  command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", instructor.Id);
            command.Parameters.AddWithValue("@Firstname", instructor.Firstname);
            command.Parameters.AddWithValue("@Lastname", instructor.Lastname);
            command.Parameters.AddWithValue("@YearsExperience", instructor.YearsExperience);
            command.Parameters.AddWithValue("@IsTenured", instructor.IsTenured);

            var recsAffected = command.ExecuteNonQuery();
            if (recsAffected != 1) {
                throw new Exception("Insert Failed");

            }
            return true;
        }
        public static bool UpdateInstructor(Instructor instructor) {
            var sql = "Update Major Set" +
                        " Id = @ Id " +
                        " Firstname = @Firstname" +
                        " Lastname = @ Lastname" +
                        " YearsExperienc = @YearsExperience" +
                        " IsTenured = @IsTenured";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", instructor.Id);
            command.Parameters.AddWithValue("@Firstname", instructor.Firstname);
            command.Parameters.AddWithValue("@Lastname", instructor.Lastname);
            command.Parameters.AddWithValue("@YearExperienc", instructor.YearsExperience);
            command.Parameters.AddWithValue("@IsTenured", instructor.IsTenured);

            var recsAffected = command.ExecuteNonQuery();
            if(recsAffected != 1) {
                throw new Exception("Update Failed");
            }
            return true;
        }
        public static bool DeleteInstructor(Instructor instructor) {
            var sql = "Delete From Instructor where Id = @Id";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", instructor.Id);
            var recsAffected = command.ExecuteNonQuery();
            if(recsAffected != 1) {
                throw new Exception("Delete Failed");
            }
            return true;
        }            

    }
    
}
