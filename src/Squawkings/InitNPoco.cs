using System.Linq;
using NPoco;
using NPoco.FluentMappings;
using Squawkings.Models;

namespace Squawkings
{
	public class InitNPoco
	{
		public static DatabaseFactory DbFact;

		public static void Init()
		{
			DbFact = new DatabaseFactory();
			var config = FluentMappingConfiguration.Scan(x =>
			                                             	{
			                                             		x.TheCallingAssembly();
			                                             		x.WithSmartConventions(true);
																x.Columns.IgnoreWhere(y => y.GetCustomAttributes(typeof(IgnoreAttribute), true).Any());
			                                             		x.OverrideMappingsWith(new OverrideNPoco());
			                                             	});
			DbFact.Config().WithFluentConfig(config);
		}
	}

	public class OverrideNPoco : Mappings
	{
		public OverrideNPoco()
		{
			For<UserSecurityInfo>().TableName("usersecurityinfo").PrimaryKey("userid");
		}
	}
}