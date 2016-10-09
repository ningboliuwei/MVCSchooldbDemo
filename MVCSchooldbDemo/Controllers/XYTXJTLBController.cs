using System;
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
    [Authorize]
    public class XYTXJTLBController : Controller
    {
        private readonly SchooldDbContext _db = new SchooldDbContext();

		public XYTXJTLBController()
		{
			ViewBag.Name = "西雅图心绞痛量表";
			ViewBag.DialogTitle = "西雅图心绞痛量表";
		}

        public ActionResult Index()
        {
            return View();
        }

		public string List(string queryParasString, int page, int rows, string sort, string order)
        {
            var result = DBHelper.GetResult(_db.XYTXJTLBs.ToList(), queryParasString, page, rows, sort, order);

            return result;
        }

        public virtual ActionResult Details(long? id)
        {
            ViewBag.CurrentId = id;
            return View(new XYTXJTLBInfo());
        }

		[HttpPost]
        public virtual ActionResult GetItemData(long? id)
        {
            if (id != null)
            {
                var item = DBHelper.FindByKeyword(_db.XYTXJTLBs.ToList(), "Id", id).First();
                var list = _db.XYTXJTLBs.ToList();
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
									item.评估日期//TODO
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
        public virtual ActionResult Create(XYTXJTLBInfo item)
        {
            if (ModelState.IsValid)
            {
                _db.XYTXJTLBs.Add(item);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        public virtual ActionResult Edit(long? id)
        {
            if (id != null)
            {
                ViewBag.CurrentId = id;
                return View(new XYTXJTLBInfo());
            }
            
			return HttpNotFound();
        }

        [HttpPost]
        public virtual ActionResult Edit(XYTXJTLBInfo item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

		[HttpPost]
        public virtual ActionResult Delete(List<long> ids)
        {
            foreach (var id in ids)
            {
                var item = DBHelper.FindByKeyword(_db.XYTXJTLBs.ToList(), "Id", id).First();
                _db.XYTXJTLBs.Remove(item);
            }

            _db.SaveChanges();
            return RedirectToAction("Index", "XYTXJTLB");
        }
    }
}
