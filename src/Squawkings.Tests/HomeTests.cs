using System.Collections.Generic;
using Moq;
using NPoco;
using NUnit.Framework;
using Squawkings.Controllers;
using System.Web.Mvc;
using Squawkings.Models;
using Squawkings.ViewModels;

namespace Squawkings.Tests
{
	[TestFixture]
	public class HomeTests : ControllerTest
	{
		[Test]
		public void WhenNotAuthenticatedRedirectToGlobal()
		{
			// Arrange
			var dbmock = new Mock<IDatabase>();
			var controller = CB.Of(new HomeController(dbmock.Object)).Build();

			// Act
			var result = controller.Index() as RedirectToRouteResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("Global", result.RouteValues["controller"]);
			Assert.AreEqual("Index", result.RouteValues["action"]);
		}

		[Test]
		public void WhenAuthenticatedReturnHomeView()
		{
			// Arrange
			var dbmock = new Mock<IDatabase>();
			dbmock.Setup(x => x.Fetch<SquawkDisplay>(It.IsAny<Sql>())).Returns(new List<SquawkDisplay>());
			var controller = CB.Of(new HomeController(dbmock.Object))
				.WithLoggedInId(1)
				.Build();

			// Act
			var result = controller.Index() as ViewResult;
			//var squawks = result.Model.As<HomeIndexViewModel>().Squawks;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(typeof (HomeIndexViewModel), result.Model.GetType());
		}
	}

}
