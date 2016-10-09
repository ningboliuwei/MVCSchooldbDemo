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
    public class StudentController : Controller
    {
        private readonly SchooldDbContext _db = new SchooldDbContext();

        // GET: Student
        public virtual ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string queryParasString, int page, int rows, string sort, string order)
        {
            var result = DBHelper.GetResult(_db.Students.ToList(), queryParasString, page, rows, sort, order);

            return new JsonResult {Data = result};
        }


        public virtual ActionResult Details(long? id)
        {
            ViewBag.DialogTitle = "明细"; //TODO
            ViewBag.CurrentId = id;
            return View(new StudentInfo());
        }

        [HttpPost]
        public virtual ActionResult GetStudentData(long? id)
        {
            if (id != null)
            {
                var item = DBHelper.FindByKeyword(_db.Students.ToList(), "Id", id).First();
                var list = _db.Students.ToList();
                var currentIndex = list.IndexOf(item);

                string photoPath;

                if (!string.IsNullOrEmpty(item.SphotoGuid))
                {
                    var fileInfo = _db.UploadFiles.Where(f => f.Guid == item.SphotoGuid).ToList()[0];
                    //                    var fileInfo = UploadFileHelper.GetFileInfoByGuid(item.SphotoGuid);
                    photoPath = fileInfo.BaseDirectory + fileInfo.FileName;
                }
                else
                {
                    photoPath = "#";
                }

                return new JsonResult
                {
                    Data =
                        new
                        {
                            Item =
                            new
                            {
                                item.Sno,
                                item.Sname,
                                item.Sage,
                                item.Ssex,
                                item.Sdept,
                                item.SphotoGuid,
                                SphotoPath = photoPath
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

        // GET: Student/Create
        public virtual ActionResult Create()
        {
            ViewBag.DialogTitle = "添加";
            return View();
        }

        // POST: Student/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        public virtual ActionResult Create(StudentInfo student)
        {
            if (ModelState.IsValid)
            {
                _db.Students.Add(student);
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
                //                var student = DBHelper.FindByKeyword(_db.Students.ToList(), "Id", id).First();
                //                ViewBag.DialogTitle = "编辑学生记录";
                //                return View(student);
                ViewBag.DialogTitle = "编辑";
                ViewBag.CurrentId = id;
                return View(new StudentInfo());
            } //死循环了


            return HttpNotFound();
        }


        // POST: Student/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        public virtual ActionResult Edit(StudentInfo student)
        {
            _db.Entry(student).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public virtual ActionResult Delete(List<long> ids)
        {
            foreach (var id in ids)
            {
                var student = DBHelper.FindByKeyword(_db.Students.ToList(), "Id", id).First();
                _db.Students.Remove(student);
            }

            _db.SaveChanges();
            return RedirectToAction("Index", "Student");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }

        public virtual ActionResult UploadPhoto(HttpPostedFileBase fileData)
        {
            var fileInfo = UploadFileHelper.Upload(fileData, "Photos/");
            return new JsonResult {Data = fileInfo};
        }

        //        [HttpPost]
        //        public ActionResult GetPhotoPath(string photoGuid)
        //        {
        //           
        //
        //            return new JsonResult {Data = photoPath};

        //        }
    }
}