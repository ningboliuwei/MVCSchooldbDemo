using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using MVCSchooldbDemo.Classes;
using MVCSchooldbDemo.Common;

namespace MVCSchooldbDemo.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private readonly SchooldDbContext _db = new SchooldDbContext();

		[AllowAnonymous]
		public virtual ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		public virtual ActionResult Login(string account, string password)
		{
			if (!string.IsNullOrEmpty(account) && !string.IsNullOrEmpty(password))
			{
				var users = DBHelper.FindByKeyword(_db.Users.ToList(), "Account", account);

				if (users.Count == 0)
					return Content("-1");

				if (users.First().Password != UtilityHelper.MD5(password))
					return Content("-2");
				//right account, right password
				FormsAuthentication.SetAuthCookie(account, false);
				Session["account"] = account;
				return Content("1");
			}
			return RedirectToAction("Login");
		}

		[HttpPost]
		public virtual ActionResult Logout()
		{
			FormsAuthentication.SignOut();

			return Content("1");
		}

		public virtual ActionResult Index()
		{
			return View();
		}

		public virtual ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public virtual ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}