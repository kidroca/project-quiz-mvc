namespace QuizProjectMvc.Web.ViewModels.ProfileInformation
{
    using System.Collections.Generic;

    public class ProfilePageViewModel
    {
        public ProfilePageViewModel()
        {
            this.QuizzesCreated = new HashSet<CreatedQuizInfo>();
            this.QuizzesTaken = new HashSet<TakenQuizInfo>();
        }

        public PublicProfileInformation PublicProfile { get; set; }

        public ICollection<CreatedQuizInfo> QuizzesCreated { get; set; }

        public ICollection<TakenQuizInfo> QuizzesTaken { get; set; }
    }
}
