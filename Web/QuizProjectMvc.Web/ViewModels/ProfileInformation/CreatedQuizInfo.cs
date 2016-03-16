namespace QuizProjectMvc.Web.ViewModels.ProfileInformation
{
    using System;
    using Data.Models;
    using Infrastructure.Mapping;

    public class CreatedQuizInfo : IMapFrom<Quiz>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
