using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class Country : BaseEntity
    {
        [JsonIgnore]
        public List<City>? Cities { get; set; }
    }
}
