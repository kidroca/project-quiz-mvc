namespace QuizProjectMvc.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using QuizProjectMvc.Common;

    public class User : IdentityUser
    {
        private ICollection<Quiz> quizzesCreated;
        private ICollection<QuizRating> ratingsGiven;
        private ICollection<QuizSolution> solutionsSubmited;

        public User()
        {
            this.ratingsGiven = new HashSet<QuizRating>();
            this.quizzesCreated = new HashSet<Quiz>();
            this.solutionsSubmited = new HashSet<QuizSolution>();
        }

        [MinLength(ModelConstraints.NameMinLength)]
        [MaxLength(ModelConstraints.NameMaxLength)]
        public string FirstName { get; set; }

        [MinLength(ModelConstraints.NameMinLength)]
        [MaxLength(ModelConstraints.NameMaxLength)]
        public string LastName { get; set; }

        [Url]
        [MinLength(ModelConstraints.UrlMinLength)]
        [MaxLength(ModelConstraints.UrlMaxLength)]
        public string AvatarUrl { get; set; }

        public DateTime RegisteredOn { get; set; }

        public virtual ICollection<Quiz> QuizzesCreated
        {
            get { return this.quizzesCreated; }
            set { this.quizzesCreated = value; }
        }

        public virtual ICollection<QuizSolution> SolutionsSubmited
        {
            get { return this.solutionsSubmited; }
            set { this.solutionsSubmited = value; }
        }

        public virtual ICollection<QuizRating> RatingsGiven
        {
            get { return this.ratingsGiven; }
            set { this.ratingsGiven = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
}
