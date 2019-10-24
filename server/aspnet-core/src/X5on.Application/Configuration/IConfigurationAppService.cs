using System.Threading.Tasks;
using X5on.Configuration.Dto;

namespace X5on.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
