﻿
using System.ComponentModel.DataAnnotations;

namespace Squawkings.Models
{
	public class LogonInputModel
	{
		[Required]
		public string Username { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public bool	RememberMe { get; set; }

		public string ReturnUrl { get; set; }
	}
}