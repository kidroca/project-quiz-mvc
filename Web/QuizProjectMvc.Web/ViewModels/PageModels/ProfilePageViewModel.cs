namespace QuizProjectMvc.Web.ViewModels.PageModels
{
    using System.Collections.Generic;
    using ProfileInformation;

    public class ProfilePageViewModel
    {
        public ProfilePageViewModel()
        {
            this.QuizzesCreated = new HashSet<CreatedQuizInfo>();
            this.QuizzesTaken = new HashSet<TakenQuizInfo>();
        }

        public PublicProfileDetailed PublicProfile { get; set; }

        public ICollection<CreatedQuizInfo> QuizzesCreated { get; set; }

        public ICollection<TakenQuizInfo> QuizzesTaken { get; set; }
    }
}
