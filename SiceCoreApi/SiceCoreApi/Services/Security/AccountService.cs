using Microsoft.AspNetCore.Identity;
using SiceCoreApi.Models.Responses;
using SiceCoreApi.Models.Security;

namespace SiceCoreApi.Services.Security
{
    public class AccountService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : IAccountService
    {
        public async Task<ServiceResult<bool>> CreateAccount(UserAccountDTO UserAccountDTO)
        {
            try
            {
                if (UserAccountDTO is null)
                    return ServiceResult<bool>.ErrorResult(new[] { $"Información de usuario incorrectos." });

                //Validating Roles
                foreach (var role in UserAccountDTO.ListIdRole)
                {
                    var checkRole = await roleManager.FindByIdAsync(role);
                    if (checkRole is null)
                        return ServiceResult<bool>.ErrorResult(new[] { $"Rol {role} no existe!" });
                }

                var newUser = new ApplicationUser()
                {
                    Name = UserAccountDTO.Name,
                    Email = UserAccountDTO.Email,
                    PasswordHash = UserAccountDTO.Password,
                    UserName = UserAccountDTO.UserName,
                    PhoneNumber = UserAccountDTO.PhoneNumber
                };

                var user = await userManager.FindByNameAsync(newUser.UserName);
                if (user is not null)
                    return ServiceResult<bool>.ErrorResult(new[] { $"Usuario {newUser.UserName} ya existe registrado!" });

                var createUser = await userManager.CreateAsync(newUser!, UserAccountDTO.Password);
                if (!createUser.Succeeded)
                {
                    List<string> listErrors = new();
                    foreach (var error in createUser.Errors)
                    {
                        listErrors.Add(error.Description);
                    }

                    return ServiceResult<bool>.ErrorResult(listErrors.ToArray());
                }

                //Assign Roles
                foreach (var role in UserAccountDTO.ListIdRole)
                {
                    var checkRole = await roleManager.FindByIdAsync(role);
                    await roleManager.CreateAsync(new IdentityRole() { Name = checkRole.Name });
                    await userManager.AddToRoleAsync(newUser, checkRole.Name);
                }

                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.ErrorResult(new[] { $"{ex.Message}" });
            }
        }

        public async Task<ServiceResult<bool>> UpdateAccount(UserAccountDTO UserAccountDTO)
        {
            try
            {


                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.ErrorResult(new[] { $"{ex.Message}" });
            }
        }
    }
}
