using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhoneApplication.Models;

namespace PhoneApplication.Controllers
{
    public class PhonesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Phones
        public ActionResult Index()
        {
            var phone = db.Phone.Include(p => p.Manufacturer);
            return View(phone.ToList());
        }

        // GET: Phones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = db.Phone.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            return View(phone);
        }

        // GET: Phones/Create
        public ActionResult Create()
        {
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerID", "ManufacturerName");
            return View();
        }

        // POST: Phones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PhoneID,PhoneName,ManufacturerID,MSRP,ScreenSize,DateReleased")] Phone phone)
        {
            if (ModelState.IsValid)
            {
                db.Phone.Add(phone);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerID", "ManufacturerName", phone.ManufacturerID);
            return View(phone);
        }

        // GET: Phones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = db.Phone.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerID", "ManufacturerName", phone.ManufacturerID);
            return View(phone);
        }

        // POST: Phones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhoneID,PhoneName,ManufacturerID,MSRP,ScreenSize,DateReleased")] Phone phone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerID", "ManufacturerName", phone.ManufacturerID);
            return View(phone);
        }

        // GET: Phones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = db.Phone.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            return View(phone);
        }

        // POST: Phones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Phone phone = db.Phone.Find(id);
            db.Phone.Remove(phone);
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
