using Assigment_02.DTO;
using Assigment_02.Data;
using Assigment_02.Interface;
using Microsoft.EntityFrameworkCore;
using Assigment_02.Model;
using AutoMapper;

namespace Assigment_02.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public StudentRepository(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<List<Student>> GetAllStudents(){
            return await _context.Students.ToListAsync();
        }
        public async Task<Student?> GetStudentById(int id)
        {
            return await _context.Students
                .Include(s => s.StudentCourses)
                .ThenInclude(sc => sc.Course)
                .FirstOrDefaultAsync(s => s.Id == id);

        }
        // public async void AddStudent(Student student)
        // {
        //     _context.Students.Add(student);
        //     await _context.SaveChangesAsync();
        // }
        public async Task UpdateStudent(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteStudent(Student student)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task AddStudent(Student student)
        {
            
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
                     
        }

        public async Task AddCourseToStudent(int studentId, int courseId)
        {
            var student = await _context.Students
                .Include(s => s.StudentCourses)  // Bao gồm StudentCourses để thêm mới
                .FirstOrDefaultAsync(s => s.Id == studentId);
            
            if (student == null)
            {
                throw new ArgumentException("Student not found");
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == courseId);
            
            if (course == null)
            {
                throw new ArgumentException("Course not found");
            }

            var studentCourse = new StudentCourse
            {
                StudentId = studentId,
                CourseId = courseId
            };

            // Thêm StudentCourse vào DbContext
            _context.StudentCourses.Add(studentCourse);
            await _context.SaveChangesAsync();  // Lưu thay đổi vào cơ sở dữ liệu
        }

    }
}