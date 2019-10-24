using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using X5on.Roles.Dto;
using X5on.Users.Dto;

namespace X5on.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
