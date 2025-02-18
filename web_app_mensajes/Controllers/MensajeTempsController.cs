using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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
            //Enviar_Whatsapps();
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

            var telefono = mensajeTemp.telefono;

            Enviar_Whatsapps(telefono);

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
        static void Enviar_Whatsapps(string tel)
        {
            string url = "https://graph.facebook.com/v22.0/569259382937171/messages"; // Endpoint de Meta API
            string token = "EAASYXrmdB08BO1auQOFJQzqAq3zySezgHUG4Q2KNrcvLlYM9xZCqtRSg9b1DZBSfBUZBCrh12zR6IJwSSJZCCSvgjhirq7J3HPosQzXGN5OGTs0llgzZAf0TxAO8I9zTfpck1xcDudtvVx1dlC4bq5fsZCkyxBcRBMa0mJz0R6VgWo5slUm2egLpq6gbS92MclKoBTcxO0TX6TWsLURfMEqKZC0ZCGER5jvEBngu89dD";
            string toNumber = "+541132542221"; // Número de destino con código de país
            string jsonData = "{ \"messaging_product\": \"whatsapp\", \"to\": \"" + "+" + tel + "\", \"type\": \"template\", \"template\": { \"name\": \"hello_world\", \"language\": { \"code\": \"en_US\" } } }";

            try
            {

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                // Crear la solicitud HTTP POST
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Headers["Authorization"] = "Bearer " + token;

                // Convertir el JSON a bytes
                byte[] byteArray = Encoding.UTF8.GetBytes(jsonData);
                request.ContentLength = byteArray.Length;

                // Escribir el JSON en la solicitud
                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }

                // Obtener la respuesta
                using (WebResponse response = request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string responseText = reader.ReadToEnd();
                    Console.WriteLine("Respuesta de WhatsApp API: " + responseText);
                }
            }
            catch (WebException ex)
            {
                using (StreamReader reader = new StreamReader(ex.Response.GetResponseStream()))
                {
                    string errorResponse = reader.ReadToEnd();
                    Console.WriteLine("Error: " + errorResponse);
                }
            }
        }
    }
}
