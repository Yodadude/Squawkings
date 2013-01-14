using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FluentValidation.Attributes;
using FluentValidation.Mvc;
using SchoStack.Web.Conventions;
using SchoStack.Web.Conventions.Core;

namespace Squawkings
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.IgnoreRoute("favicon.ico");

			routes.MapRoute(
				"Logon", // Route name
				"logon", // URL with parameters
				new { controller = "Logon", action = "Index" } // Parameter defaults
			);

			routes.MapRoute(
				"Logoff", // Route name
				"logoff", // URL with parameters
				new { controller = "Logon", action = "Logoff" } // Parameter defaults
);

			routes.MapRoute(
				"Global", // Route name
				"global", // URL with parameters
				new { controller = "Global", action = "Index"} // Parameter defaults
			);

			routes.MapRoute(
				"Totals", // Route name
				"totals", // URL with parameters
				new { controller = "Home", action = "Totals", id = UrlParameter.Optional } // Parameter defaults
			);

			routes.MapRoute(
				"Home", // Route name
				"", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);

			routes.MapRoute(
				"Upload", // Route name
				"upload", // URL with parameters
				new {controller = "Profile", action = "Upload", username = ""} // Parameter defaults
			);

			routes.MapRoute(
				"Profile", // Route name
				"{username}", // URL with parameters
				new { controller = "Profile", action = "Index", username = "" } // Parameter defaults
			);

			//routes.MapRoute(
			//    "Default", // Route name
			//    "{controller}/{action}/{id}", // URL with parameters
			//    new { controller = "Home", action = "Index" } // Parameter defaults
			//);

		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);

			HtmlConventionFactory.Add(new DefaultHtmlConventions());
			HtmlConventionFactory.Add(new DataAnnotationHtmlConventions());
			HtmlConventionFactory.Add(new DataAnnotationValidationHtmlConventions());
			HtmlConventionFactory.Add(new UploadFileHtmlConvention());

			ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(new AttributedValidatorFactory()));
		}
	}
}