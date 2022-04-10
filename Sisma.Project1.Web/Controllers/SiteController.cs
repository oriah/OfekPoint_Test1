using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sisma.Project1.BL.Business;
using Sisma.Project1.DAL.Data;
using Sisma.Project1.Shared.Exceptions;
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




        /// <summary>
        /// Returns all schools in the current system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getAllSchools")]
        public IActionResult GetAllSchools()
        {
            try
            {
                var res = _blAdmin.Schools.GetAll();
                return Ok(res.Select(item => item.Map<SchoolDTO>(_mapper)));
            }
            catch (SismaException sexc)
            {
                Console.WriteLine(sexc);
                return BadRequest(sexc.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);   //Todo LOG for the current layer (=PL)
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schoolId">school id (=referential unique ID)</param>
        /// <param name="forceDelete"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deleteSchool")]
        public IActionResult DeleteSchool(Guid schoolId, bool forceDelete)   // – if forceDelete is false – only delete school which has no users/classrooms attached. if forceDelete is true – do whatever it takes to delete the school.
        {
            try
            {
                if (schoolId == default)
                {
                    return BadRequest("'schoolId' parameter cannot be null");
                }

                bool deleteAvoided;
                _bl.DeleteSchool(schoolId, forceDelete);

                return Ok();
            }
            catch (SismaException sexc) when (sexc.Type == SismaExceptionTypes.ObjectDependencyExists)
            {
                return BadRequest("The school has associated classes and/or students while 'forceDelete' was specified as 'false'");
            }
            catch (SismaException sexc)
            {
                Console.WriteLine(sexc);
                return BadRequest(sexc.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);   //Todo LOG for the current layer (=PL)
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classId">class id (=referential unique ID)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getClassStudentsByClassId")]
        public IActionResult GetClassStudentsByClassId(Guid classId) // – list of all active students of a class
        {
            try
            {
                if (classId == default)
                {
                    return BadRequest("'classId' parameter cannot be null");
                }

                List<Student> res = _bl.GetClassStudentsByClassId(classId);
                return Ok(res.Select(item => item.Map<StudentDTO>(_mapper)));
            }
            catch (SismaException sexc)
            {
                Console.WriteLine(sexc);
                return BadRequest(sexc.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);   //Todo LOG for the current layer (=PL)
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentId">class id (=referential unique ID)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getAllStudentClasses")]
        public IActionResult GetAllStudentClasses(Guid studentId) // – all active classes that the user has.
        {
            try
            {
                if (studentId == default)
                {
                    return BadRequest("'studentId' parameter cannot be null");
                }

                List<Class> res = _bl.GetAllStudentClasses(studentId);
                return Ok(res.Select(item => item.Map<ClassDTO>(_mapper)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);   //Todo LOG for the current layer (=PL)
                throw;
            }
        }

        [HttpPost]
        [Route("createStudent")]
        public IActionResult CreateStudent(StudentDTO item) //    -create a student with no classes, but of course he has school
        {
            try
            {
                Student obj;
                _blAdmin.Students.Create((obj = item.Map<Student>(_mapper)));

                return CreatedAtAction(null, obj.Map<StudentDTO>(this._mapper));
            }
            //catch (SismaException sexc) when (sexc.Type == SismaExceptionTypes.ObjectNotFound)
            //{
            //    return BadRequest(sexc.Message);
            //}
            catch (SismaException sexc) when (sexc.Type == SismaExceptionTypes.StudentSchoolMismatch)
            {
                return BadRequest("The specified student's school is not the same as the school of the given class");
            }
            catch (SismaException sexc)
            {
                Console.WriteLine(sexc);
                return BadRequest(sexc.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);   //Todo LOG for the current layer (=PL)
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentId">student id (=referential unique ID)</param>
        /// <param name="classId">class id (=referential unique ID)</param>
        /// <returns></returns>
        [HttpPut]
        [Route("addStudentToClass")]
        public IActionResult AddStudentToClass(Guid studentId, Guid classId)
        {
            try
            {
                if (studentId == default)
                {
                    return BadRequest("'studentId' parameter cannot be null");
                }
                else if (classId == default)
                {
                    return BadRequest("'classId' parameter cannot be null");
                }

                _bl.AddStudentToClass(studentId, classId);

                return Ok();
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
            catch (Exception e)
            {
                Console.WriteLine(e);   //Todo LOG for the current layer (=PL)
                throw;
            }
        }


        //<one endpoint from your imagination>
        [HttpDelete]
        [Route("deleteAllEmptyClasses")]
        public IActionResult DeleteAllEmptyClasses()
        {
            try
            {
                _bl.DeleteAllEmptyClasses();

                return Ok();
            }
            catch (SismaException sexc)
            {
                Console.WriteLine(sexc);
                return BadRequest(sexc.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);   //Todo LOG for the current layer (=PL)
                throw;
            }
        }




    }
}