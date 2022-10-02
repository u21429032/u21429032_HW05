using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1;

namespace WebApplication1.Controllers
{
    public class borrowsController : Controller
    {
        private LibraryEntities db = new LibraryEntities();

        // GET: borrows
        public ActionResult Index(int id)
        {
            var borrowed = db.borrows.Where(a => a.bookId == id).Include(b => b.book).Include(b => b.student);
            var book = db.books.FirstOrDefault(a => a.bookId == id);
            ViewBag.BookName = book.name;
            var available = db.borrows.FirstOrDefault(a => a.bookId == id);

            if(available.broughtDate != null)
            {
                ViewBag.Available = "Book available";
            }
            else
            {
                ViewBag.Available = "Book out";
            }
            var borrows = db.borrows.Include(b => b.book).Include(b => b.student);
            return View(borrowed.ToList());
        }

        // GET: borrows/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            borrow borrow = db.borrows.Find(id);
            if (borrow == null)
            {
                return HttpNotFound();
            }
            return View(borrow);
        }

        // GET: borrows/Create
        public ActionResult Create()
        {
            ViewBag.bookId = new SelectList(db.books, "bookId", "name");
            ViewBag.studentId = new SelectList(db.students, "studentId", "name");
            return View();
        }

        // POST: borrows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "borrowId,studentId,bookId,takenDate,broughtDate")] borrow borrow)
        {
            if (ModelState.IsValid)
            {
                db.borrows.Add(borrow);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.bookId = new SelectList(db.books, "bookId", "name", borrow.bookId);
            ViewBag.studentId = new SelectList(db.students, "studentId", "name", borrow.studentId);
            return View(borrow);
        }

        // GET: borrows/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            borrow borrow = db.borrows.Find(id);
            if (borrow == null)
            {
                return HttpNotFound();
            }
            ViewBag.bookId = new SelectList(db.books, "bookId", "name", borrow.bookId);
            ViewBag.studentId = new SelectList(db.students, "studentId", "name", borrow.studentId);
            return View(borrow);
        }

        // POST: borrows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "borrowId,studentId,bookId,takenDate,broughtDate")] borrow borrow)
        {
            if (ModelState.IsValid)
            {
                db.Entry(borrow).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bookId = new SelectList(db.books, "bookId", "name", borrow.bookId);
            ViewBag.studentId = new SelectList(db.students, "studentId", "name", borrow.studentId);
            return View(borrow);
        }

        // GET: borrows/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            borrow borrow = db.borrows.Find(id);
            if (borrow == null)
            {
                return HttpNotFound();
            }
            return View(borrow);
        }

        // POST: borrows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            borrow borrow = db.borrows.Find(id);
            db.borrows.Remove(borrow);
            db.SaveChanges();
            return RedirectToAction("Index");
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
