using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class VacationType : BaseEntity
    {
        [JsonIgnore]
        public List<Vacation>? Vacations { get; set; }
    }
}
