using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using MVCSchooldbDemo.Classes;
using MVCSchooldbDemo.Models.Data;

namespace MVCSchooldbDemo.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        private readonly SchooldDbContext _db = new SchooldDbContext();

        public PatientController()
        {
            ViewBag.Name = "Patient";
        }

        // GET: Student
        public virtual ActionResult Index()
        {
            return View();
        }

        public string List(string queryParasString, int page, int rows, string sort, string order)
        {
            var result = DBHelper.GetResult(_db.Patients.ToList(), queryParasString, page, rows, sort, order);

            return result;
        }


        public virtual ActionResult Details(long? id)
        {
            ViewBag.DialogTitle = "查看病人基本资料";
            ViewBag.CurrentId = id;
            return View(new PatientInfo());
        }

        [HttpPost]
        public virtual ActionResult GetData(long? id)
        {
            if (id != null)
            {
                var item = DBHelper.FindByKeyword(_db.Patients.ToList(), "Id", id).First();
                var list = _db.Patients.ToList();
                var currentIndex = list.IndexOf(item);

                return new JsonResult
                {
                    Data =
                        new
                        {
                            Item =
                            new
                            {
                                item.住院号,
                                item.姓名,
                                item.性别,
                                item.出生日期,
                                item.入组日期,
                                item.民族,
                                item.婚姻状况,
                                item.文化程度,
                                item.职业,
                                item.年收入,
                                item.保险类别,
                                item.联系电话,
                                item.联系地址
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
            ViewBag.DialogTitle = "添加病人基本资料";
            return View();
        }

        [HttpPost]
        public virtual ActionResult Create(PatientInfo item)
        {
            if (ModelState.IsValid)
            {
                _db.Patients.Add(item);
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
                ViewBag.DialogTitle = "编辑病人基本信息";
                ViewBag.CurrentId = id;
                return View(new PatientInfo());
            }

            return HttpNotFound();
        }


        [HttpPost]
        public virtual ActionResult Edit(PatientInfo item)
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
                var item = DBHelper.FindByKeyword(_db.Patients.ToList(), "Id", id).First();
                _db.Patients.Remove(item);
            }

            _db.SaveChanges();
            return RedirectToAction("Index", "Patient");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }
    }
}