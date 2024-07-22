namespace Assigment_02.DTO.Grade
{
    public class GradeDTOIfGetById
    {
        public int Id {get; set;}

        public string? Name {get; set;}
        public ICollection<StudentDTO>? StudentDTOs {get; set;}
    }
}