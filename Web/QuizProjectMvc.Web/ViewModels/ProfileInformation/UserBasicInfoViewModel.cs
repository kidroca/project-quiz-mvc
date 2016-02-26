namespace QuizProjectMvc.Web.ViewModels.ProfileInformation
{
    using Data.Models;
    using Infrastructure.Mapping;

    public class UserBasicInfoViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string AvatarUrl { get; set; }
    }
}
