namespace QuizProjectMvc.Web.Controllers.Tests
{

    using NUnit.Framework;

    [TestFixture]
    public class JokesControllerTests
    {
        //[Test]
        //public void ByIdShouldWorkCorrectly()
        //{
        //    var autoMapperConfig = new AutoMapperConfig();
        //    autoMapperConfig.Execute(typeof(QuizzesController).Assembly);
        //    const string JokeContent = "SomeContent";
        //    var jokesServiceMock = new Mock<IQuizzesService>();
        //    jokesServiceMock.Setup(x => x.GetById(It.IsAny<string>()))
        //        .Returns(new Joke { Content = JokeContent, Category = new JokeCategory { Name = "asda" } });
        //    var controller = new QuizzesController(jokesServiceMock.Object);
        //    controller.WithCallTo(x => x.ById("asdasasd"))
        //        .ShouldRenderView("ById")
        //        .WithModel<QuizBasicViewModel>(
        //            viewModel =>
        //                {
        //                    Assert.AreEqual(JokeContent, viewModel.Content);
        //                }).AndNoModelErrors();
        //}
    }
}
