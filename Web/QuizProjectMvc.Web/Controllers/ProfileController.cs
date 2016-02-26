namespace QuizProjectMvc.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Services.Data.Protocols;
    using ViewModels.ProfileInformation;

    public class ProfileController : BaseController
    {
        private readonly IUsersService users;

        public ProfileController(IUsersService users)
        {
            this.users = users;
        }

        [Authorize]
        public ActionResult My()
        {
            var user = this.users.ById(this.UserId);
            var profileInfo = this.Mapper.Map<PublicProfileInformation>(user);
            var lastQuizzes = this.Mapper.Map<List<CreatedQuizInfo>>(user.QuizzesCreated.Take(10));
            var lastSolutions = this.Mapper.Map<List<TakenQuizInfo>>(user.SolutionsSubmited.Take(10));

            var pageModel = new ProfilePageViewModel
            {
                PublicProfile = profileInfo,
                QuizzesCreated = lastQuizzes,
                QuizzesTaken = lastSolutions
            };

            return this.View(pageModel);
        }
    }
}
