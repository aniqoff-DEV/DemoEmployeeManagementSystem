using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities
{
    public class Vacation : OtherBaseEntity
    {
        [Required]
        public DateTime StartDate { get; set; }
        public int NumbersOfDays { get; set; }
        public DateTime EndDate => StartDate.AddDays(NumbersOfDays);

        public VacationType? VacationType { get; set; }
        public int VacationTypeId { get; set; }
    }
}
