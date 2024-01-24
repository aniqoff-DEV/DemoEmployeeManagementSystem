using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        [JsonIgnore]
        public List<Employee>? Employees { get; set; }
    }
}
