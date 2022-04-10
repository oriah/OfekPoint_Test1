using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sisma.Project1.BL.Business;
using Sisma.Project1.DAL.Data;
using Sisma.Project1.Shared.Exceptions;
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
            try
            {
                var res = _blAdmin.StudentInClasses.GetAll();

                return Ok(res.Select(item => item.Map<StudentInClassDTO>(_mapper)));

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
                var res = _blAdmin.StudentInClasses.Get(studentId);

                return Ok(res.Map<StudentInClassDTO>(_mapper));

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
        [HttpPost]
        public IActionResult Create(StudentInClassDTO item)
        {
            try
            {
                StudentInClass obj;
                _blAdmin.StudentInClasses.Create((obj = item.Map<StudentInClass>(_mapper)));

                return CreatedAtAction(null, obj.Map<StudentInClassDTO>(this._mapper));
            }
            catch (SismaException sexc) when (sexc.Type == SismaExceptionTypes.ObjectNotFound)
            {
                return BadRequest(sexc.Message);
            }
            catch (SismaException sexc) when (sexc.Type == SismaExceptionTypes.StudentSchoolMismatch)
            {
                return BadRequest("The specified student's school is not the same as the school of the given class");
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
                StudentInClass obj;
                _blAdmin.StudentInClasses.Update((obj = item.Map<StudentInClass>(_mapper)));

                return Ok(obj.Map<StudentInClassDTO>(this._mapper));
            }
            catch (SismaException sexc) when (sexc.Type == SismaExceptionTypes.ObjectNotFound)
            {
                return BadRequest(sexc.Message);
            }
            catch (SismaException sexc) when (sexc.Type == SismaExceptionTypes.StudentSchoolMismatch)
            {
                return BadRequest("The specified student's school is not the same as the school of the given class");
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
                _blAdmin.StudentInClasses.Delete(studentId);

                return Ok();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


    }
}