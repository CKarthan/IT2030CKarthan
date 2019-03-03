using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCMusicStoreApplication.Models;
using System.Net;
using System.Data.Entity;

namespace MVCMusicStoreApplication.Controllers
{
    public class ArtistController : Controller
    {
        private MVCMusicStoreDB db = new MVCMusicStoreDB();
        // GET: Artist
        public ActionResult Index(){
            var artists = db.Artists.ToList();
            return View(artists);
        }


        // GET: Artists/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Find(id);
            if (artist == null) {
                return HttpNotFound();
            }
            return View(artist);
        }

        // GET: Artist/Create
        public ActionResult Create() {
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name");
            return View();
        }

        // POST: Artist/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArtistId,Name")] Artist artist ){
            if (ModelState.IsValid) {
                db.Artists.Add(artist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", artist.ArtistId);
           
            return View(artist);
        }

        // GET: Artist/Update/
        public ActionResult Update(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Find(id);
            if (artist == null) {
                return HttpNotFound();
            }
           
            return View("Update", artist);
        }

        // POST: Artist/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Include = "ArtistId,Name")] Artist artist) {
            if (ModelState.IsValid) {
                db.Entry(artist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", artist.ArtistId);
            return View("Update", artist);
        }

        // GET: Artist/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Find(id);
            if (artist == null) {
                return HttpNotFound();
            }
            return View(artist);
        }

        // POST: Artist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Artist artist = db.Artists.Find(id);
            db.Artists.Remove(artist);
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