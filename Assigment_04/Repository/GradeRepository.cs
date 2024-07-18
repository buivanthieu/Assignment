using Assigment_02.DTO;
using Assigment_02.Interface;
using Assigment_02.Model;
using Assigment_02.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Assigment_02.DTO.Grade;

namespace Assigment_02.Repository
{
    public class GradeRepository : IGradeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GradeRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Grade>> GetAllGrade(){
            return await _context.Grades.ToListAsync();
        }
        public async Task<Grade?> GetGradeById(int id){
            return await _context.Grades
            .Include(g => g.Students) 
            .FirstOrDefaultAsync(g => g.Id == id);
        }
        public void AddGrade(Grade grade){
            _context.Grades.Add(grade);
            _context.SaveChanges();
        }
        public void UpdateGrade(Grade grade){
            _context.Grades.Update(grade);
            _context.SaveChanges();
        }
        public void DeleteGrade(Grade grade){
            _context.Grades.Remove(grade);
            _context.SaveChanges();
        }
         
        public async Task<List<GradeDTO>> GetAllGradeDTO(){
            var grades = await GetAllGrade();
            return _mapper.Map<List<Grade>, List<GradeDTO>>(grades);
        }

        public Task<bool> GradeExists(int id){
            return  _context.Grades.AnyAsync(e => e.Id == id);
        }
    }
}