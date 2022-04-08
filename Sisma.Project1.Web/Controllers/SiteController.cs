using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sisma.Project1.Logic.Business;
using Sisma.Project1.Logic.Data;
using Sisma.Project1.Web.Helpers;
using Sisma.Project1.Web.Models;

namespace Sisma.Project1.Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class SiteController : ControllerBase
    {

        private IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly SismaBL _bl;
        private readonly SismaBL.CRUD _blAdmin;
        private readonly ILogger _logger;

        public SiteController(IMapper mapper, IConfiguration config, SismaBL sismaBL, SismaBL.CRUD sismaBLCRUD, ILogger<SiteController> logger)
        {
            this._mapper = mapper;
            _config = config;
            _bl = sismaBL;
            _blAdmin = sismaBLCRUD;
            _logger = logger;
        }





        [HttpGet]
        [Route("getAllSchools")]
        public IActionResult GetAllSchools()
        {
            var res = _blAdmin.Schools.GetAll();
            return Ok(res.Select(item => item.Map<SchoolDTO>(_mapper)));
        }

        [HttpDelete]
        [Route("deleteSchool")]
        public IActionResult DeleteSchool(Guid schoolId, bool forceDelete) // – if forceDelete is false – only delete school which has no users/classrooms attached. if forceDelete is true – do whatever it takes to delete the school.
        {
            _bl.DeleteSchool(schoolId, forceDelete);

            return Ok();
        }

        [HttpGet]
        [Route("getClassStudentsByClassId")]
        public IActionResult GetClassStudentsByClassId(Guid classId) // – list of all active students of a class
        {
            List<Student> res = _bl.GetClassStudentsByClassId(classId);
            return Ok(res.Select(item => item.Map<StudentDTO>(_mapper)));
        }

        [HttpGet]
        [Route("getAllStudentClasses")]
        public IActionResult GetAllStudentClasses(Guid studentId) // – all active classes that the user has.
        {
            List<Class> res = _bl.GetAllStudentClasses(studentId);
            return Ok(res.Select(item => item.Map<ClassDTO>(_mapper)));
        }

        [HttpPost]
        [Route("createStudent")]
        public IActionResult CreateStudent(StudentDTO item) //    -create a student with no classes, but of course he has school
        {
            _blAdmin.Students.Create(item.Map<Student>(_mapper));

            return Ok();
        }

        [HttpPut]
        [Route("addStudentToClass")]
        public IActionResult AddStudentToClass(Guid studentId, Guid classId)
        {
            _bl.AddStudentToClass(studentId, classId);

            return Ok();
        }


        //<one endpoint from your imagination>
        [HttpDelete]
        [Route("deleteAllEmptyClasses")]
        public IActionResult DeleteAllEmptyClasses()
        {
            _bl.DeleteAllEmptyClasses();

            return Ok();
        }




    }
}