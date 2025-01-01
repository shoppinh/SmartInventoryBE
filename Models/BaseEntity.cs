using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SmartInventoryBE.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
