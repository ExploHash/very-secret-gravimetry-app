using NUnit.Framework;
using Moq;

using Gravimetry.ViewModels;
using Gravimetry.Services;
using Gravimetry.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Gravimetry.UnitTests
{
    [TestFixture()]
    public class JoinTeamsTest
    {
        JoinTeamsViewModel sut;

        Mock<UserService> MockedUserService;
        Mock<TeamsService> MockedTeamsService;

        public JoinTeamsTest()
        {
            MockedUserService = new Mock<UserService>();
            MockedTeamsService = new Mock<TeamsService>();

            sut = new JoinTeamsViewModel(
                MockedTeamsService.Object,
                MockedUserService.Object
            );
        }

        [Test()]
        public async Task TestItemsFiltering()
        {
            //Setup
            MockedTeamsService.Setup(x => x.GetPublicTeams()).Returns(Task.FromResult(new List<Team> {
                new Team()
                {
                    Id = 1
                },
                new Team()
                {
                    Id = 2
                }
            }));

            MockedUserService.Setup(x => x.GetTeams()).Returns(Task.FromResult(new List<Team> {
                new Team()
                {
                    Id = 1
                }
            }));

            //Act
            await sut.ExecuteLoadItemsCommand();

            //Assert
            Assert.AreEqual(sut.Items.Count, 2);
            foreach (var item in sut.Items) //Loop through and check if userjoined is set correctly
            {
                if (item.Id == 1)
                {
                    Assert.IsTrue(item.UserJoined);
                }
                else
                {
                    Assert.IsFalse(item.UserJoined);
                }
            }
        }
    }
}
