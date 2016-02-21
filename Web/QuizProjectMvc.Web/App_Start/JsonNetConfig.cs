namespace QuizProjectMvc.Web
{
    using System.Web.Http;
    using Newtonsoft.Json.Serialization;

    public static class JsonNetConfig
    {
        public static void UseCamelCase(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
        }
    }
}
