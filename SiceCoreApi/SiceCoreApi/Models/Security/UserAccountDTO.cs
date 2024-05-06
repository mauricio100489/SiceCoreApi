using System.ComponentModel.DataAnnotations;

namespace SiceCoreApi.Models.Security
{
    public class UserAccountDTO
    {
        public string? Id { get; set; } = string.Empty;

        [Required]
        public string? UserName { get; set; } = string.Empty;

        [Required]
        public string? Name { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string? ConfirmPassword { get; set; } = string.Empty;

        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; } = string.Empty;

        [Required]
        public IEnumerable<string>? ListIdRole { get; set; }
    }
}
