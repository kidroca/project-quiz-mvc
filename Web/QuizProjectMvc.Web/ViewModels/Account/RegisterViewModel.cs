namespace QuizProjectMvc.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class RegisterViewModel
    {
        [Display(Name = "Email")]
        [EmailAddress]
        [MinLength(ModelConstraints.UsernameMinLength)]
        [MaxLength(ModelConstraints.UsernameMaxLength)]
        public string Email { get; set; }

        [Display(Name = "Username")]
        [Required]
        [MinLength(ModelConstraints.UsernameMinLength, ErrorMessage = StatusMessages.MinimumLength)]
        [MaxLength(ModelConstraints.UsernameMaxLength, ErrorMessage = StatusMessages.MaximumLength)]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        [MinLength(ModelConstraints.UsernameMinLength, ErrorMessage = StatusMessages.MinimumLength)]
        [MaxLength(ModelConstraints.UsernameMaxLength, ErrorMessage = StatusMessages.MaximumLength)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = StatusMessages.PasswordMismatch)]
        public string ConfirmPassword { get; set; }

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
        [MinLength(ModelConstraints.UrlMinLength, ErrorMessage = StatusMessages.MinimumLength)]
        [MaxLength(ModelConstraints.UrlMaxLength, ErrorMessage = StatusMessages.MaximumLength)]
        public string AvatarUrl { get; set; }
    }
}
