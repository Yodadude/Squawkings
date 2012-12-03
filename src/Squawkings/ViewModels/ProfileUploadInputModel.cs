using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Squawkings.ViewModels
{
	public class ProfileUploadInputModel
	{
		public bool IsGravatar { get; set; }
		public HttpPostedFileBase ImageFile { get; set; }
	}
}