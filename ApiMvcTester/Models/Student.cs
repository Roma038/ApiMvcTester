using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ApiMvcTester.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [EmailAddress]
        public string email { get; set; }

        public string Degree { get; set; }

        public int ConfirmCode { get; set; }

    }
}