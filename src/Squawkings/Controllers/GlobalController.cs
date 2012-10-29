﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Squawkings.Models;

namespace Squawkings.Controllers
{
    public class GlobalController : Controller
    {

		public ActionResult Index()
		{
			return View(GetSquawkDisplays());
		}

		private static List<SquawkDisplay> GetSquawkDisplays()
		{
			var squawks = new List<SquawkDisplay>
			                  {
			                      new SquawkDisplay() { AvatarUrl = "~/Content/images/placeholder-profile-img.jpg", Username = "test", FullName = "Test User", Time = DateTime.Now.AddMinutes(0), Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."},
			                      new SquawkDisplay() { AvatarUrl = "~/Content/images/placeholder-profile-img2.jpg", Username = "test2", FullName = "Test Two", Time = DateTime.Now.AddMinutes(-1), Content = "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."},
			                      new SquawkDisplay() { AvatarUrl = "~/Content/images/placeholder-profile-img.jpg", Username = "test", FullName = "Test User", Time = DateTime.Now.AddMinutes(-2), Content = "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."},
			                      new SquawkDisplay() { AvatarUrl = "~/Content/images/placeholder-profile-img2.jpg", Username = "test2", FullName = "Test Two", Time = DateTime.Now.AddMinutes(-3), Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."},
			                      new SquawkDisplay() { AvatarUrl = "~/Content/images/placeholder-profile-img2.jpg", Username = "test2", FullName = "Test Two", Time = DateTime.Now.AddMinutes(-4), Content = "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."},
			                      new SquawkDisplay() { AvatarUrl = "~/Content/images/placeholder-profile-img.jpg", Username = "test", FullName = "Test User", Time = DateTime.Now.AddMinutes(-5), Content = "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."}
			                  };
			return squawks;

		}

    }
}