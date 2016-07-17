using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCSchooldbDemo;
using Newtonsoft.Json;

namespace MVCSchooldbDemo.Controllers
{
    public class StudentController : Controller
    {
        private SchooldbEntities db = new SchooldbEntities();

        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public string GetAll()
        {
            return JsonConvert.SerializeObject(db.Student.ToList());
        }

        // GET: Student/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
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
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Sno,Sname,Ssex,Sage,Sdept")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        [HttpPost]
        public ActionResult Delete(List<string> ids)
        {
            foreach (string id in ids)
            {
                Student student = db.Student.First(s => s.Sno == id);
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