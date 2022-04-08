using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
            var res = _blAdmin.Classes.GetAll();

            return Ok(res.Select(item => item.Map<ClassDTO>(_mapper)));
        }
        [HttpGet("get")]
        public IActionResult Get(Guid classId)
        {
            var res = _blAdmin.Classes.Get(classId);

            return Ok(res.Map<ClassDTO>(_mapper));
        }
        [HttpPost]
        public IActionResult Create(ClassDTO item)
        {
            _blAdmin.Classes.Create(item.Map<Class>(_mapper));

            return Ok();
        }
        [HttpPut]
        public IActionResult Update(ClassDTO item)
        {
            _blAdmin.Classes.Update(item.Map<Class>(_mapper));

            return Ok();
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