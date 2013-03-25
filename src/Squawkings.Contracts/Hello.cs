using ServiceStack.ServiceHost;

namespace Squawkings.Contracts
{
	public class Hello : IReturn<HelloResponse>
	{
		public string Name { get; set; }
	}
}
