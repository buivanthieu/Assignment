using Assigment_02.Data;
using Assigment_02.DTO;
using Assigment_02.Interface;
using Assigment_02.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Assigment_02.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CourseRepository(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<Course>> GetAllCourses(){
            return await _context.Courses.ToListAsync();
        }
        public async Task<List<CourseDTO>> GetAllCourseDTO(){
            var courses = await GetAllCourses();
            return _mapper.Map<List<Course>, List<CourseDTO>>(courses);
        }
        public async Task<Course> GetCourseById(int id){
            return await _context.Courses
                .Include(c => c.StudentCourses)
                .ThenInclude(sc => sc.Student)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public void AddCourse(Course course){
            _context.Courses.Add(course);
            _context.SaveChanges();
        }
        public void UpdateCourse(Course course){
            _context.Courses.Update(course);
            _context.SaveChanges();
        }
        public void DeleteCourse(Course course){
            _context.Courses.Remove(course);
            _context.SaveChanges();
        }

    }
}