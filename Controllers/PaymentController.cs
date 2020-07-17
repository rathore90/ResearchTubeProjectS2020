using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ResearchTube.Data;
using ResearchTube.Models;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ResearchTube.Controllers
{
    public class PaymentController : Controller
    {
        // GET: /<controller>/
        private readonly ResearchTubeDbContext _db;
        //public readonly Payment payment;
        
        public PaymentController(ResearchTubeDbContext db)
        {
            //this.payment = payment;
            _db = db;
        }

        public IActionResult Plans()
        {
            return View();
        }

        public IActionResult PremiumPlanPage()
        {
            return View();
        }
        public IActionResult BasicPlanPage()
        {
            return View();
        }

        
        public IActionResult TestPage(string uid)
        {
            return View();
          
        }
        [HttpPost]
        public ActionResult Upgrade(string uid)
        {
            var paymentUser = _db.Payments.First(a => a.UserId == uid);
            paymentUser.PlanType = "Pro";
            //_db.Payments.Update(paymentUser);
            _db.SaveChanges();

            Console.WriteLine("You have successfully upgraded your account!");

            return View("Plans");
        }
        

    }
}
