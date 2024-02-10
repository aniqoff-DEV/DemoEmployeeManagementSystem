using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities
{
    public class OtherBaseEntity
    {
        public int Id { get; set; }
        [Required] 
        public string CivildId { get; set; } = string.Empty;
        [Required]
        public string FileNumber { get; set; } = string.Empty;
        public string? Other { get; set; }
    }
}
