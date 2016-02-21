namespace QuizProjectMvc.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;

    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        public string Username { get; set; }
    }
}
