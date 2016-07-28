using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Linq.Dynamic;

namespace MVCSchooldbDemo.Controllers
{
    public class StudentController : Controller
    {
        private readonly SchooldbEntities db = new SchooldbEntities();

        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public string GetResult(string sno, string ssex, string sdept, string page, string rows, string sort, string order)
        {
            var students = db.Student.ToList();

            if (!string.IsNullOrEmpty(sno))
            {
                students = students.Where(s => s.Sno.Contains(sno)).ToList();
            }

            if (!string.IsNullOrEmpty(ssex))
            {
                students = students.Where(s => s.Ssex.Contains(ssex)).ToList();
            }

            if (!string.IsNullOrEmpty(sdept))
            {
                students = students.Where(s => s.Sdept == sdept).ToList();
            }

            if (!string.IsNullOrEmpty(sort) && !string.IsNullOrEmpty(order))
            {
                students = students.OrderBy($"{sort} {order}").ToList();
            }

            return JsonConvert.SerializeObject(students.ToList());
        }



        // GET: Student/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        [Bind(Include = "Id,Sno,Sname,Ssex,Sage,Sdept")]
        public ActionResult Create(Student student)
        {
            //            if (ModelState.IsValid)
            //            {
            //                db.Student.Add(student);
            //                db.SaveChanges();
            //                return RedirectToAction("Index");
            //            }
            //
            //            return View(student);
            db.Student.Add(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Student/Edit/5
        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id != null)
            {
                var student = db.Student.First(s => s.Id.ToString() == id);
                return View(student);
            } //死循环了
            return View();
        }


        // POST: Student/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(List<string> ids)
        {
            foreach (var id in ids)
            {
                var student = db.Student.First(s => s.Sno == id);
                db.Student.Remove(student);
            }

            db.SaveChanges();
            return RedirectToAction("Index", "Student");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}