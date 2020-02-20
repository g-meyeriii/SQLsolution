using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SQLLibrary {
    public class StudentController {//Student class to call the database, removing the things that shouldn't be there


        public static BcConnection bcConnection { get; set; }

        public static List<Student> GetAllStudents() {
            var sql = "Select* From Student s Left Join Major m on m.Id = s.MajorId"; //Test in SSMS first
            var command = new SqlCommand(sql, bcConnection.Connection);
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                Console.WriteLine("No rows from GetAllStudent()");
                reader.Close();
                reader = null;
                return new List<Student>();
            }
            
            var students = new List<Student>();
            while (reader.Read()) {
                var student = new Student();
                student.Id = Convert.ToInt32(reader["Id"]);
                student.Firstname = reader["Firstname"].ToString();
                student.Lastname = reader["Lastname"].ToString();
                student.SAT = Convert.ToInt32(reader["SAT"]);
                student.GPA = Convert.ToInt32(reader["GPA"]);
                //student.MajorId = Convert.ToInt32(reader["MajorId"]);
                if (Convert.IsDBNull(reader["Description"])) {
                    student.Major = null;
                } else {
                    var major = new Major {
                        Description = reader["Description"].ToString(),
                        MinSat = Convert.ToInt32(reader["MinSat"])
                    };
                }
                students.Add(student);
            }
            reader.Close();
            reader = null;
            return students;

        }

        public static Student GetStudentByPk(int id) {
            var sql = $"Select * From Student where ID = {id}";
            var command = new SqlCommand(sql, bcConnection.Connection);
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                return null;
            }
            reader.Read();
            var student = new Student();
            student.Id = Convert.ToInt32(reader["Id"]);
            student.Firstname = reader["Firstname"].ToString();
            student.Lastname = reader["Lastname"].ToString();
            student.SAT = Convert.ToInt32(reader["SAT"]);
            student.GPA = Convert.ToInt32(reader["GPA"]);
            //student.MajorId = Convert.ToInt32(reader["MajorId"]);

            reader.Close();//Closing out reader, it is a expesive, but less than connection to sql
            reader = null;// Allows the garbage collector to collect the unused stuff and free up memory, efficiency

            return student;

        }

        public static bool InsertStudent(Student student) {


            //#error stops running code. Using string interpolation for SQL statements is not good practice      
            var sql = $"Insert into Student (Id, Firstname, Lastname, SAT, GPA, MajorId)" +
                    $"VALUES(@Id, @Firstname, @Lastname,@SAT, @GPA, @Majorid);";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", student.Id);
            command.Parameters.AddWithValue("@Firstname", student.Firstname);
            command.Parameters.AddWithValue("@Lastname", student.Lastname);
            command.Parameters.AddWithValue("@SAT", student.SAT);
            command.Parameters.AddWithValue("@GPA", student.GPA);
            command.Parameters.AddWithValue("@MajorId", student.MajorId ?? Convert.DBNull);

            var recsAffected = command.ExecuteNonQuery();
            if (recsAffected != 1) {
                throw new Exception("Insert failed");
            }
            return true;


        }
        public static bool UpdateStudent(Student student) {
            var sql = "UPDATE Student Set " +
                        " Firstname = @Firstname, " +
                        " Lastname = @Lastname, " +
                        " SAT = @SAT, " +
                        " GPA = @GPA, " +
                        " MajorId = @MajorId " +
                        " Where Id = @Id; ";

            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", student.Id);
            command.Parameters.AddWithValue("@Firstname", student.Firstname);
            command.Parameters.AddWithValue("@Lastname", student.Lastname);
            command.Parameters.AddWithValue("@SAT", student.SAT);
            command.Parameters.AddWithValue("@GPA", student.GPA);
            command.Parameters.AddWithValue("@MajorId", student.MajorId ?? Convert.DBNull);

            var recsAffected = command.ExecuteNonQuery();
            if (recsAffected != 1) {
                throw new Exception("Updated failed");
            }
            return true;

        }
        public static bool DeleteStudent(Student student) {
            var sql = "DELETE From Student Where Id = @Id";

            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", student.Id);
            var recsAffected = command.ExecuteNonQuery();
            if (recsAffected != 1) {
                throw new Exception("Delete Failed");
            }
            return true;

        }
        public static bool DeleteStudent(int id) {
            var student = GetStudentByPk(id);
            if (student == null) {
                return false;
            }
            var success = DeleteStudent(student);
            return true;

        }
    }
 }

     
