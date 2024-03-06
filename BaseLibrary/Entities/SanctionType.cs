using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class SanctionType : BaseEntity
    {
        [JsonIgnore]
        public List<Sanction>? Sanctions { get; set; }
    }
}
