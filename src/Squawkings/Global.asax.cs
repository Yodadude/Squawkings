using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FluentValidation.Attributes;
using FluentValidation.Mvc;
using NPoco.FluentMappings;
using SchoStack.Web.Conventions;
using SchoStack.Web.Conventions.Core;
using Squawkings.Models;

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

			routes.MapRoute("Logon","logon", new { controller = "Logon", action = "Index" });
			routes.MapRoute("Logoff","logoff", new { controller = "Logon", action = "Logoff" });
			routes.MapRoute("Global","global", new { controller = "Global", action = "Index"});
			routes.MapRoute("Totals", "totals", new { controller = "Home", action = "Totals", id = UrlParameter.Optional });
			routes.MapRoute("Home", "", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
			routes.MapRoute("Upload", "upload", new {controller = "Profile", action = "Upload", username = ""});
			routes.MapRoute("Chat", "chat", new { controller = "Chat", action = "Index"});
			routes.MapRoute("Profile", "{username}", new { controller = "Profile", action = "Index", username = "" });

		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RouteTable.Routes.MapHubs();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);

			HtmlConventionFactory.Add(new DefaultHtmlConventions());
			HtmlConventionFactory.Add(new DataAnnotationHtmlConventions());
			HtmlConventionFactory.Add(new DataAnnotationValidationHtmlConventions());
			HtmlConventionFactory.Add(new UploadFileHtmlConvention());

			ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(new AttributedValidatorFactory()));

			InitNPoco.Init();
		}
	}

}