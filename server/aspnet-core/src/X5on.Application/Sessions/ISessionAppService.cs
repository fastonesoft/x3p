using System.Threading.Tasks;
using Abp.Application.Services;
using X5on.Sessions.Dto;

namespace X5on.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
