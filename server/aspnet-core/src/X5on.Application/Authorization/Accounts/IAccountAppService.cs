using System.Threading.Tasks;
using Abp.Application.Services;
using X5on.Authorization.Accounts.Dto;

namespace X5on.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
