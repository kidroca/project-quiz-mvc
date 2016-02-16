namespace QuizProjectMvc.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class LoginViewModel
    {
        [Required]
        [MinLength(ModelConstraints.UsernameMinLength, ErrorMessage = StatusMessages.MinimumLength)]
        [MaxLength(ModelConstraints.UsernameMaxLength, ErrorMessage = StatusMessages.MaximumLength)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
