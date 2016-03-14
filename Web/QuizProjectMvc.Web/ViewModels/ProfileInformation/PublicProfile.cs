namespace QuizProjectMvc.Web.ViewModels.ProfileInformation
{
    using System.ComponentModel.DataAnnotations;
    using Common;
    using Data.Models;
    using Infrastructure.Mapping;
    using Services.Data.Models.Account;

    public class PublicProfile : BasicAccountInfoViewModel, IMapFrom<User>, IMapTo<User>
    {
        public string Id { get; set; }

        [Display(Name = "Username")]
        [Required]
        [MinLength(ModelConstraints.UsernameMinLength, ErrorMessage = StatusMessages.MinimumLength)]
        [MaxLength(ModelConstraints.UsernameMaxLength, ErrorMessage = StatusMessages.MaximumLength)]
        public string UserName { get; set; }
    }
}
