using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SiceCoreApi.Context;
using SiceCoreApi.Models.Responses;
using SiceCoreApi.Models.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace SiceCoreApi.Services.Security
{
    public class LoginService : ILoginService
    {
        private readonly SICECoreContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration config;

        public LoginService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration config,
            SICECoreContext context
        )
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.config = config;
        }

        public async Task<ServiceResult<string>> LoginAccount(LoginDTO loginDTO)
        {
            try
            {
                if (loginDTO == null)
                    return ServiceResult<string>.ErrorResult(new[] { $"Credenciales incorrectas." });

                string usuario = loginDTO.UserName ?? string.Empty;
                string password = loginDTO.Password ?? string.Empty;

                if (!string.IsNullOrEmpty(loginDTO.RefreshToken))
                {
                    string jsonString = Base64Decode(loginDTO.RefreshToken);

                    LoginDTO credenciales = JsonSerializer.Deserialize<LoginDTO>(jsonString);
                    usuario = credenciales.UserName;
                    password = credenciales.Password;
                }

                var getUser = await userManager.FindByNameAsync(usuario);
                if (getUser is null)
                    return ServiceResult<string>.ErrorResult(new[] { $"Usuario/Password incorrectos." });

                bool checkUserPasswords = await userManager.CheckPasswordAsync(getUser, password);
                if (!checkUserPasswords)
                    return ServiceResult<string>.ErrorResult(new[] { $"Usuario/Password incorrectos." });

                var getUserRole = await userManager.GetRolesAsync(getUser);
                var userSession = new UserSession(getUser.Id, getUser.UserName, getUser.Email, getUserRole);
                string token = await GenerateTokenAsync(userSession, loginDTO);

                return ServiceResult<string>.SuccessResult(token);
            }
            catch (Exception ex)
            {
                return ServiceResult<string>.ErrorResult(new[] { $"{ex.Message}" });
            }
        }

        private async Task<string> GenerateTokenAsync(UserSession user, LoginDTO loginDTO)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var menu = await context.UvwMenus.Where(c => c.UserId == user.Id).ToListAsync();
            var resfreshToken = Base64Encode(JsonSerializer.Serialize(loginDTO));

            var userClaims = new[]
            {
                new Claim("id", user.Id),
                new Claim("username", user.UserName),
                new Claim("email", user.Email ?? ""),
                new Claim("roles", user.Role != null ? JsonSerializer.Serialize(user.Role) : null),
                new Claim("menu", menu != null ? JsonSerializer.Serialize(menu) : null),
                new Claim("refreshtoken", resfreshToken)
            };

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(config["Jwt:MinuteToExpires"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
