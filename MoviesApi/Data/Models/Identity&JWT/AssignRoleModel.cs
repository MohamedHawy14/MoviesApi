namespace MoviesApi.Data.Models
{
    public class AssignRoleModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
