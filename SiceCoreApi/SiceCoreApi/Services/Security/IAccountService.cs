using SiceCoreApi.Models.Responses;
using SiceCoreApi.Models.Security;

namespace SiceCoreApi.Services.Security
{
    public interface IAccountService
    {
        Task<ServiceResult<bool>> CreateAccount(UserAccountDTO UserAccountDTO);
    }
}
