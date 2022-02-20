using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Charts.Models;
using Charts.Services;
using FilterRLC.Models;

namespace FilterRLC.Controllers
{
    public class FilterController : Controller
    {
        private FilterDbContext db = new FilterDbContext();

        // GET: Filter
        public ActionResult Index()
        {
            return View(db.SetOfFilters.ToList());
        }

        // GET: Filter/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FilterModel filterModel = db.SetOfFilters.Find(id);
            if (filterModel == null)
            {
                return HttpNotFound();
            }
            return View(filterModel);
        }

        // GET: Filter/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Filter/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Resistance1,Resistance2,Inductance,Capacitance,U1,Fmin,Fmax,NumOfRows")] FilterModel filterModel)
        {
            if (ModelState.IsValid)
            {
                db.SetOfFilters.Add(filterModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(filterModel);
        }

        // GET: Filter/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FilterModel filterModel = db.SetOfFilters.Find(id);
            if (filterModel == null)
            {
                return HttpNotFound();
            }
            return View(filterModel);
        }

        // POST: Filter/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Resistance1,Resistance2,Inductance,Capacitance,U1,Fmin,Fmax,NumOfRows")] FilterModel filterModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(filterModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(filterModel);
        }

        // GET: Filter/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FilterModel filterModel = db.SetOfFilters.Find(id);
            if (filterModel == null)
            {
                return HttpNotFound();
            }
            return View(filterModel);
        }

        // POST: Filter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FilterModel filterModel = db.SetOfFilters.Find(id);
            db.SetOfFilters.Remove(filterModel);
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

        public ActionResult CreateWaveform(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FilterModel filter = db.SetOfFilters.Find(id);

            if (filter == null)
            {
                return HttpNotFound();
            }

            var calculationService = new TransmittanceService();

            var dataForChart = calculationService.GetTransmittance(new Charts.Models.FilterModel()
            {
                Fmax = filter.Fmax,
                Capacitance = filter.Capacitance,
                Fmin = filter.Fmin,
                ID = filter.ID,
                Inductance = filter.Inductance,
                NumOfRows = filter.NumOfRows,
                Resistance1 = filter.Resistance1,
                Resistance2 = filter.Resistance2,
                U1 = filter.U1
            });

            var drawingService = new ChartService();
            var ms = drawingService.CreateWaveform(dataForChart);

            return File(ms.GetBuffer(), "image/png");
        }
    }
}
