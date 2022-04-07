using Microsoft.AspNetCore.Mvc;
using Sisma.Project1.Logic.Business;
using Sisma.Project1.Logic.Data;
using Sisma.Project1.Logic.Models;

namespace Sisma.Project1.Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class SiteController : ControllerBase
    {

        public SiteController()
        {

        }




        [HttpGet]
        [Route("getAllSchools")]
        public IActionResult GetAllSchools()
        {
            var x = new GeneralBL.CRUD();
            return Ok(x.Schools.GetAll());
        }
      
        [HttpDelete]
        [Route("deleteSchool")]
        public IActionResult DeleteSchool(Guid schoolId, bool forceDelete) // – if forceDelete is false – only delete school which has no users/classrooms attached. if forceDelete is true – do whatever it takes to delete the school.
        {
            var x = new GeneralBL();
            x.DeleteSchool(schoolId, forceDelete);

            return Ok();
        }

        [HttpGet]
        [Route("getClassStudentsByClassId")]
        public IActionResult GetClassStudentsByClassId(Guid classId) // – list of all active students of a class
        {
            var x = new GeneralBL();
            return Ok(x.GetClassStudentsByClassId(classId));
        }
    
        [HttpGet]
        [Route("getAllStudentClasses")]
        public IActionResult GetAllStudentClasses(Guid studentId) // – all active classes that the user has.
        {
            var x = new GeneralBL();
            return Ok(x.GetAllStudentClasses(studentId));
        }

        [HttpPost]
        [Route("createStudent")]
        public IActionResult CreateStudent(Student item) //    -create a student with no classes, but of course he has school
        {
            var x = new GeneralBL.CRUD();
            x.Students.Create(item);

            return Ok();
        }

        [HttpPut]
        [Route("addStudentToClass")]
        public IActionResult AddStudentToClass(Guid studentId, Guid classId)
        {
            var x = new GeneralBL();
            x.AddStudentToClass(studentId, classId);

            return Ok();
        }


        //<one endpoint from your imagination>
        [HttpDelete]
        [Route("deleteAllEmptyClasses")]
        public IActionResult DeleteAllEmptyClasses()
        {
            var x = new GeneralBL();
            x.DeleteAllEmptyClasses();

            return Ok();
        }







    }
}