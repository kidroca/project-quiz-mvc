namespace QuizProjectMvc.Web.ViewModels.Quiz
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using User;

    public class QuizBasicViewModel : IMapFrom<Quiz>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public QuizCategoryViewModel Category { get; set; }

        public string Description { get; set; }

        [Display(Name = "Created By")]
        public UserBasicInfoViewModel CreatedBy { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Questions")]
        public virtual int QuestionsCount { get; set; }

        [Display(Name = "Times Completed")]
        public virtual int TimesCompleted { get; set; }

        public bool IsPrivate { get; set; }

        public double Rating { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Quiz, QuizBasicViewModel>()
                .ForMember(
                    self => self.Rating,
                    opt => opt.MapFrom(
                        dest => dest.Ratings.Count > 0 ? dest.Ratings.Average(r => r.Value) : 0))
                .ForMember(
                    self => self.QuestionsCount,
                    opt => opt.MapFrom(
                        dest => dest.Questions.Count));
        }
    }
}
