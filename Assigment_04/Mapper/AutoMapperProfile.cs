using Assigment_02.DTO;
using Assigment_02.DTO.Course;
using Assigment_02.DTO.Grade;
using Assigment_02.DTO.Student;
using Assigment_02.Model;
using AutoMapper;

namespace Assigment_02.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {   
            CreateMap<CreateStudentDTO, Student>();
            CreateMap<StudentDTO, Student>();
            CreateMap<Student, StudentDTO>();
            CreateMap<Student, StudentDTOIfGetById>()
                .ForMember(dest => dest.CourseDTOs, opt =>
                opt.MapFrom(src => src.StudentCourses.Select(sc => sc.Course)));

            CreateMap<CreatGradeDTO, Grade>();
            CreateMap<Grade, GradeDTO>();
            CreateMap<Grade, GradeDTOIfGetById>()
            .ForMember(dest => dest.StudentDTOs, opt =>
                opt.MapFrom(src => src.Students));


            CreateMap<Course, CourseDTO>();
            CreateMap<CreateCourseDTO, Course>();
            CreateMap<Course, CourseDTOIfGetById>()
                .ForMember(dest => dest.StudentDTOs, opt =>    
                opt.MapFrom(src => src.StudentCourses.Select(sc => sc.Student)));

            
        }
    }
}
