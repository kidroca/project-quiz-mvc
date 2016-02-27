namespace QuizProjectMvc.Web.Areas.Api.Models
{
    using System;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using Services.Data.Models.Account;

    public class UserInfoViewModel : BasicAccountInfoViewModel, IHaveCustomMappings
    {
        public string UserId { get; set; }

        public string Username { get; set; }

        public DateTime CreateedOn { get; set; }

        public string LoginProvider { get; set; }

        public int QuizzesCreated { get; set; }

        public int QuizzesTaken { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<User, UserInfoViewModel>()
                .ForMember(
                    self => self.QuizzesCreated,
                    opt => opt.MapFrom(model => model.QuizzesCreated.Count))
                .ForMember(
                    self => self.QuizzesTaken,
                    opt => opt.MapFrom(model => model.SolutionsSubmited.Count));
        }
    }
}
