﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Linq.Dynamic;
using MVCSchooldbDemo.Classes;
using LinqKit;

namespace MVCSchooldbDemo.Controllers
{
    public class StudentController : Controller
    {
        private readonly SchooldbEntities _db = new SchooldbEntities();

        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public string GetList(string sno, string ssex, string sdept, int page, int rows, string sort, string order)
        {
            var list = _db.Student.ToList();

            list = DBHelper.FilterByKeywords(list, new[] { "Sno", "Ssex", "Sdept" }, new[] { sno, ssex, sdept });

            return DBHelper.SortingAndPaging(list, page, rows, sort, order);
        }

        // GET: Student/Details/5
        public ActionResult Details(long? id)
        {
            if (id != null)
            {
                ViewBag.DialogTitle = "查看学生明细";
                var student = DBHelper.FindByKeyword(_db.Student.ToList(), "Id", id).First();

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
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _db.Student.Add(student);
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
                var student = DBHelper.FindByKeyword(_db.Student.ToList(), "Id", id).First();
                ViewBag.DialogTitle = "编辑学生记录";
                return View(student);
            } //死循环了

            return HttpNotFound();
        }


        // POST: Student/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        public ActionResult Edit(Student student)
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
                var student = DBHelper.FindByKeyword(_db.Student.ToList(), "Id", id).First();
                _db.Student.Remove(student);
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