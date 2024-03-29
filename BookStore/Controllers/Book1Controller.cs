﻿using BookStore.Filters;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    [MyAuthorize]
    public class Book1Controller : Controller
    {
        //private IBookRepository _service;
        private BookService _service;
        public Book1Controller()
        {
            _service = new BookService();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(int id)
        {
            ViewBag.id = id;
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            ViewBag.id = id;
            return View();
        }
        public ActionResult Delete(int id)
        {
            ViewBag.id = id;
            return View();
        }

        //Web Api -----------
        [HttpPost]
        public JsonResult GetAllBooks()
        {
            var ret = _service.GetAll();
            return Json(ret);
        }
        [HttpPost]
        //[ValidateAnti_MyForgeryToken]
        public JsonResult GetBook(int id)
        {
            var ret = _service.Get(id);
            return Json(ret);
        }
        [HttpPost]
        public JsonResult CreateBook(Book book)
        {
            var ret = _service.Insert(book);
            return Json(ret);
        }
        [HttpPost]
        public JsonResult UpdateBook(Book book)
        {
            var ret = _service.Update(book);
            return Json(ret);
        }
        [HttpPost]
        public ActionResult DeleteBook(int id)
        {
            var ret = _service.Delete(id);
            return Json(ret);
        }
        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            base.Dispose(disposing);
        }
    }
}
