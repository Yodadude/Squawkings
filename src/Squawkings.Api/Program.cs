using System;
using Funq;
using ServiceStack.ServiceInterface;
using ServiceStack.WebHost.Endpoints;
using Squawkings.Contracts;

namespace Squawkings.Api
{
	public class Program
	{
		static void Main(string[] args)
		{
			const string listernOn = @"http://localhost:1337/";

			var host = new AppHost();
			host.Init();
			host.Start(listernOn);

			Console.WriteLine("Ready...");
			Console.ReadKey();

		}
	}

	public class HelloService : Service
	{
		public object Any(Hello request)
		{
			return new HelloResponse { Result = "Hello, " + request.Name };
		}
	}

	public class AppHost : AppHostHttpListenerBase
	{
		public AppHost() : base("Hello Web Services", typeof(HelloService).Assembly) { }

		public override void Configure(Container container)
		{
			Routes.Add<Hello>("/hello");
			Routes.Add<Hello>("/hello/{Name}");
		}
	}
}
