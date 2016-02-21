namespace QuizProjectMvc.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;
    using Common;
    using Data.Models;
    using Infrastructure.Mapping;

    public class BasicAccountInfoViewModel : IMapFrom<User>
    {
        [Display(Name = "Email")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [MinLength(ModelConstraints.UsernameMinLength)]
        [MaxLength(ModelConstraints.UsernameMaxLength)]
        public string Email { get; set; }

        [Display(Name = "First Name")]
        [Required]
        [MinLength(ModelConstraints.NameMinLength, ErrorMessage = StatusMessages.MinimumLength)]
        [MaxLength(ModelConstraints.NameMaxLength, ErrorMessage = StatusMessages.MaximumLength)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [MinLength(ModelConstraints.NameMinLength, ErrorMessage = StatusMessages.MinimumLength)]
        [MaxLength(ModelConstraints.NameMaxLength, ErrorMessage = StatusMessages.MaximumLength)]
        public string LastName { get; set; }

        // Todo chage for file upload
        [Display(Name = "Avatar Url")]
        [Url]
        [DataType(DataType.ImageUrl)]
        [MinLength(ModelConstraints.UrlMinLength, ErrorMessage = StatusMessages.MinimumLength)]
        [MaxLength(ModelConstraints.UrlMaxLength, ErrorMessage = StatusMessages.MaximumLength)]
        public string AvatarUrl { get; set; }
    }
}
