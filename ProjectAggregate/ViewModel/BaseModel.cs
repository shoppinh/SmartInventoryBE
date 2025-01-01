using System.Text.Json.Serialization;

namespace SmartInventoryBE.ProjectAggregate.ViewModel
{
    public class BaseModel
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; }
    }
}
