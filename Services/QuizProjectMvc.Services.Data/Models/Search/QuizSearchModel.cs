namespace QuizProjectMvc.Services.Data.Models.Search
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;
    using Common;

    // Todo: Add Constraints
    public class QuizSearchModel
    {
        public string Category { get; set; }

        [MinLength(ModelConstraints.TitleMinLength)]
        [Display(Name = "Key Phrase")]
        public string KeyPhrase { get; set; }

        [MinLength(ModelConstraints.NameMinLength)]
        public string Author { get; set; }

        [Range(1, int.MaxValue)]
        public int? MinQuestions { get; set; }

        [Range(1, int.MaxValue)]
        public int? MaxQuestions { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public ResultOrder? OrderBy { get; set; }

        public bool OrderDescending { get; set; }

        public static IEnumerable<SelectListItem> GetOrderByDropDownValues()
        {
            var items = Enum.GetNames(typeof(ResultOrder)).Select(x => new SelectListItem { Value = x, Text = x });
            return items;
        }
    }
}
