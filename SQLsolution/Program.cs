using System;
using SQLLibrary;


namespace SQLsolution {
    public class Program {
        static void Main(string[] args) {

            var sqllib = new BcConnection();
            sqllib.Connect(@"localhost\sqlexpress", "EdDb", "trusted_connection=true");
            MajorController.bcConnection = sqllib;

            var majors = MajorController.GetAllMajors();
            foreach(var m in majors) {
                Console.WriteLine(m);
            }

            var major = MajorController.GetMajorByPk(1);
            Console.WriteLine(major);

            
            
            StudentController.bcConnection = sqllib;



            //var newStudent = new Student {
            //    Id = 443,
            //    Firstname = "Fred",
            //    Lastname = "Five",
            //    SAT = 500,
            //    GPA = 5.00,
            //    MajorId = 101
            //};
            //var success = StudentController.InsertStudent(newStudent);


            
            var student = StudentController.GetStudentByPk(888);
            if (student == null) {
                Console.WriteLine("Student not found");
            } else {
                Console.WriteLine(student);
            }
           // student.Firstname = "Charlie";
            //student.Lastname = "Chan";
            //var success = StudentController.UpdateStudent(student);

            var studentToDelete = new Student {
                Id = 999
            };
           // success = StudentController.DeleteStudent(999);

            //var student1 = StudentController.DeleteStudent(888);

            //var student = new Student(sqllib);
            var students = StudentController.GetAllStudents();
            foreach(var student0 in students) {
                Console.WriteLine(student0);
            }

            
            sqllib.Disconnect();


        }
    }
}
