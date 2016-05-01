namespace QuizProjectMvc.Web.ViewModels.Quiz
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using ProfileInformation;

    public class QuizBasicViewModel : IMapFrom<Quiz>, IHaveCustomMappings
    {
        public static int MaxTimesCompleted { get; set; }

        public int Id { get; set; }

        public string Title { get; set; }

        public QuizCategoryViewModel Category { get; set; }

        public string Description { get; set; }

        [Display(Name = "Created By")]
        public PublicProfile CreatedBy { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Questions")]
        public virtual int QuestionsCount { get; set; }

        [Display(Name = "Times Completed")]
        public virtual int TimesCompleted { get; set; }

        public bool IsPrivate { get; set; }

        public double Rating => (this.TimesCompleted / (double)MaxTimesCompleted) * 10;

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Quiz, QuizBasicViewModel>()
                .ForMember(
                    self => self.QuestionsCount,
                    opt => opt.MapFrom(
                        dest => dest.NumberOfQuestions))
                .ForMember(
                    self => self.TimesCompleted,
                    opt => opt.MapFrom(model => model.Solutions.Count));
        }
    }
}
