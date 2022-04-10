using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.WebEncoders.Testing;
using Sisma.Project1.BL.Business;
using Sisma.Project1.DAL.Data;
using Sisma.Project1.Shared.Exceptions;
using Sisma.Project1.Web.Helpers;
using Sisma.Project1.Web.Models;

namespace Sisma.Project1.Web.Controllers
{
    [Route("api/admin/classes")]
    [ApiController]
    public class AdminClassesController : ControllerBase
    {

        private IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly SismaBL.CRUD _blAdmin;
        private readonly ILogger _logger;

        public AdminClassesController(IMapper mapper, IConfiguration config, SismaBL.CRUD sismaBLCRUD, ILogger<AdminClassesController> logger)
        {
            this._mapper = mapper;
            _config = config;
            _blAdmin = sismaBLCRUD;
            _logger = logger;
        }



        //Class (L)CRUD

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            try
            {
                var res = _blAdmin.Classes.GetAll();

                return Ok(res.Select(item => item.Map<ClassDTO>(_mapper)));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        [HttpGet("get")]
        public IActionResult Get(Guid classId)
        {
            try
            {
                var res = _blAdmin.Classes.Get(classId);

                return Ok(res.Map<ClassDTO>(_mapper));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        [HttpPost]
        public IActionResult Create(ClassDTO item)
        {
            try
            {
                Class obj;
                _blAdmin.Classes.Create((obj = item.Map<Class>(_mapper)));

                return CreatedAtAction(null, obj.Map<ClassDTO>(this._mapper));
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
        public IActionResult Update(ClassDTO item)
        {
            try
            {
                Class obj;
                _blAdmin.Classes.Update((obj = item.Map<Class>(_mapper)));

                return Ok(obj.Map<ClassDTO>(this._mapper));
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
        public IActionResult Delete(Guid classId)
        {
            try
            {
                _blAdmin.Classes.Delete(classId);

                return Ok();
            }
            catch (SismaException sexc) when (sexc.Type == SismaExceptionTypes.ObjectDependencyExists)
            {
                return BadRequest("The class has associated student-related records");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


    }
}