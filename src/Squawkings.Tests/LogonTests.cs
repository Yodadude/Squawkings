using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using Moq;
using NPoco;
using NUnit.Framework;
using Squawkings.Controllers;
using Squawkings.ViewModels;

namespace Squawkings.Tests
{
	[TestFixture]
	public class LogonTests
	{
		[Test]
		public void WhenInvalidUserNameSpecified()
		{
			// Arrange
			var dbmock = new Mock<IDatabase>();
			var forms = new Mock<IAuthentication>();
			var controller = CB.Of(new LogonController(dbmock.Object, forms.Object)).Build();
			var model = new LogonInputModel {Username = "", Password = "", RememberMe = true, ReturnUrl = ""};

			// Act
			var result = controller.Index(model) as ViewResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(false, controller.ModelState.IsValid);

		}

		[Test]
		public void WhenValidUserNameAndPasswordSpecifiedWithNoReturnUrl()
		{
			// Arrange
			var dbmock = new Mock<IDatabase>();
			var forms = new Mock<IAuthentication>();
			dbmock.Setup(x => x.SingleOrDefault<LogonController.LogonUser>(It.IsAny<string>(), It.IsAny<string>())).Returns(new LogonController.LogonUser() { UserId = 1, Password = "AEqCcjEAX+BAXue1TDPJFYbrhxSd5hVzYxNSIPmB7QNj+gkcr1wo727NWIbLIXcEJw==" });
			var controller = CB.Of(new LogonController(dbmock.Object, forms.Object)).Build();

			var model = new LogonInputModel { Username = "test", Password = "password", RememberMe = true, ReturnUrl = "" };

			// Act
			var result = controller.Index(model) as RedirectToRouteResult;

			// Assert
			Assert.AreEqual("Home",result.RouteValues["controller"]);
			Assert.AreEqual("Index",result.RouteValues["action"]);
			forms.Verify(x => x.SetAuthCookie(It.IsAny<string>(), It.IsAny<bool>()), Times.Once());

		}
	
		[Test]
		public void WhenValidUserNameAndPasswordSpecifiedWithReturnUrl()
		{
			// Arrange
			var dbmock = new Mock<IDatabase>();
			var forms = new Mock<IAuthentication>();
			dbmock.Setup(x => x.SingleOrDefault<LogonController.LogonUser>(It.IsAny<string>(), It.IsAny<string>())).Returns(new LogonController.LogonUser() { UserId = 1, Password = "AEqCcjEAX+BAXue1TDPJFYbrhxSd5hVzYxNSIPmB7QNj+gkcr1wo727NWIbLIXcEJw==" });
			var controller = CB.Of(new LogonController(dbmock.Object, forms.Object)).Build();

			var model = new LogonInputModel { Username = "test", Password = "password", RememberMe = true, ReturnUrl = "blah/blah" };

			// Act
			var result = controller.Index(model) as RedirectResult;

			// Assert
			Assert.AreEqual("blah/blah", result.Url);
			forms.Verify(x => x.SetAuthCookie(It.IsAny<string>(), It.IsAny<bool>()), Times.Once());

		}
	}
}
