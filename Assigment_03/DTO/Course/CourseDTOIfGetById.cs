namespace Assigment_02.DTO.Course
{
    public class CourseDTOIfGetById
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public ICollection<StudentDTO>? StudentDTOs { get; set; }
    }
}