using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSchooldbDemo.Classes;
using MVCSchooldbDemo.Common;
using MVCSchooldbDemo.Models.Data;

namespace MVCSchooldbDemo.Controllers
{
    public class RoleController : Controller
    {
        private readonly SchooldDbContext _db = new SchooldDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public string GetList(string queryParasString, int page, int rows, string sort, string order)
        {
            var result = DBHelper.GetResult(_db.Roles.ToList(), queryParasString, page, rows, sort, order);

            return result;
        }

	    public ActionResult GetAllRoles()
	    {
		    return new JsonResult() {Data = _db.Roles.ToList(),
				JsonRequestBehavior = JsonRequestBehavior.AllowGet
			};
	    }

        [HttpPost]
        public ActionResult GetRoleData(long? id)
        {
            if (id != null)
            {
                var item = DBHelper.FindByKeyword(_db.Roles.ToList(), "Id", id).First();
                var list = _db.Roles.ToList();
                var currentIndex = list.IndexOf(item);

                return new JsonResult
                {
                    Data =
                        new
                        {
                            Item =
                                new
                                {
                                    item.Id,
                                    item.Name
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

        public ActionResult Create()
        {
            ViewBag.DialogTitle = "添加角色";
            return View();
        }

        
        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        [Bind(Include = "Id,Sno,Sname,Ssex,Sage,Sdept")]
        public ActionResult Create(RoleInfo role)
        {
            if (ModelState.IsValid)
            {
                _db.Roles.Add(role);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(long? id)
        {
            if (id != null)
            {
                ViewBag.DialogTitle = "编辑角色";
                ViewBag.CurrentId = id;
                return View(new RoleInfo());
            }

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Edit(RoleInfo item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(List<long> ids)
        {
            foreach (var id in ids)
            {
                var item = DBHelper.FindByKeyword(_db.Roles.ToList(), "Id", id).First();
                _db.Roles.Remove(item);
            }

            _db.SaveChanges();
            return RedirectToAction("Index", "Role");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}