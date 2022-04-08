using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sisma.Project1.Logic.Business;
using Sisma.Project1.Logic.Data;
using Sisma.Project1.Web.Helpers;
using Sisma.Project1.Web.Models;

namespace Sisma.Project1.Web.Controllers
{
    [Route("api/admin/studentInClasses")]
    [ApiController]
    public class AdminStudentInClassesController : ControllerBase
    {

        private IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly SismaBL.CRUD _blAdmin;
        private readonly ILogger _logger;

        public AdminStudentInClassesController(IMapper mapper, IConfiguration config, SismaBL.CRUD sismaBLCRUD, ILogger<AdminStudentInClassesController> logger)
        {
            this._mapper = mapper;
            _config = config;
            _blAdmin = sismaBLCRUD;
            _logger = logger;
        }



        //Student (L)CRUD

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var res = _blAdmin.StudentInClasses.GetAll();

            return Ok(res.Select(item => item.Map<StudentInClassDTO>(_mapper)));
        }
        [HttpGet("get")]
        public IActionResult Get(Guid studentId)
        {
            var res = _blAdmin.StudentInClasses.Get(studentId);

            return Ok(res.Map<StudentInClassDTO>(_mapper));
        }
        [HttpPost]
        public IActionResult Create(StudentInClassDTO item)
        {
            _blAdmin.StudentInClasses.Create(item.Map<StudentInClass>(_mapper));

            return Ok();
        }
        [HttpPut]
        public IActionResult Update(StudentDTO item)
        {
            _blAdmin.StudentInClasses.Update(item.Map<StudentInClass>(_mapper));

            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(Guid studentId)
        {
            _blAdmin.StudentInClasses.Delete(studentId);

            return Ok();
        }


    }
}