using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ApiMvcTester.Models
{
    public class StudentTestApiContext : DbContext
    {
        public StudentTestApiContext() : base("Ebunlar")
        {

        }
        public DbSet<Student> Students { get; set; }
    }
}