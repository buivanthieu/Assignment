using Assigment_02.DTO.Grade;
using Assigment_02.Model;

namespace Assigment_02.Interface
{
    public interface IGradeRepository
    {
        public Task<List<Grade>> GetAllGrade();
        public Task<List<GradeDTO>> GetAllGradeDTO();
        public Task<Grade> GetGradeById(int gradeId);
        public void AddGrade(Grade grade);
        public void UpdateGrade(Grade grade);
        public void DeleteGrade(Grade grade);

        public Task<bool> GradeExists(int id);
    }
}