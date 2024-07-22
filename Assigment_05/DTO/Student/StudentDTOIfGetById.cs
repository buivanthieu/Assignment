namespace Assigment_02.DTO.Student
{
    public class StudentDTOIfGetById
    {
        public int id { get; set; }
        public string? Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int GradeId { get; set; }
        public ICollection<CourseDTO>? CourseDTOs {get; set;}
    }
}