using Microsoft.AspNetCore.Mvc;
using Sisma.Project1.Logic.Business;
using Sisma.Project1.Logic.Data;
using Sisma.Project1.Logic.Models;

namespace Sisma.Project1.Web.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {

        public SchoolsController()
        {

        }



        //School (L)CRUD

        public IActionResult GetAll()
        {
            var x = new GeneralBL.CRUD();
            var x0 = x.Schools.GetAll();

            return Ok(x0);
        }
        public IActionResult Get(Guid classId)
        {
            var x = new GeneralBL.CRUD();
            var x0 = x.Schools.Get(classId);

            return Ok(x0);
        }
        public IActionResult Create(School item)
        {
            var x = new GeneralBL.CRUD();
            x.Schools.Create(item);

            return Ok();
        }
        public IActionResult Update(School item)
        {
            var x = new GeneralBL.CRUD();
            x.Schools.Update(item);

            return Ok();
        }
        public IActionResult Delete(Guid classId)
        {
            var x = new GeneralBL.CRUD();
            x.Schools.Delete(classId);

            return Ok();
        }


    }
}