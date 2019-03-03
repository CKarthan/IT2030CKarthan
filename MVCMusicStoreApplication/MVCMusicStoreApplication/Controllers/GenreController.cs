using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCMusicStoreApplication.Models;
using System.Net;
using System.Data.Entity;

namespace MVCMusicStoreApplication.Controllers {
    public class GenreController : Controller {
        private MVCMusicStoreDB db = new MVCMusicStoreDB();
        // GET: Genre
        public ActionResult Index() {
            var genres = db.Genres.ToList(); 
            return View(genres);
        }


        // GET: Genre/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genres.Find(id);
            if (genre == null) {
                return HttpNotFound();
            }
            return View(genre);
        }

        // GET: Genre/Create
        public ActionResult Create() {
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name");
            return View();
        }

        // POST: Genre/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GenreId,Name,Description")] Genre genre) {
            if (ModelState.IsValid) {
                db.Genres.Add(genre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", genre.GenreId);

            return View(genre);
        }

        // GET: Genre/Update/
        public ActionResult Update(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genres.Find(id);
            if (genre == null) {
                return HttpNotFound();
            }

            return View("Update", genre);
        }

        // POST: Genre/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Include = "GenreId,Name,Description")] Genre genre) {
            if (ModelState.IsValid) {
                db.Entry(genre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", genre.GenreId);
            return View("Update", genre);
        }

        // GET: Genre/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genres.Find(id);
            if (genre == null) {
                return HttpNotFound();
            }
            return View(genre);
        }

        // POST: Genre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Genre genre = db.Genres.Find(id);
            db.Genres.Remove(genre);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}