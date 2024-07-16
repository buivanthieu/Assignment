using Assigment_2.Model;

namespace Assigment_2.Interface
{
    public interface IStudentRepository
    {
        public Task<List<Student>> GetAllStudentsAsync();
        public Task<Student> GetStudentById(int id);
        public void AddStudent(Student student);
        public void UpdateStudent(Student student);
        public void DeleteStudent(Student student);
        
    }
}