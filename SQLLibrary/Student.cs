﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SQLLibrary {
   public class Student {

        public int  Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int SAT { get; set; }
        public double GPA { get; set; }
        public int? MajorId { get; set; }

        public override string ToString() {
            return $"{Id}|{Firstname} {Lastname}|{SAT}|{GPA}|{MajorId}";
        }

        public Student() { }


        }
    }

