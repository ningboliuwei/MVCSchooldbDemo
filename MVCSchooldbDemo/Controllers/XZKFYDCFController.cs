using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using MVCSchooldbDemo.Classes;
using MVCSchooldbDemo.Models.Data;

namespace MVCSchooldbDemo.Controllers
{
	[Authorize]
	public class XZKFYDCFController : Controller
	{
		private readonly SchooldDbContext _db = new SchooldDbContext();

		public XZKFYDCFController()
		{
			ViewBag.Name = "心脏康复运动处方";
			ViewBag.DialogTitle = "心脏康复运动处方";
		}

		public ActionResult Index()
		{
			return View();
		}

		public string List(string queryParasString, int page, int rows, string sort, string order)
		{
			var result = DBHelper.GetResult(_db.XZKFYDCFs.ToList(), queryParasString, page, rows, sort, order);

			return result;
		}


		public virtual ActionResult GetAllItems()
		{
			return new JsonResult
			{
				Data = _db.XZKFYDCFs.ToList(),
				JsonRequestBehavior = JsonRequestBehavior.AllowGet
			};
		}


		public virtual ActionResult Details(long? id)
		{
			ViewBag.CurrentId = id;
			return View(new XZKFYDCFInfo());
		}

		[HttpPost]
		public virtual ActionResult GetSingleItem(long? id)
		{
			if (id != null)
			{
				var item = DBHelper.FindByKeyword(_db.XZKFYDCFs.ToList(), "Id", id).First();
				var list = _db.XZKFYDCFs.ToList();
				var currentIndex = list.IndexOf(item);

				return new JsonResult
				{
					Data =
						new
						{
							Item =
							new
							{
								item.病人编号,
								item.基础心率,
								item.运动峰值心率,
								item.基础血压,
								item.运动峰值血压,
								item.心肺联合运动试验平板运动实验结果
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
		public virtual ActionResult Create(XZKFYDCFInfo item)
		{
			if (ModelState.IsValid)
			{
				_db.XZKFYDCFs.Add(item);
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
				return View(new XZKFYDCFInfo());
			}

			return HttpNotFound();
		}

		[HttpPost]
		public virtual ActionResult Edit(XZKFYDCFInfo item)
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
				var item = DBHelper.FindByKeyword(_db.XZKFYDCFs.ToList(), "Id", id).First();
				_db.XZKFYDCFs.Remove(item);
			}

			_db.SaveChanges();
			return RedirectToAction("Index", "XZKFYDCF");
		}
	}
}