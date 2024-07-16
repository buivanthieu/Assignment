using Assigment_2.Data;
using Assigment_2.Interface;
using Assigment_2.Model;
using Microsoft.EntityFrameworkCore;

namespace Assigment_2.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }
        public async Task<Student?> GetStudentById(int id)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
        }
        public void AddStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }
        public void UpdateStudent(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
        }
        public void DeleteStudent(Student student)
        {
            _context.Students.Remove(student);
            _context.SaveChanges();
        }


    }
}