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
        public virtual int TotalTimesCompleted { get; set; }

        public bool IsPrivate { get; set; }

        public bool HasMultipleSolutions { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Quiz, QuizBasicViewModel>()
                .ForMember(
                    self => self.QuestionsCount,
                    opt => opt.MapFrom(
                        dest => dest.NumberOfQuestions > 0 ? dest.NumberOfQuestions : dest.Questions.Count))
                .ForMember(
                    self => self.HasMultipleSolutions,
                    opt => opt.MapFrom(
                        dest => dest.NumberOfQuestions > 0 && dest.NumberOfQuestions + 10 < dest.Questions.Count))
                .ForMember(
                    self => self.TotalTimesCompleted,
                    opt => opt.MapFrom(model => model.Solutions.Count));
        }
    }
}
