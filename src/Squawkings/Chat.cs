using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Squawkings
{
	public class Chat : Hub
	{
		public void Send(string message)
		{
			// Call the addMessage method on all clients            
			Clients.All.addMessage(message);
		}

		public void Move(int x, int y)
		{
			Clients.All.moveBox(x, y);
		}
	}
}