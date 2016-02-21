namespace QuizProjectMvc.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class AccountEditableDetailsViewModel : BasicAccountInfoViewModel
    {
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
    }
}
