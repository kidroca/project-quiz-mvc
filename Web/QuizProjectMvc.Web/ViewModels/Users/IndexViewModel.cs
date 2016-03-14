namespace QuizProjectMvc.Web.ViewModels.Users
{
    using System.Collections.Generic;
    using ProfileInformation;
    using Services.Data.Models;

    public class IndexViewModel
    {
        public ICollection<PublicProfileDetailed> Profiles { get; set; }

        public Pager Pager { get; set; }
    }
}
