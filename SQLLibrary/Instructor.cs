using System;
using System.Collections.Generic;
using System.Text;

namespace SQLLibrary {
   public class Instructor {

        public int Id { get; set; }
        public String Firstname { get; set; }
        public string Lastname { get; set; }
        public int YearsExperience { get; set; }
        public  bool IsTenure { get; set; }

       public Instructor() { }

    }
}
