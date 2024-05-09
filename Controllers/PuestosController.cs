using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Papeleria.Models;
using EntityState = System.Data.Entity.EntityState;

namespace Papeleria.Controllers
{
    public class PuestosController : Controller
    {
        private DbModels db = new DbModels();

        // GET: Puestos
        public ActionResult Index()
        {
            var puestos = db.Puestos.Include(p => p.Departamentos);
            return View(puestos.ToList());
        }

        // GET: Puestos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Puestos puestos = db.Puestos.Find(id);
            if (puestos == null)
            {
                return HttpNotFound();
            }
            return View(puestos);
        }

        // GET: Puestos/Create
        public ActionResult Create()
        {
            ViewBag.ID_Departamento = new SelectList(db.Departamentos, "ID_Departamento", "Nombre");
            return View();
        }

        // POST: Puestos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Puesto,ID_Departamento,Nombre,Descripcion,Salario_Base")] Puestos puestos)
        {
            if (ModelState.IsValid)
            {
                if (puestos.Salario_Base < 300)
                {
                    ModelState.AddModelError("Salario_Base", "El salario no puede ser menor a $300.");
                    ViewBag.ID_Departamento = new SelectList(db.Departamentos, "ID_Departamento", "Nombre", puestos.ID_Departamento);
                    return View(puestos);
                }

                db.Puestos.Add(puestos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Departamento = new SelectList(db.Departamentos, "ID_Departamento", "Nombre", puestos.ID_Departamento);
            return View(puestos);
        }


        // GET: Puestos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Puestos puestos = db.Puestos.Find(id);
            if (puestos == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Departamento = new SelectList(db.Departamentos, "ID_Departamento", "Nombre", puestos.ID_Departamento);
            return View(puestos);
        }

        // POST: Puestos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Puesto,ID_Departamento,Nombre,Descripcion,Salario_Base")] Puestos puestos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(puestos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Departamento = new SelectList(db.Departamentos, "ID_Departamento", "Nombre", puestos.ID_Departamento);
            return View(puestos);
        }

        // GET: Puestos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Puestos puestos = db.Puestos.Find(id);
            if (puestos == null)
            {
                return HttpNotFound();
            }
            return View(puestos);
        }

        // POST: Puestos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Puestos puestos = db.Puestos.Find(id);
            db.Puestos.Remove(puestos);
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
