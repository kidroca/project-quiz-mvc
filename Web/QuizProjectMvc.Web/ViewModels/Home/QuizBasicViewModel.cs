namespace QuizProjectMvc.Web.ViewModels.Home
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Infrastructure.Mapping;

    public class QuizBasicViewModel : IMapFrom<Quiz>
    {
        public int Id { get; set; }

        public QuizCategoryViewModel Category { get; set; }

        public string Description { get; set; }

        [Display(Name = "Created By")]
        public UserBasicInfoViewModel CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsPrivate { get; set; }

        //public void CreateMappings(IMapperConfiguration configuration)
        //{
        //    configuration.CreateMap<Quiz, QuizBasicViewModel>()
        //        .ForMember(
        //            self => self.CreatedBy,
        //            opt => opt.MapFrom(
        //                x => $"{x.CreatedBy.FirstName} {x.CreatedBy.LastName}"));
        //}
    }
}
