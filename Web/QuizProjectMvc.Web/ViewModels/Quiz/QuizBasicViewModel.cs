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

        public QuizCategoryViewModel Category { get; set; }

        public string Description { get; set; }

        [Display(Name = "Created By")]
        public UserBasicInfoViewModel CreatedBy { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        public bool IsPrivate { get; set; }

        public double Rating { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Quiz, QuizBasicViewModel>()
                .ForMember(
                    self => self.Rating,
                    opt => opt.MapFrom(
                        db => db.Ratings.Count > 0 ? db.Ratings.Average(r => r.Value) : 0));
        }
    }
}
