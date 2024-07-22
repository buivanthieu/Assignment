using Assigment_02.DTO;
using Assigment_02.Model;

namespace Assigment_02.Interface
{
    public interface IStudentRepository
    {
        public Task<List<Student>> GetAllStudents();  
        public Task<Student> GetStudentById(int id);
        public Task AddStudent(Student student);
        public Task UpdateStudent(Student student);
        public Task DeleteStudent(Student student);    
        public Task AddCourseToStudent(int studentId, int courseId);

    }
}