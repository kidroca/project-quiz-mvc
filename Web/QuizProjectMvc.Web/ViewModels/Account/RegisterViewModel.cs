namespace QuizProjectMvc.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;
    using Common;
    using Data.Models;
    using Infrastructure.Mapping;

    public class RegisterViewModel : AccountEditableDetailsViewModel, IMapTo<User>
    {
        [Display(Name = "Username")]
        [Required]
        [MinLength(ModelConstraints.UsernameMinLength, ErrorMessage = StatusMessages.MinimumLength)]
        [MaxLength(ModelConstraints.UsernameMaxLength, ErrorMessage = StatusMessages.MaximumLength)]
        public string UserName { get; set; }
    }
}
