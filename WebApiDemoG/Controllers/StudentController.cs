using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiDemoG.Dtos;
using WebApiDemoG.Entities;
using WebApiDemoG.Models;
using WebApiDemoG.Services.Abstract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiDemoG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/<StudentController>
        //[HttpGet]
        //public IEnumerable<StudentDto> Get()
        //{
        //    var items = _studentService.GetAll();
        //    var dataToReturn = items.Select(a =>
        //    {
        //        return new StudentDto
        //        {
        //            Id = a.Id,
        //            Age = a.Age,
        //            Fullname = a.Fullname,
        //            Score = a.Score,
        //            SeriaNo = a.SeriaNo
        //        };
        //    });
        //    return dataToReturn;
        //}

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public List<StudentModel> Get()
        {
            /*
             * $.ajax({
         url: "http://localhost:8080/Student",
         type: 'GET',
         // Fetch the stored token from localStorage and set in the header
         headers: {"Authorization": "basic "+token}
});
             */
            var items = _studentService.GetAll().ToList();
            var dataToReturn = items.Select(a =>
            {
                return new StudentModel
                {
                    Id = a.Id,
                    Age = a.Age,
                    Fullname = a.Fullname,
                    Score = a.Score,
                    SeriaNo = a.SeriaNo
                };
            }).ToList();
            return dataToReturn;
        }


        // GET: api/<StudentController>
        [HttpGet("Best")]
        public IEnumerable<StudentDto> GetBestStudents()
        {
            var items = _studentService.GetAll().Where(s=>s.Score>90);
            var dataToReturn = items.Select(a =>
            {
                return new StudentDto
                {
                    Id = a.Id,
                    Age = a.Age,
                    Fullname = a.Fullname,
                    Score = a.Score,
                    SeriaNo = a.SeriaNo
                };
            });
            return dataToReturn;
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public StudentModel Get(int id)
        {
            var item=_studentService.Get(id);
            var dataToReturn = new StudentModel
            {
                Id = item.Id,
                SeriaNo = item.SeriaNo,
                Age = item.Age,
                Fullname = item.Fullname,
                Score = item.Score
            };
            return dataToReturn;
        }

        // POST api/<StudentController>
        [HttpPost]
        public IActionResult Post([FromBody] StudentModel value)
        {
            try
            {
                var obj = new Student
                {
                    Age = value.Age,
                    Score = value.Score,
                    Fullname = value.Fullname,
                    SeriaNo = value.SeriaNo
                };
                _studentService.Add(obj);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] StudentDto value)
        {
            try
            {
                var item = _studentService.Get(id);
                item.SeriaNo = value.SeriaNo;
                item.Age = value.Age;
                item.Fullname = value.Fullname;
                item.Score = value.Score;
                _studentService.Update(item);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _studentService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
