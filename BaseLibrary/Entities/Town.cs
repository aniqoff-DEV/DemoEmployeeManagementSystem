using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class Town : BaseEntity
    {
        [JsonIgnore]
        public List<Employee>? Employees { get; set; }
        public City? City { get; set; }
        public int CityId { get; set; }
    }
}
