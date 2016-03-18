namespace QuizProjectMvc.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Common.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using QuizProjectMvc.Common;

    public class User : IdentityUser, IHavePrimaryKey<string>, IAuditInfo, IDeletableEntity
    {
        private ICollection<Quiz> quizzesCreated;
        private ICollection<QuizSolution> solutionsSubmited;
        private ICollection<Comment> comments;

        public User()
        {
            this.quizzesCreated = new HashSet<Quiz>();
            this.solutionsSubmited = new HashSet<QuizSolution>();
            this.comments = new HashSet<Comment>();

            this.CreatedOn = DateTime.Now;
        }

        [MinLength(ModelConstraints.NameMinLength)]
        [MaxLength(ModelConstraints.NameMaxLength)]
        public string FirstName { get; set; }

        [MinLength(ModelConstraints.NameMinLength)]
        [MaxLength(ModelConstraints.NameMaxLength)]
        public string LastName { get; set; }

        [MinLength(ModelConstraints.DescriptionMinLength)]
        [MaxLength(ModelConstraints.DescriptionMaxLength)]
        public string Bio { get; set; }

        [RegularExpression(ModelConstraints.AvatarPathPattern)]
        [MinLength(ModelConstraints.UrlMinLength)]
        [MaxLength(ModelConstraints.UrlMaxLength)]
        public string AvatarUrl { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

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

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

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
