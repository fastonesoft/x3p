using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using X5on.MultiTenancy;

namespace X5on.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}
