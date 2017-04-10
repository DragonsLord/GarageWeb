using System;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using GarageWeb.Controllers;

using GarageWeb.Infrastructure;

namespace GarageWeb.Tests.Controllers
{
    [TestClass]
    public class AdminControllerTest
    {
        private IAuthHelper GetMock()
        {
            Mock<IAuthHelper> mock = new Mock<IAuthHelper>();
            mock.Setup(m => m.SignIn(It.Is<string>(login => login == "Admin"), It.Is<string>(password => password == "admin"))).Returns(true);

            return mock.Object;
        }
        [TestMethod]
        public void IncorrectLoginTest()
        {
            var admin_controller = new AdminController(GetMock());
            var result = admin_controller.Login(new Models.ViewModel.LoginViewModel() { Login = "User", Password = "user" });
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void CorrectLoginTest()
        {
            var admin_controller = new AdminController(GetMock());
            var result = admin_controller.Login(new Models.ViewModel.LoginViewModel() { Login = "Admin", Password = "admin" }) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);
        }
    }
}
