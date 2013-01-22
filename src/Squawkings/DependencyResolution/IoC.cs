using FluentValidation;
using NPoco;
using Squawkings.Controllers;
using StackExchange.Profiling;
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
		return new Database("Squawkings");
	}
}

