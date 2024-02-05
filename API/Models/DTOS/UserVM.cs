namespace API.Models.DTOS
{
    public class UserVM
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? ProfilePic { get; set; }
        public DateTimeOffset DateCreated { get; set; }
    }
}
