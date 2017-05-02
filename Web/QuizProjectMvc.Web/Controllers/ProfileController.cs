namespace QuizProjectMvc.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Services.Data.Models.Account;
    using Services.Data.Protocols;
    using ViewModels.PageModels;
    using ViewModels.ProfileInformation;

    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly IUsersService users;

        public ProfileController(IUsersService users)
        {
            this.users = users;
        }

        public ActionResult My()
        {
            return this.RedirectToAction("UserProfile", new { id = this.UserId });
        }

        [HttpPost]
        public ActionResult Edit(BasicAccountInfoViewModel model)
        {
            // Todo check if model.Id and current userId match or if admin
            if (!this.ModelState.IsValid)
            {
                this.TempData["error"] = "Invalid profile data, the profile wasn't updated";
            }
            else
            {
                var result = this.users.Update(model, this.UserId);
            }

            return this.PartialView("DisplayTemplates/BasicAccountInfoViewModel", model);
        }

        public ActionResult UserProfile(string id)
        {
            var user = this.users.ById(id);
            if (user == null)
            {
                return this.View("Error");
            }

            var profileInfo = this.Mapper.Map<PublicProfileDetailed>(user);
            var lastQuizzes = this.Mapper.Map<List<CreatedQuizInfo>>(
                user.QuizzesCreated.OrderByDescending(q => q.CreatedOn).Take(5));
            var lastSolutions = this.Mapper.Map<List<TakenQuizInfo>>(
                user.SolutionsSubmited.OrderByDescending(s => s.CreatedOn).Take(5));

            var pageModel = new ProfilePageViewModel
            {
                PublicProfile = profileInfo,
                QuizzesCreated = lastQuizzes,
                QuizzesTaken = lastSolutions
            };

            var maxQuizzesCreated = this.users.GetMaxCreatedQuizzesCount();
            PublicProfileDetailed.MaxQuizzesCreated = maxQuizzesCreated;

            return this.View(pageModel);
        }
    }
}
