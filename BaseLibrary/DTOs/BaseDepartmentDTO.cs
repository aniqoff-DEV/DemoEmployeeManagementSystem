using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs
{
    public class BaseDepartmentDTO
    {
        public int Id { get; set; }

        [Required, MaxLength(50), MinLength(3)]
        public string? Name { get; set; }
    }
}
