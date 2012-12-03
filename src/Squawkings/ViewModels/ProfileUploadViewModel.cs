using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Squawkings.ViewModels
{
	public class ProfileUploadViewModel
	{
		public bool IsGravatar { get; set; }
		public HttpPostedFileBase File { get; set; }
	}
}