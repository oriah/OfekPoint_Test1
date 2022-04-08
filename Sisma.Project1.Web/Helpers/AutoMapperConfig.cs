using AutoMapper;
using Sisma.Project1.Logic.Data;
using Sisma.Project1.Web.Models;

namespace Sisma.Project1.Web.Helpers
{
    public class MyAutoMapperProfiles : Profile
    {
        public MyAutoMapperProfiles()
        {
            // account/register:
            CreateMap<SchoolDTO, School>();    //school: view->db
            CreateMap<School, SchoolDTO>();    //school: db->view
            CreateMap<ClassDTO, Class>();    //class: view->db
            CreateMap<Class, ClassDTO>();    //class: db-> view
            CreateMap<StudentDTO, Student>();    //student: view->db
            CreateMap<Student, StudentDTO>();    //student: db->view
            CreateMap<StudentInClassDTO, StudentInClass>();    //studentInClass: view->db
            CreateMap<StudentInClass, StudentInClassDTO>();    //studentInClass: db-> view

        }
    }

    public static class AutoMapperExtensions
    {
        public static TResult Map<TResult>(this object dbEntity, IMapper autoMapperObj)
        {
            return autoMapperObj.Map<TResult>(dbEntity);
        }
    }
}