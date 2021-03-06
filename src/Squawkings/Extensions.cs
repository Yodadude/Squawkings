﻿using System;
using System.Security.Principal;
using NPoco;
using Squawkings.Models;
using StructureMap;

namespace Squawkings
{
	public static class Extensions
	{
		/// <summary>
		/// Return the Left number of characters of the string
		/// </summary>
		/// <param name="str"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public static string Left(this string str, int length)
		{
			if (str.Length >= length)
			{
				return str.Substring(0, length);
			}
			
			return str;
		}

		public static int Id (this IIdentity identity)
		{
			return identity.IsAuthenticated ? Convert.ToInt32(identity.Name) : 0;
		}

		public static string GetName(this IIdentity identity)
		{
			var userName = "";

			if (identity.IsAuthenticated)
			{
				//IDatabase db = new Database("Squawkings");
				var db = ObjectFactory.GetInstance<IDatabase>();
				userName = db.SingleOrDefault<string>(@"select UserName from Users where UserId = @0", identity.Id());
			}

			return userName;
		}


		public static string ToPrettyDate(this DateTime d)
		{
			// 1.
			// Get time span elapsed since the date.
			TimeSpan s = DateTime.UtcNow.Subtract(d);

			// 2.
			// Get total number of days elapsed.
			int dayDiff = (int)s.TotalDays;

			// 3.
			// Get total number of seconds elapsed.
			int secDiff = (int)s.TotalSeconds;

			// 4.
			// Don't allow out of range values.
			if (dayDiff < 0 || dayDiff >= 31)
			{
				return null;
			}

			// 5.
			// Handle same-day times.
			if (dayDiff == 0)
			{
				// A.
				// Less than one minute ago.
				if (secDiff < 60)
				{
					return "just now";
				}
				// B.
				// Less than 2 minutes ago.
				if (secDiff < 120)
				{
					return "1 minute ago";
				}
				// C.
				// Less than one hour ago.
				if (secDiff < 3600)
				{
					return string.Format("{0} minutes ago", Math.Floor((double)secDiff / 60));
				}
				// D.
				// Less than 2 hours ago.
				if (secDiff < 7200)
				{
					return "1 hour ago";
				}
				// E.
				// Less than one day ago.
				if (secDiff < 86400)
				{
					return string.Format("{0} hours ago", Math.Floor((double)secDiff / 3600));
				}
			}
			// 6.
			// Handle previous days.
			if (dayDiff == 1)
			{
				return "yesterday";
			}
			if (dayDiff < 7)
			{
				return string.Format("{0} days ago", dayDiff);
			}
			if (dayDiff < 31)
			{
				double ceiling = Math.Ceiling((double)dayDiff / 7);
				return string.Format("{0} {1} ago", ceiling, ceiling == 1.0d ? "week" : "weeks");
			}
			return null;
		}
	}
}