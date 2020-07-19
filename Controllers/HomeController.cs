using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResearchTube.Models;
using Microsoft.Extensions.Options;


namespace ResearchTube.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOptions<StripeOptions> options;


        public IActionResult _LoginPartial()
        {
            return View();
        }

        public HomeController(ILogger<HomeController> logger)
        {
            this.options = options;
            _logger = logger;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Plans()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        [HttpPost]
        public IActionResult upload([FromServices] IWebHostEnvironment environment)
        {

            var files = Request.Form.Files;
            string webRootPath = environment.WebRootPath;
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

            return Content("Upload format error！");
        }
    }
}
