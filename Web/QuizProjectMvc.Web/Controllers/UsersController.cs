namespace QuizProjectMvc.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using Services.Data.Models;
    using Services.Data.Protocols;
    using ViewModels.ProfileInformation;
    using ViewModels.Users;

    [Authorize]
    public class UsersController : BaseController
    {
        public const int ProfilesPerPage = 6;

        private readonly IUsersService users;

        public UsersController(IUsersService users)
        {
            this.users = users;
        }

        public ActionResult Index(Pager pager)
        {
            if (pager == null || !this.ModelState.IsValid)
            {
                pager = new Pager
                {
                    PageSize = ProfilesPerPage
                };
            }

            if (pager.TotalPages == 0)
            {
                pager.TotalPages =
                    this.Cache.Get(
                        "userPages",
                        () => this.users.GetTotalPages(ProfilesPerPage),
                        durationInSeconds: 15 * 60);
            }

            pager.PageSize = ProfilesPerPage;

            var models = this.users.GetPage(pager)
                .To<PublicProfileDetailed>()
                .ToList();

            var maxQuizzesCreated = this.users.GetMaxCreatedQuizzesCount();
            PublicProfileDetailed.MaxQuizzesCreated = maxQuizzesCreated;

            var viewModel = new IndexViewModel
            {
                Pager = pager,
                Profiles = models
            };

            return this.View(viewModel);
        }
    }
}
