using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using web_app_mensajes;

namespace web_app_mensajes.Controllers
{
    public class MensajeTempsController : Controller
    {
        private BdCrecerSyAEntities db = new BdCrecerSyAEntities();

        // GET: MensajeTemps
        public ActionResult Index()
        {
            return View(db.MensajeTemp.ToList());
        }

        // GET: MensajeTemps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MensajeTemp mensajeTemp = db.MensajeTemp.Find(id);
            if (mensajeTemp == null)
            {
                return HttpNotFound();
            }
            return View(mensajeTemp);
        }

        // GET: MensajeTemps/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MensajeTemps/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "iD,telefono,apellido")] MensajeTemp mensajeTemp)
        {
            if (ModelState.IsValid)
            {
                db.MensajeTemp.Add(mensajeTemp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mensajeTemp);
        }

        // GET: MensajeTemps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MensajeTemp mensajeTemp = db.MensajeTemp.Find(id);
            if (mensajeTemp == null)
            {
                return HttpNotFound();
            }
            return View(mensajeTemp);
        }

        // POST: MensajeTemps/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "iD,telefono,apellido")] MensajeTemp mensajeTemp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mensajeTemp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mensajeTemp);
        }

        // GET: MensajeTemps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MensajeTemp mensajeTemp = db.MensajeTemp.Find(id);
            if (mensajeTemp == null)
            {
                return HttpNotFound();
            }
            return View(mensajeTemp);
        }

        // POST: MensajeTemps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MensajeTemp mensajeTemp = db.MensajeTemp.Find(id);
            db.MensajeTemp.Remove(mensajeTemp);
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
