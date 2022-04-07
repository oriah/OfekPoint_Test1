using Microsoft.AspNetCore.Mvc;
using Sisma.Project1.Logic.Business;
using Sisma.Project1.Logic.Data;
using Sisma.Project1.Logic.Models;

namespace Sisma.Project1.Web.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        public StudentsController()
        {

        }



        //Student (L)CRUD

        public IActionResult GetAll()
        {
            var x = new GeneralBL.CRUD();
            var x0 = x.Students.GetAll();

            return Ok(x0);
        }
        public IActionResult Get(Guid classId)
        {
            var x = new GeneralBL.CRUD();
            var x0 = x.Students.Get(classId);

            return Ok(x0);
        }
        public IActionResult Create(Student item)
        {
            var x = new GeneralBL.CRUD();
            x.Students.Create(item);

            return Ok();
        }
        public IActionResult Update(Student item)
        {
            var x = new GeneralBL.CRUD();
            x.Students.Update(item);

            return Ok();
        }
        public IActionResult Delete(Guid classId)
        {
            var x = new GeneralBL.CRUD();
            x.Students.Delete(classId);

            return Ok();
        }


    }
}