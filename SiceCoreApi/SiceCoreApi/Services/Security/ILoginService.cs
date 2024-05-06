using SiceCoreApi.Models.Responses;
using SiceCoreApi.Models.Security;

namespace SiceCoreApi.Services.Security
{
    public interface ILoginService
    {
        Task<ServiceResult<string>> LoginAccount(LoginDTO loginDTO);
    }
}
