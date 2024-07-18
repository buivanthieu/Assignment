using Assigment_02.DTO;
using Assigment_02.Model;

namespace Assigment_02.Interface
{
    public interface ICourseRepository
    {
         public Task<List<Course>> GetAllCourses();
         public Task<List<CourseDTO>> GetAllCourseDTO();
         public Task<Course> GetCourseById(int id);
         public void AddCourse(Course course);
         public void UpdateCourse(Course course);
         public void DeleteCourse(Course course);
    }
}