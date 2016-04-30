namespace QuizProjectMvc.Common
{
    public class ModelConstraints
    {
        public const int TitleMinLength = 2;
        public const int TitleMaxLength = 500;

        public const int NameMinLength = 2;
        public const int NameMaxLength = 50;

        public const int DescriptionMinLength = 5;
        public const int DescriptionMaxLength = 2000;

        public const int UrlMinLength = 10;
        public const int UrlMaxLength = 500;

        public const int UsernameMinLength = 4;
        public const int UsernameMaxLength = 80;

        public const int MinRating = 1;
        public const int MaxRating = 10;

        public const int MinQuestionsCount = 3;

        public const string AvatarPathPattern =
            @"^\/Content\/images\/avatars\/[\w-]+\.png$|^https:\/\/secure.gravatar.com\/avatar\/.+$";
    }
}
