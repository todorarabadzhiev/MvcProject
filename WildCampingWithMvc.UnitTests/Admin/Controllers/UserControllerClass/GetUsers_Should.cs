using NUnit.Framework;
using Services.DataProviders;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.JustMock;
using TestStack.FluentMVCTesting;
using WildCampingWithMvc.Areas.Admin.Controllers;
using WildCampingWithMvc.Areas.Admin.Models;
using WildCampingWithMvc.UnitTests.Controllers.CampingPlaceControllerClass;

namespace WildCampingWithMvc.UnitTests.Admin.Controllers.UserControllerClass
{
    [TestFixture]
    public class GetUsers_Should
    {
        [Test]
        public void ReturnJsonWithCorrectModel()
        {
            // Arrange
            var campingUserProvider = Mock.Create<ICampingUserDataProvider>();
            var userController = new UserController(campingUserProvider);
            IEnumerable<ICampingUser> users = Util.GetCampingUsers(3);
            Mock.Arrange(() => campingUserProvider.GetAllCampingUsers()).Returns(users);

            // Act & Assert
            userController
                .WithCallTo(c => c.GetUsers())
                .ShouldReturnJson(data =>
                {
                    Assert.That(data.Count, Is.EqualTo(users.Count()));
                    foreach (var doubleData in users.Zip((ICollection<UserViewModel>)data, Tuple.Create))
                    {
                        Assert.AreEqual(doubleData.Item1.Id, doubleData.Item2.Id);
                        Assert.AreEqual(doubleData.Item1.FirstName, doubleData.Item2.FirstName);
                        Assert.AreEqual(doubleData.Item1.LastName, doubleData.Item2.LastName);
                        Assert.AreEqual(doubleData.Item1.UserName, doubleData.Item2.UserName);
                        Assert.AreEqual(doubleData.Item1.RegisteredOn, doubleData.Item2.RegisteredOn);
                    }
                });
        }
    }
}
