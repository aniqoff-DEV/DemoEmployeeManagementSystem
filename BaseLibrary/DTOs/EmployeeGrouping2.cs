using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs
{
    public class EmployeeGrouping2
    {
        [Required]
        public string JobName { get; set; } = string.Empty;
        [Required, Range(1,99999, ErrorMessage = "You must select branch")]
        public int BranchId { get; set; }
        [Required, Range(1, 99999, ErrorMessage = "You must select town")]
        public int TownId { get; set; }
        [Required]
        public string? Other { get; set; }
    }
}
