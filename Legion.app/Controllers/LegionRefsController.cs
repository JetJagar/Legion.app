using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Legion.app.Models;

namespace Legion.app.Controllers
{

    [CustomAuthorize (Roles = "Chapter_Master")]

    public class LegionRefsController : Controller
    {
        private SpaceMarine_dbEntities1 db = new SpaceMarine_dbEntities1();

        // GET: LegionRefs
        public ActionResult Index()
        {
            var legionRefs = db.LegionRefs.Include(l => l.LegionDetail);
            return View(legionRefs.ToList());
        }

        // GET: LegionRefs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LegionRef legionRef = db.LegionRefs.Find(id);
            if (legionRef == null)
            {
                return HttpNotFound();
            }
            return View(legionRef);
        }

        // GET: LegionRefs/Create
        public ActionResult Create()
        {
            ViewBag.Ref_Legion_ID = new SelectList(db.LegionDetails, "Legion_ID", "Legion_Name");
            return View();
        }

        // POST: LegionRefs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ref_Legion_ID,Legion_Primarch_Name,Loyal_or_Heretic")] LegionRef legionRef)
        {
            if (ModelState.IsValid)
            {
                db.LegionRefs.Add(legionRef);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Ref_Legion_ID = new SelectList(db.LegionDetails, "Legion_ID", "Legion_Name", legionRef.Ref_Legion_ID);
            return View(legionRef);
        }

        // GET: LegionRefs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LegionRef legionRef = db.LegionRefs.Find(id);
            if (legionRef == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ref_Legion_ID = new SelectList(db.LegionDetails, "Legion_ID", "Legion_Name", legionRef.Ref_Legion_ID);
            return View(legionRef);
        }

        // POST: LegionRefs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ref_Legion_ID,Legion_Primarch_Name,Loyal_or_Heretic")] LegionRef legionRef)
        {
            if (ModelState.IsValid)
            {
                db.Entry(legionRef).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Ref_Legion_ID = new SelectList(db.LegionDetails, "Legion_ID", "Legion_Name", legionRef.Ref_Legion_ID);
            return View(legionRef);
        }

        // GET: LegionRefs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LegionRef legionRef = db.LegionRefs.Find(id);
            if (legionRef == null)
            {
                return HttpNotFound();
            }
            return View(legionRef);
        }

        // POST: LegionRefs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LegionRef legionRef = db.LegionRefs.Find(id);
            db.LegionRefs.Remove(legionRef);
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
