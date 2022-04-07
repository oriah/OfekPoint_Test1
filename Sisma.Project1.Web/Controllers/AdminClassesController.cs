using Microsoft.AspNetCore.Mvc;
using Sisma.Project1.Logic.Business;
using Sisma.Project1.Logic.Data;
using Sisma.Project1.Logic.Models;

namespace Sisma.Project1.Web.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {

        public ClassesController()
        {

        }



        //Class (L)CRUD

        public IActionResult GetAll()
        {
            var x = new GeneralBL.CRUD();
            var x0 = x.Classes.GetAll();

            return Ok(x0);
        }
        public IActionResult Get(Guid classId)
        {
            var x = new GeneralBL.CRUD();
            var x0 = x.Classes.Get(classId);

            return Ok(x0);
        }
        public IActionResult Create(Class item)
        {
            var x = new GeneralBL.CRUD();
            x.Classes.Create(item);

            return Ok();
        }
        public IActionResult Update(Class item)
        {
            var x = new GeneralBL.CRUD();
            x.Classes.Update(item);

            return Ok();
        }
        public IActionResult Delete(Guid classId)
        {
            var x = new GeneralBL.CRUD();
            x.Classes.Delete(classId);

            return Ok();
        }


    }
}