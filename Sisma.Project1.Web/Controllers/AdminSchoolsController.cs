using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sisma.Project1.BL.Business;
using Sisma.Project1.DAL.Data;
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
            var res = _blAdmin.Schools.GetAll();

            return Ok(res.Select(item=>item.Map<SchoolDTO>(_mapper)));
        }
        [HttpGet("get")]
        public IActionResult Get(Guid schoolId)
        {
            var res = _blAdmin.Schools.Get(schoolId);

            return Ok(res.Map<SchoolDTO>(_mapper));
        }
        [HttpPost]
        public IActionResult Create(SchoolDTO item)
        {
            _blAdmin.Schools.Create(item.Map<School>(_mapper));

            return Ok();
        }
        [HttpPut]
        public IActionResult Update(SchoolDTO item)
        {
            _blAdmin.Schools.Update(item.Map<School>(_mapper));

            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(Guid schoolId)
        {
            _blAdmin.Schools.Delete(schoolId);

            return Ok();
        }


    }
}