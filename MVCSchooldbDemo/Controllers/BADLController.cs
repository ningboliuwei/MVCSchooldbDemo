using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using MVCSchooldbDemo.Classes;
using MVCSchooldbDemo.Models.Data;

namespace MVCSchooldbDemo.Controllers
{
    [Authorize]
    public class BADLController : Controller
    {
        private readonly SchooldDbContext _db = new SchooldDbContext();

        public BADLController()
        {
            ViewBag.Name = "BADL";
            ViewBag.DialogTitle = "BADL 评估：Barthel 指数评价表"; //TODO
        }

        public ActionResult Index()
        {
            return View();
        }

        public string List(string queryParasString, int page, int rows, string sort, string order)
        {
            var result = DBHelper.GetResult(_db.BADLs.ToList(), queryParasString, page, rows, sort, order);

            return result;
        }

        public virtual ActionResult GetAllItems()
        {
            return new JsonResult
            {
                Data = _db.BADLs.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public virtual ActionResult Details(long? id)
        {
            ViewBag.CurrentId = id;
            return View(new BADLInfo());
        }

        [HttpPost]
        public virtual ActionResult GetSingleItem(long? id)
        {
            if (id != null)
            {
                var item = DBHelper.FindByKeyword(_db.BADLs.ToList(), "Id", id).First();
                var list = _db.BADLs.ToList();
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
                                item.大便,
                                item.小便,
                                item.修饰,
                                item.用厕,
                                item.进食,
                                item.转移,
                                item.活动,
                                item.穿衣,
                                item.上下楼梯,
                                item.洗澡,
                                item.被评估人,
                                item.评估日期,
                                item.康复护士,
                                item.总分
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
        public virtual ActionResult Create(BADLInfo item)
        {
            if (ModelState.IsValid)
            {
                item.总分 = GetSum(item);
                _db.BADLs.Add(item);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public int GetSum(BADLInfo item)
        {
            return item.大便 + item.小便 + item.修饰 + item.用厕 + item.进食 + item.转移 + item.活动 + item.穿衣 + item.上下楼梯 + item.洗澡;
        }

        public virtual ActionResult Edit(long? id)
        {
            if (id != null)
            {
                ViewBag.CurrentId = id;
                return View(new BADLInfo());
            }

            return HttpNotFound();
        }

        [HttpPost]
        public virtual ActionResult Edit(BADLInfo item)
        {
            item.总分 = GetSum(item);
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public virtual ActionResult Delete(List<long> ids)
        {
            foreach (var id in ids)
            {
                var item = DBHelper.FindByKeyword(_db.BADLs.ToList(), "Id", id).First();
                _db.BADLs.Remove(item);
            }

            _db.SaveChanges();
            return RedirectToAction("Index", "BADL");
        }

        [HttpPost]
        public int SumScore(string d)
        {
            return 0;
        }
    }
}