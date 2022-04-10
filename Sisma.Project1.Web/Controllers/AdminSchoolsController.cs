using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sisma.Project1.BL.Business;
using Sisma.Project1.DAL.Data;
using Sisma.Project1.Shared.Exceptions;
using Sisma.Project1.Web.Helpers;
using Sisma.Project1.Web.Models;

namespace Sisma.Project1.Web.Controllers
{
    [Route("api/admin/schools")]
    [ApiController]
    public class AdminSchoolsController : ControllerBase
    {

        private IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly SismaBL.CRUD _blAdmin;
        private readonly ILogger _logger;

        public AdminSchoolsController(IMapper mapper, IConfiguration config, SismaBL.CRUD sismaBLCRUD, ILogger<AdminSchoolsController> logger)
        {
            this._mapper = mapper;
            _config = config;
            _blAdmin = sismaBLCRUD;
            _logger = logger;
        }



        //School (L)CRUD

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            try
            {
                var res = _blAdmin.Schools.GetAll();

                return Ok(res.Select(item => item.Map<SchoolDTO>(_mapper)));

            }
            catch (SismaException sexc)
            {
                //Console.WriteLine(ex);
                return BadRequest(sexc.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        [HttpGet("get")]
        public IActionResult Get(Guid schoolId)
        {
            try
            {
                var res = _blAdmin.Schools.Get(schoolId);

                return Ok(res.Map<SchoolDTO>(_mapper));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        [HttpPost]
        public IActionResult Create(SchoolDTO item)
        {
            try
            {
                School obj;
                _blAdmin.Schools.Create((obj = item.Map<School>(_mapper)));

                return CreatedAtAction(null, obj.Map<SchoolDTO>(this._mapper));
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
        public IActionResult Update(SchoolDTO item)
        {
            try
            {
                School obj;
                _blAdmin.Schools.Update((obj = item.Map<School>(_mapper)));

                return Ok(obj.Map<SchoolDTO>(this._mapper));
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
        public IActionResult Delete(Guid schoolId)
        {
            try
            {
                _blAdmin.Schools.Delete(schoolId);

                return Ok();
            }
            catch (SismaException sexc) when (sexc.Type == SismaExceptionTypes.ObjectDependencyExists)
            {
                return BadRequest("The school has associated Schools and/or students");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


    }
}