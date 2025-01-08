namespace SmartInventoryBE.ProjectAggregate.Response
{
    public class UserDto
    {
        public string Id { get; set; }
        public string? Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string Token { get; set; } = string.Empty;
    }
}
