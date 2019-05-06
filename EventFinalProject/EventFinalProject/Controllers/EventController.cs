using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EventFinalProject.Models;

namespace EventFinalProject.Controllers
{
    public class EventController : Controller
    {
        private EventFinalProjectDB db = new EventFinalProjectDB();

        public ActionResult Index()
        {
            IQueryable<Event> events = db.Events.Include(e => e.Category).Include(e => e.Organizer);
            return View("Index",events.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event myEvent = db.Events.Find(id);
            if (myEvent == null)
            {
                return HttpNotFound();
            }
            return View(myEvent);
        }

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            ViewBag.OrganizerId = new SelectList(db.Organizers, "OrganizerId", "LastName");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventId,CategoryId,Category,Organizer,OrganizerId,EventIcon,Title,Description,AddressLine1,AddressLine2,City,State,Zipcode,StartDateTime,EndDateTime,MaxTickets,AvailableTickets,Price")] Event myEvent)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(myEvent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", myEvent.CategoryId);
            ViewBag.OrganizerId = new SelectList(db.Organizers, "OrganizerId", "FirstName", myEvent.OrganizerId);
            return View(myEvent);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event oneEvent = db.Events.Find(id);
            if (oneEvent == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", oneEvent.CategoryId);
            ViewBag.OrganizerId = new SelectList(db.Organizers, "OrganizerId", "FirstName", oneEvent.OrganizerId);
            return View(oneEvent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventId,CategoryId,OrganizerId,EventIcon,Title,Description,AddressLine1,AddressLine2,City,State,Zipcode,StartDateTime,EndDateTime,MaxTickets,AvailableTickets,Price")] Event oneEvent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oneEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", oneEvent.CategoryId);
            ViewBag.OrganizerId = new SelectList(db.Organizers, "OrganizerId", "FirstName", oneEvent.OrganizerId);
            return View(oneEvent);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event oneEvent = db.Events.Find(id);
            if (oneEvent == null)
            {
                return HttpNotFound();
            }
            return View(oneEvent);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event oneEvent = db.Events.Find(id);
            db.Events.Remove(oneEvent);
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

        public ActionResult EventSearchByName(string keyword) {
            var events = db.Events.Where(e => e.Category.Name.Contains(keyword) || e.Title.Contains(keyword)).ToList();
            return PartialView("_EventSearch", events.OrderBy(e => e.Title));
        }

        public ActionResult EventSearchByLocation(string keyword) {
            var events = db.Events.Where(e => e.City.Contains(keyword) || e.State.Contains(keyword)).ToList();
            return PartialView("_EventSearch", events.OrderBy(e => e.Title));
        }

        public ActionResult LastMinuteDeals() {
            var allEvents = db.Events.ToList();
            List<Event> lastMinuteDeals = new List<Event>();
            foreach(var oneEvent in allEvents) {
                if(DateTime.Now < oneEvent.StartDateTime && DateTime.Now.AddDays(2) > oneEvent.StartDateTime) {
                    lastMinuteDeals.Add(oneEvent);
                }
            }

            return PartialView("_LastMinuteDeals", lastMinuteDeals.OrderBy(e => e.Title));
        }
    }
}
