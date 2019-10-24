using System.ComponentModel.DataAnnotations;

namespace X5on.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}