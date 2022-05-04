using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCdbdemo;

namespace MVCdbdemo.Controllers
{
    public class modelsController : Controller
    {
        private vehiclesEntities db = new vehiclesEntities();
        
        // GET: models
        public ActionResult Index(string searchby, string search)
        {

            if(searchby == "Model")
            {
                var models = db.models.Where(m => m.modelName.Contains(search) || search == null);
                return View(models.ToList());
            }
            else if(searchby == "Company")
            {
                var models = db.models.Where(m => m.company.companyName.Contains(search) || search == null);
                return View(models.ToList());
            }
            var getmodels = db.models.Include(m => m.company);
            return View(getmodels.ToList());
        }

        // GET: models/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model model = db.models.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: models/Create
        public ActionResult Create()
        {
            ViewBag.companyID = new SelectList(db.companies, "companyID", "companyName");
            return View();
        }

        // POST: models/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "modelID,modelName,companyID")] model model)
        {
            if (ModelState.IsValid)
            {
                db.models.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.companyID = new SelectList(db.companies, "companyID", "companyName", model.companyID);
            return View(model);
        }

        // GET: models/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model model = db.models.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.companyID = new SelectList(db.companies, "companyID", "companyName", model.companyID);
            return View(model);
        }

        // POST: models/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "modelID,modelName,companyID")] model model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.companyID = new SelectList(db.companies, "companyID", "companyName", model.companyID);
            return View(model);
        }

        // GET: models/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model model = db.models.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: models/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            model model = db.models.Find(id);
            db.models.Remove(model);
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
