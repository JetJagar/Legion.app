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

    [CustomAuthorize (Roles = "Chapter_Master, Battle_Bro") ]


    public class LegionDetailsController : Controller
    {
        private SpaceMarine_dbEntities1 db = new SpaceMarine_dbEntities1();

        // GET: LegionDetails
        public ActionResult Index()
        {
            var legionDetails = db.LegionDetails.Include(l => l.LegionRef);
            return View(legionDetails.ToList());
        }

        // GET: LegionDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LegionDetail legionDetail = db.LegionDetails.Find(id);
            if (legionDetail == null)
            {
                return HttpNotFound();
            }
            return View(legionDetail);
        }

        // GET: LegionDetails/Create
        public ActionResult Create()
        {
            ViewBag.Legion_ID = new SelectList(db.LegionRefs, "Ref_Legion_ID", "Legion_Primarch_Name");
            return View();
        }

        // POST: LegionDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Legion_ID,Legion_Name,Legion_Color,Primarch_Status")] LegionDetail legionDetail)
        {
            if (ModelState.IsValid)
            {
                db.LegionDetails.Add(legionDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Legion_ID = new SelectList(db.LegionRefs, "Ref_Legion_ID", "Legion_Primarch_Name", legionDetail.Legion_ID);
            return View(legionDetail);
        }

        // GET: LegionDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LegionDetail legionDetail = db.LegionDetails.Find(id);
            if (legionDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.Legion_ID = new SelectList(db.LegionRefs, "Ref_Legion_ID", "Legion_Primarch_Name", legionDetail.Legion_ID);
            return View(legionDetail);
        }

        // POST: LegionDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Legion_ID,Legion_Name,Legion_Color,Primarch_Status")] LegionDetail legionDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(legionDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Legion_ID = new SelectList(db.LegionRefs, "Ref_Legion_ID", "Legion_Primarch_Name", legionDetail.Legion_ID);
            return View(legionDetail);
        }

        // GET: LegionDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LegionDetail legionDetail = db.LegionDetails.Find(id);
            if (legionDetail == null)
            {
                return HttpNotFound();
            }
            return View(legionDetail);
        }

        // POST: LegionDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LegionDetail legionDetail = db.LegionDetails.Find(id);
            db.LegionDetails.Remove(legionDetail);
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
