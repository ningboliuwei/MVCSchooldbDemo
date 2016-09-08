using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using MVCSchooldbDemo.Classes;
using MVCSchooldbDemo.Models.Data;

namespace MVCSchooldbDemo.Controllers
{
    [Authorize]
    public class DataDictItemController : Controller
    {
        private readonly SchooldDbContext _db = new SchooldDbContext();

        public DataDictItemController()
        {
            ViewBag.Name = "DataDictItem";
        }

        public ActionResult Index()
        {
            return View();
        }

        public string List(string queryParasString, int page, int rows, string sort, string order)
        {
            var result = DBHelper.GetResult(_db.DataDictItems.ToList(), queryParasString, page, rows, sort, order);

            return result;
        }

        public virtual ActionResult Details(long? id)
        {
            ViewBag.DialogTitle = "查看数据字典项目明细";
            ViewBag.CurrentId = id;
            return View(new DataDictItemInfo());
        }

        [HttpPost]
        public virtual ActionResult GetItemData(long? id)
        {
            if (id != null)
            {
                var item = DBHelper.FindByKeyword(_db.DataDictItems.ToList(), "Id", id).First();
                var list = _db.DataDictItems.ToList();
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
                                item.项目名,
                                item.项目值
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

        public ActionResult GetDataDictItemValues(string itemName)
        {
            var index = 0;
            var result = DBHelper.FindByKeyword(_db.DataDictItems.ToList(), "项目名", itemName)
                .First()
                .项目值.Split(',')
                .ToList().Select(v => new {id = index++, value = v});

            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public virtual ActionResult Create()
        {
            ViewBag.DialogTitle = "添加数据字典项目记录"; //TODO
            return View();
        }

        [HttpPost]
        public virtual ActionResult Create(DataDictItemInfo item)
        {
            if (ModelState.IsValid)
            {
                _db.DataDictItems.Add(item);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public virtual ActionResult Edit(long? id)
        {
            if (id != null)
            {
                ViewBag.DialogTitle = "编辑数据字典项目记录"; //TODO
                ViewBag.CurrentId = id;
                return View(new DataDictItemInfo());
            }

            return HttpNotFound();
        }

        [HttpPost]
        public virtual ActionResult Edit(DataDictItemInfo item)
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
                var item = DBHelper.FindByKeyword(_db.DataDictItems.ToList(), "Id", id).First();
                _db.DataDictItems.Remove(item);
            }

            _db.SaveChanges();
            return RedirectToAction("Index", "DataDictItem");
        }
    }
}