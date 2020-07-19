using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ResearchTube.Areas.Identity.Pages.Account.Manage
{
    public class UploadVideoModel : PageModel
    {
		private readonly IWebHostEnvironment _iweb;
		public void OnGet()
        {
        }

		public IActionResult upload([FromServices] IWebHostEnvironment environment)
		{
			var files = Request.Form.Files;

			string webRootPath = _iweb.WebRootPath;
			string filePath = webRootPath + "/Videos/";
			string fullPath = "";

			string fileName = Path.GetFileName(files[0].FileName);
			string fileExtension = Path.GetExtension(fileName).ToLower(); 

			if (fileExtension == ".mp4" || fileExtension == ".webm" || fileExtension == ".MPEG")
			{
				string saveName = DateTime.Now.ToString("yyyyMMddhhmmssffff") + fileExtension; 
				fullPath = Path.Combine(filePath, saveName);
				if (!Directory.Exists(filePath))
				{
					Directory.CreateDirectory(filePath);
				}
				using (var stream = new FileStream(fullPath, FileMode.CreateNew))
				{
					files[0].CopyTo(stream);
				}
				return Content("ok");
			}
			//uploadPath = "/Uploads/Images/" + folder + "/" + saveName;

			return Content("Upload format error£¡");
		}
	}
}
