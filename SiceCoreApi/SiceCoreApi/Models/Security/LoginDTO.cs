using System.ComponentModel.DataAnnotations;

namespace SiceCoreApi.Models.Security
{
    public class LoginDTO
    {
        [Required]
        public string? UserName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; } = string.Empty;

        public string? RefreshToken { get; set; } = string.Empty;
    }
}
