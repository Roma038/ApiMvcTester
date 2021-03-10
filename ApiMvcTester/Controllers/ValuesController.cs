using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using ApiMvcTester.Models;
using Newtonsoft.Json;

namespace ApiMvcTester.Controllers
{
    public class ValuesController : ApiController
    {
        StudentTestApiContext dbase = new StudentTestApiContext();
        // GET api/values
        public string Get()
        {
            
            IEnumerable<Student> Students = dbase.Students;

            return JsonConvert.SerializeObject(Students);

        }

        // GET api/values/5
        public string Get(int id)
        {
            using (StudentTestApiContext db = new StudentTestApiContext())
            {
                var stud = db.Students.Where(x => x.Id == id).FirstOrDefault();
                if (stud != null)
                {
                    return JsonConvert.SerializeObject(stud);
                }
                else
                {
                    return JsonConvert.SerializeObject("Not Found");
                }
            }
            
        }

        // POST api/values
        public void Post([FromBody] Student student)
        {
            using (StudentTestApiContext db = new StudentTestApiContext())
            {
                if (student != null)
                {
                    db.Students.Add(student);
                    db.SaveChanges();
                    
                }
                
                
            }
        }

        // PUT api/values/5
        public IHttpActionResult Put(int id,[FromBody] Student student)
        {
            using (var db = new StudentTestApiContext())
            {
                var stud = db.Students.Where(x => x.Id == id).FirstOrDefault();
                if (stud != null)
                {
                    stud.Id = student.Id;
                    stud.Name = student.Name;
                    stud.email = student.email;
                    stud.Degree = student.Degree;
                    db.SaveChanges();
                }
                return Redirect("https://localhost:44314/api/Values");
            }
        }

        // DELETE api/values/5
        public IHttpActionResult Delete(int id)
        {
            using (var db = new StudentTestApiContext())
            {
                var stud = db.Students.Where(x => x.Id == id).FirstOrDefault();
                if (stud != null)
                {
                    db.Students.Remove(stud);
                    db.SaveChanges();
                    return Redirect("https://localhost:44314/api/Values");
                }
                return NotFound();
            }
        }
    }
}
