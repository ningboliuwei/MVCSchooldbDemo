﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSchooldbDemo.Classes;
using MVCSchooldbDemo.Common;
using MVCSchooldbDemo.Models.Data;
using MVCSchooldbDemo.Views.Student;

namespace MVCSchooldbDemo.Controllers
{
    public class StudentController : Controller
    {
        private readonly SchooldDbContext _db = new SchooldDbContext();

        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public string GetList(string queryParasString, int page, int rows, string sort, string order)
        {
            var result = DBHelper.GetResult(_db.Students.ToList(), queryParasString, page, rows, sort, order);

            ViewBag.Ids = DBHelper.GetListFromResultString<StudentInfo, long>(s => s.Id, result);

            return result;
        }

        // GET: Student/Details/5
        public ActionResult Details(long? id)
        {
            if (id != null)
            {
                ViewBag.DialogTitle = "查看学生明细";
                var student = DBHelper.FindByKeyword(_db.Students.ToList(), "Id", id).First();

                return View(student);
            }

            return HttpNotFound();
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            ViewBag.DialogTitle = "添加学生记录";
            return View();
        }

        // POST: Student/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        [Bind(Include = "Id,Sno,Sname,Ssex,Sage,Sdept")]
        public ActionResult Create(StudentInfo student)
        {
            if (ModelState.IsValid)
            {
                _db.Students.Add(student);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
            //            _db.SaveChanges();
            //            return RedirectToAction("Index");
        }

        // GET: Student/Edit/5
        [HttpGet]
        public ActionResult Edit(long? id)
        {
            if (id != null)
            {
                var student = DBHelper.FindByKeyword(_db.Students.ToList(), "Id", id).First();
                ViewBag.DialogTitle = "编辑学生记录";
                return View(student);
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

        public ActionResult UploadPhoto(HttpPostedFileBase fileData)
        {
            var fileInfo = UploadFileHelper.Upload(fileData, "Photos/");
            return new JsonResult {Data = fileInfo};
        }
    }
}