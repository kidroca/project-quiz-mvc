namespace QuizProjectMvc.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class QuizRating
    {
        [Key]
        [Column(Order = 0)]
        public int QuizId { get; set; }

        public virtual Quiz Quiz { get; set; }

        [Key]
        [Column(Order = 1)]
        [Required]
        public string ByUserId { get; set; }

        public User ByUser { get; set; }

        [Range(1, 10)]
        public double Value { get; set; }
    }
}
