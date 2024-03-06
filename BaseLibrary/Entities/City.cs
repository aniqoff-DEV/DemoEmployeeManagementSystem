using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class City : BaseEntity
    {
        public Country? Country { get; set; }
        public int CountryId { get; set; }
        [JsonIgnore]
        public List<Town>? Towns { get; set; }
    }
}
