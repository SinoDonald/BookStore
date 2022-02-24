using BookStore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        //private IBookRepository _service;
        private IBookService _service;
        public BookController()
        {
            //_service = new BookService(); < -- 移除相依性
            _service = new BookService(new BookDBRepository());
        }
        public ActionResult Index()
        {
            var ret = _service.GetAll();
            return View(ret);
        }
        public ActionResult Details(int id)
        {
            var ret = _service.Get(id);
            return View(ret);
        }
        public ActionResult Create()
        {
            return View(new Book());
        }
        [HttpPost]
        public ActionResult Create(Book book)
        {
            bool ret = _service.Insert(book);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var ret = _service.Get(id);
            return View(ret);
        }
        [HttpPost]
        public ActionResult Edit(Book book)
        {
            bool ret = _service.Update(book);
            return RedirectToAction("Index", new { id = book.ID.ToString() });
        }
        public ActionResult Delete(int id)
        {
            return View(id);
        }
        [HttpPost]
        public ActionResult Delete(int id, string confirm)
        {
            var ret = _service.Delete(id);
            if (!ret) return RedirectToAction("Details", new { id = id });
            return RedirectToAction("Index");
        }
        // 很重要, 不要忘記
        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            base.Dispose(disposing);
        }
    }
}
