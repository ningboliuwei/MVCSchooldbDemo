using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using MVCSchooldbDemo.Classes;
using MVCSchooldbDemo.Common;
using MVCSchooldbDemo.Models.Data;

namespace MVCSchooldbDemo.Controllers
{
	[Authorize]
	public class UserController : Controller
	{
		private readonly SchooldDbContext _db = new SchooldDbContext();

		public UserController()
		{
			ViewBag.Name = "User";
			ViewBag.DialogTitle = "用户";
		}

		public virtual ActionResult Index()
		{
			return View();
		}


		public virtual ActionResult GetAllItems()
		{
			return new JsonResult
			{
				Data = _db.Users.ToList(),
				JsonRequestBehavior = JsonRequestBehavior.AllowGet
			};
		}

		public string List(string queryParasString, int page, int rows, string sort, string order)
		{
			var list = from r in _db.Roles
				join u in _db.Users
				on r.Id equals u.RoleId
				select new {u.Id, u.Account, u.FullName, RoleName = r.Name};
			var result = DBHelper.GetResult(list.ToList(), queryParasString, page, rows, sort, order);

			return result;
		}

		[HttpPost]
		public virtual ActionResult GetSingleItem(long? id)
		{
			if (id != null)
			{
				var user = DBHelper.FindByKeyword(_db.Users.ToList(), "Id", id).First();
				var list = _db.Users.ToList();
				var currentIndex = list.IndexOf(user);

				var item = (from r in _db.Roles
					where r.Id == user.RoleId
					select new {user.Id, user.Account, user.FullName, user.RoleId, RoleName = r.Name}).ToList().First();

				return new JsonResult
				{
					Data =
						new
						{
							Item =
							new
							{
								item.Id,
								item.Account,
								item.FullName,
								item.RoleId,
								item.RoleName
							},
							CurrentIndex = currentIndex,
							PreviousId = currentIndex == 0 ? -1 : list[currentIndex - 1].Id,
							NextId = currentIndex == list.Count - 1 ? -1 : list[currentIndex + 1].Id
						},
					JsonRequestBehavior = JsonRequestBehavior.AllowGet
				};
			}

			return HttpNotFound();
		}

		public virtual ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public virtual ActionResult Create(UserInfo user)
		{
			if (ModelState.IsValid)
			{
				user.Password = UtilityHelper.MD5(user.Password);
				_db.Users.Add(user);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View();
		}

		// GET: Student/Edit/5
		[HttpGet]
		public virtual ActionResult Edit(long? id)
		{
			if (id != null)
			{
				ViewBag.CurrentId = id;
				return View(new UserInfo());
			}

			return HttpNotFound();
		}

		[HttpPost]
		public virtual ActionResult Edit(UserInfo user)
		{
			_db.Entry(user).State = EntityState.Modified;
			user.Password = UtilityHelper.MD5(user.Password);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpPost]
		public virtual ActionResult Delete(List<long> ids)
		{
			foreach (var id in ids)
			{
				var user = DBHelper.FindByKeyword(_db.Users.ToList(), "Id", id).First();
				_db.Users.Remove(user);
			}

			_db.SaveChanges();
			return RedirectToAction("Index", "User");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
				_db.Dispose();
			base.Dispose(disposing);
		}
	}
}