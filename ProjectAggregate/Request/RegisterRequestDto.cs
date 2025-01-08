using System.ComponentModel.DataAnnotations;

namespace SmartInventoryBE.ProjectAggregate.Request
{
    public class RegisterRequestDto
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
