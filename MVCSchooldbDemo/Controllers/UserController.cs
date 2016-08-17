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
    public class UserController : Controller
    {
//        private static List<long?> _ids = new List<long?>();
        private readonly SchooldDbContext _db = new SchooldDbContext();

        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public string GetList(string queryParasString, int page, int rows, string sort, string order)
        {
            var list = from r in _db.Roles join u in _db.Users
                               on r.Id  equals u.RoleId
                               select new { u.Id, u.Account, u.FullName, RoleName = r.Name };
            var result = DBHelper.GetResult(list.ToList(), queryParasString, page, rows, sort, order);

            return result;
        }


        public ActionResult Details(long? id)
        {
            ViewBag.DialogTitle = "查看学生明细";
            ViewBag.CurrentId = id;
            return View(new UserInfo());
        }

        [HttpPost]
        public ActionResult GetUserData(long? id)
        {
            if (id != null)
            {
                var user = DBHelper.FindByKeyword(_db.Users.ToList(), "Id", id).First();
                var list = _db.Users.ToList();
                var currentIndex = list.IndexOf(user);

                var item = (from r in _db.Roles
                    where r.Id == user.RoleId
                    select new {user.Id, user.Account, user.FullName, r.Name}).ToList().First();
                        


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

        // GET: Student/Create
        public ActionResult Create()
        {
            ViewBag.DialogTitle = "添加用户";
            return View();
        }

        // POST: Student/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        [Bind(Include = "Id,Sno,Sname,Ssex,Sage,Sdept")]
        public ActionResult Create(UserInfo user)
        {
            if (ModelState.IsValid)
            {
                _db.Users.Add(user);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Student/Edit/5
        [HttpGet]
        public ActionResult Edit(long? id)
        {
            if (id != null)
            {
                //                var student = DBHelper.FindByKeyword(_db.Students.ToList(), "Id", id).First();
                //                ViewBag.DialogTitle = "编辑学生记录";
                //                return View(student);
                ViewBag.DialogTitle = "编辑学生记录";
                ViewBag.CurrentId = id;
                return View(new StudentInfo());
            } //死循环了

          

            return HttpNotFound();
        }


        // POST: Student/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        public ActionResult Edit(StudentInfo student)
        {
            _db.Entry(student).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(List<long> ids)
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
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }



    }
}