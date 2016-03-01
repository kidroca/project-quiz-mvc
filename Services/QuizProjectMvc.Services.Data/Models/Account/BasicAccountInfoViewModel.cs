namespace QuizProjectMvc.Services.Data.Models.Account
{
    using System.ComponentModel.DataAnnotations;
    using Common;
    using QuizProjectMvc.Data.Models;
    using QuizProjectMvc.Web.Infrastructure.Mapping;

    public class BasicAccountInfoViewModel : IMapFrom<User>, IMapTo<User>
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

        [MinLength(ModelConstraints.DescriptionMinLength, ErrorMessage = StatusMessages.MinimumLength)]
        [MaxLength(ModelConstraints.DescriptionMaxLength, ErrorMessage = StatusMessages.MaximumLength)]
        public string Bio { get; set; }

        // Todo chage for file upload
        [Display(Name = "Avatar Url")]
        [DataType(DataType.ImageUrl)]
        [RegularExpression(ModelConstraints.AvatarPathPattern)]
        [MinLength(ModelConstraints.UrlMinLength, ErrorMessage = StatusMessages.MinimumLength)]
        [MaxLength(ModelConstraints.UrlMaxLength, ErrorMessage = StatusMessages.MaximumLength)]
        public string AvatarUrl { get; set; }
    }
}
