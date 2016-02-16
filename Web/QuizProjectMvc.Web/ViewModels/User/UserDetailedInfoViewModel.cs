namespace QuizProjectMvc.Web.ViewModels.User
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using Quiz;

    public class UserDetailedInfoViewModel : UserBasicInfoViewModel, IHaveCustomMappings
    {
        public DateTime RegisteredOn { get; set; }

        public ICollection<QuizBasicViewModel> QuizzesCreated { get; set; }

        public int QuizzesRated { get; set; }

        public int TotalQuizzesTaken { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<User, UserDetailedInfoViewModel>()
                .ForMember(
                    self => self.TotalQuizzesTaken,
                    opt => opt.MapFrom(x => x.SolutionsSubmited.Count))
                .ForMember(
                    self => self.QuizzesRated,
                    opt => opt.MapFrom(x => x.RatingsGiven.Count));
        }
    }
}
