using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sisma.Project1.BL.Business;
using Sisma.Project1.DAL.Data;
using Sisma.Project1.Shared.Exceptions;
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
            try
            {
                var res = _blAdmin.Students.GetAll();

                return Ok(res.Select(item => item.Map<StudentDTO>(_mapper)));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        [HttpGet("get")]
        public IActionResult Get(Guid studentId)
        {
            try
            {
                var res = _blAdmin.Students.Get(studentId);

                return Ok(res.Map<StudentDTO>(_mapper));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        [HttpPost]
        public IActionResult Create(StudentDTO item)
        {
            try
            {
                Student obj;
                _blAdmin.Students.Create((obj = item.Map<Student>(_mapper)));

                return CreatedAtAction(null, obj.Map<StudentDTO>(this._mapper));
            }
            catch (SismaException sexc)
            {
                Console.WriteLine(sexc);
                return BadRequest(sexc.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        [HttpPut]
        public IActionResult Update(StudentDTO item)
        {
            try
            {
                Student obj;
                _blAdmin.Students.Update((obj = item.Map<Student>(_mapper)));

                return Ok(obj.Map<StudentDTO>(this._mapper));
            }
            catch (SismaException sexc)
            {
                Console.WriteLine(sexc);
                return BadRequest(sexc.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        [HttpDelete]
        public IActionResult Delete(Guid studentId)
        {
            try
            {
                _blAdmin.Students.Delete(studentId);

                return Ok();

            }
            catch (SismaException sexc) when (sexc.Type == SismaExceptionTypes.ObjectDependencyExists)
            {
                return BadRequest("The student has associated classes");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


    }
}