namespace QuizProjectMvc.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    using Common.Models;

    using Microsoft.AspNet.Identity.EntityFramework;

    using QuizProjectMvc.Data.Models;

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public IDbSet<Quiz> Quizzes { get; set; }

        public IDbSet<QuizCategory> QuizCategories { get; set; }

        public IDbSet<Question> Questions { get; set; }

        public IDbSet<Answer> Answers { get; set; }

        public IDbSet<QuizRating> QuizRatings { get; set; }

        public IDbSet<QuizSolution> QuizSolutions { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            this.DeleteOrphanQuestions();

            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //modelBuilder.Entity<QuizRating>()
            //    .HasRequired(rating => rating.Quiz)
            //    .WithMany(quiz => quiz.Ratings)
            //    .HasForeignKey(rating => rating.QuizId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<QuizSolution>()
            //    .HasRequired(solution => solution.ForQuiz)
            //    .WithMany(quiz => quiz.Solutions)
            //    .HasForeignKey(solution => solution.ForQuizId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Comment>()
            //    .HasRequired(comment => comment.Quiz)
            //    .WithMany(quiz => quiz.Comments)
            //    .HasForeignKey(comment => comment.QuizId)
            //    .WillCascadeOnDelete(false);
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        private void DeleteOrphanQuestions()
        {
            this.Questions
                .Local
                .Where(q => q.Quiz == null)
                .ToList()
                .ForEach(q => this.Questions.Remove(q));
        }
    }
}
