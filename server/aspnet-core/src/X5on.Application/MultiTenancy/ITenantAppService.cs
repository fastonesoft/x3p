using Abp.Application.Services;
using Abp.Application.Services.Dto;
using X5on.MultiTenancy.Dto;

namespace X5on.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

