using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web;
using FluentValidation;
using FubuCore;
using NPoco;
using Squawkings;
using Squawkings.Controllers;
using Squawkings.Models;
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
							x.For<IDatabase>().HybridHttpOrThreadLocalScoped().Use(MyDatabase1.GetDatabase);
							x.For<IAuthentication>().HybridHttpOrThreadLocalScoped().Use(new SquawkAuthentication());
                        });
            return ObjectFactory.Container;
        }
    }
}

public class MyDatabase1
{
	public static IDatabase GetDatabase()
	{
		return InitNPoco.DbFact.Build(new MyDatabase("Squawkings"));
	}

	public class MyDatabase : Database
	{
		public MyDatabase(string connectionStringName) : base(connectionStringName)
		{
		}

		public override System.Data.IDbConnection OnConnectionOpened(System.Data.IDbConnection conn)
		{
			return new ProfiledDbConnection((DbConnection) conn, MiniProfiler.Current);
		}
	}
}

