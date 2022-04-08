using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sisma.Project1.Logic.Business;
using Sisma.Project1.Logic.Data;
using Sisma.Project1.Web.Helpers;
using Sisma.Project1.Web.Models;

namespace Sisma.Project1.Web.Controllers
{
    [Route("api/admin/students")]
    [ApiController]
    public class AdminStudentsController : ControllerBase
    {

        private IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly SismaBL.CRUD _blAdmin;
        private readonly ILogger _logger;

        public AdminStudentsController(IMapper mapper, IConfiguration config, SismaBL.CRUD sismaBLCRUD, ILogger<AdminStudentsController> logger)
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
            var res = _blAdmin.Students.GetAll();

            return Ok(res.Select(item => item.Map<StudentDTO>(_mapper)));
        }
        [HttpGet("get")]
        public IActionResult Get(Guid studentId)
        {
            var res = _blAdmin.Students.Get(studentId);

            return Ok(res.Map<StudentDTO>(_mapper));
        }
        [HttpPost]
        public IActionResult Create(StudentDTO item)
        {
            _blAdmin.Students.Create(item.Map<Student>(_mapper));

            return Ok();
        }
        [HttpPut]
        public IActionResult Update(StudentDTO item)
        {
            _blAdmin.Students.Update(item.Map<Student>(_mapper));

            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(Guid studentId)
        {
            _blAdmin.Students.Delete(studentId);

            return Ok();
        }


    }
}