using System;
using System.Linq;
using System.Web.Mvc;
using MVCSchooldbDemo.Classes;
using MVCSchooldbDemo.Common;

namespace MVCSchooldbDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly SchooldDbContext _db = new SchooldDbContext();

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(string account, string password)
        {
            if (!string.IsNullOrEmpty(account) && !string.IsNullOrEmpty(password))
            {
                var users = DBHelper.FindByKeyword(_db.Users.ToList(), "Account", account);

                if (users.Count == 0)
                {
                    //cannot find the account
                    return Content("-1");
                }

                if (users.First().Password != UtilityHelper.MD5(password))
                {
                    //right account, wrong password
                    return Content("-2");
                }
                //right account, right password
                Session["account"] = account;
                return Content("1");
            }
            return RedirectToAction("Login");
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}