using System.Web;
using SchoStack.Web.Conventions.Core;

namespace Squawkings
{
	public class UploadFileHtmlConvention : HtmlConvention
	{
		public UploadFileHtmlConvention()
		{
			this.Inputs.If<HttpPostedFileBase>().Modify((h, r) => h.Attr("type", "file"));
		}
	}
}