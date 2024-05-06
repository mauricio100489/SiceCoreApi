namespace SiceCoreApi.Models.Security
{
    public record UserSession(string? Id, string? UserName, string? Email, IEnumerable<string>? Role);
}
