using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Gravimetry.UiTests
{
    [TestFixture(Platform.Android)]
    public class TestManager
    {
        IApp app;
        Platform platform;

        public TestManager(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = ConfigureApp.Android.InstalledApp("com.explohash.gravimetry").StartApp();
            //Setup
            app.WaitForElement(c => c.Marked("LoginButton"));
            app.Query(e => e.Marked("LoginUserName").Invoke("setText", "jimmy"));
            app.Query(e => e.Marked("LoginPassword").Invoke("setText", "Start1234%"));
            app.Tap(c => c.Marked("LoginButton"));
        }

        [Test]
        public void ManagerCreateTeam()
        {
            //Prepare
            app.WaitForElement(c => c.Marked("AdminButton"));
            app.Tap(c => c.Marked("AdminButton"));
            //Act
            app.WaitForElement(c => c.Marked("GoToCreateButton"));
            app.Tap(c => c.Marked("GoToCreateButton"));
            app.WaitForElement(c => c.Marked("CreateTeamButton"));
            app.Query(e => e.Marked("CreateTeamName").Invoke("setText", "CreateTeamTest"));
            app.Tap(c => c.Marked("CreateTeamCheck"));
            app.Tap(c => c.Marked("CreateTeamButton"));
            //Assert
            AppResult[] results = app.WaitForElement(c => c.Text("CreateTeamTest"));
            Assert.IsTrue(results.Any());
            Assert.NotNull(app.Query(c => c.Text("CreateTeamTest")));
        }

        [Test]
        public void ManagerDeleteTeam()
        {
            //Prepare
            app.WaitForElement(c => c.Marked("AdminButton"));
            app.Tap(c => c.Marked("AdminButton"));
            app.Tap(c => c.Text("DeleteTeamTest"));
            //Act
            app.Tap(c => c.Text("Delete"));
            //Assert
            app.WaitForElement(c => c.Marked("GoToCreateButton"));
            Assert.IsEmpty(app.Query(c => c.Text("DeleteTeamTest")));
        }

        [Test]
        public void UserLeaveTeam()
        {
            //Prepare
            app.WaitForElement(c => c.Marked("AdminButton"));
            app.Tap(c => c.Marked("Teams"));
            //Act
            app.Tap(c => c.Text(""));
            //Assert
            app.WaitForElement(c => c.Text("Join"));
            Assert.IsEmpty(app.Query(c => c.Text("Team 1")));
        }
    }
 }
   
