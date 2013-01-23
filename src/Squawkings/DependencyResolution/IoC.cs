using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using FluentValidation;
using NPoco;
using Squawkings.Controllers;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;
using StructureMap;

namespace Squawkings {
    public static class IoC {
        public static IContainer Initialize() {
            ObjectFactory.Initialize(x =>
                        {
                            x.Scan(scan =>
                                    {
                                        scan.TheCallingAssembly();
                                        scan.WithDefaultConventions();
                                    	scan.ConnectImplementationsToTypesClosing(typeof(IValidator<>));
                                    });
							x.For<IDatabase>().HybridHttpOrThreadLocalScoped().Use(MyDatabase.GetDatabase);
							x.For<IAuthentication>().HybridHttpOrThreadLocalScoped().Use(new SquawkAuthentication());
                        });
            return ObjectFactory.Container;
        }
    }
}

public class MyDatabase
{
	public static IDatabase GetDatabase()
	{
		if (HttpContext.Current.Request.IsLocal)
		{
			var conn =
				new ProfiledDbConnection(new SqlConnection(ConfigurationManager.ConnectionStrings["Squawkings"].ConnectionString),
				                         MiniProfiler.Current);
			var db = new Database(conn);
			db.Connection.Open();
			return db;
		}
		
		return new Database("Squawkings");
	}
}

