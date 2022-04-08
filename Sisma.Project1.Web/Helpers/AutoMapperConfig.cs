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
            CreateMap<SchoolDTO, School>();    //(user)registerModel: view->db
            CreateMap<School, SchoolDTO>();    //(user)registerModel: view->db
            CreateMap<ClassDTO, Class>();    //(user)registerModel: view->db
            CreateMap<Class, ClassDTO>();    //(user)registerModel: view->db
            CreateMap<StudentDTO, Student>();    //(user)registerModel: view->db
            CreateMap<Student, StudentDTO>();    //(user)registerModel: view->db


            //samples::
            //CreateMap<UserEntity, User>()
            //  .ForMember(dest => dest.Gender, source => source.MapFrom(src => src.Gender.ToString()))
            //  .ForMember(dest => dest.FirstName, source => source.MapFrom(src => src.FirstName.ToUpper()))
            //  .ForMember(dest => dest.LastName, source => source.MapFrom(src => src.LastName.ToUpper()))
            //  .ForMember(dest => dest.SIN, source => source.MapFrom(src => src.Id));
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