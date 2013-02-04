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
			                                             		x.PrimaryKeysNamed(y => y.Name.ToLower() + "id");
			                                             		x.TablesNamed(y => Inflector.MakePlural(y.Name).ToLower());
			                                             		x.Columns.Named(y => y.Name.ToLower());
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