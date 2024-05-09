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
    public class Evaluaciones_DesempenoController : Controller
    {
        private DbModels db = new DbModels();

        // GET: Evaluaciones_Desempeno
        public ActionResult Index()
        {
            // Obtiene la evaluación más reciente para cada empleado
            var evaluaciones = db.Evaluaciones_Desempeno
                .GroupBy(e => e.ID_Empleado)
                .Select(g => g.OrderByDescending(e => e.Mes_Evaluacion).FirstOrDefault())
                .ToList();

            return View(evaluaciones);
        }

        // GET: Evaluaciones_Desempeno/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluaciones_Desempeno evaluaciones_Desempeno = db.Evaluaciones_Desempeno.Find(id);
            if (evaluaciones_Desempeno == null)
            {
                return HttpNotFound();
            }
            return View(evaluaciones_Desempeno);
        }

        public ActionResult Create()
        {
            // Crea una lista de SelectListItem para las calificaciones
            var calificaciones = Enumerable.Range(0, 11).Select(i => new SelectListItem
            {
                Value = i.ToString(),
                Text = i.ToString()
            }).ToList();

            // Pasa la lista a la vista
            ViewBag.Calificacion = calificaciones;

            // Obtén la lista de empleados de tu base de datos
            var empleados = db.Empleados.ToList();

            // Crea una lista de SelectListItem
            var listaEmpleados = empleados.Select(e => new SelectListItem
            {
                Value = e.ID_Empleado.ToString(),
                Text = e.Nombre + " " + e.Apellido_Paterno
            }).ToList();

            // Pasa la lista a la vista
            ViewBag.ID_Empleado = listaEmpleados;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Evaluacion,ID_Empleado,Mes_Evaluacion,Calificacion,Comentarios")] Evaluaciones_Desempeno evaluaciones_Desempeno)
        {
            // Verifica si ya existe una evaluación para este empleado en el mes dado
            var evaluacionExistente = db.Evaluaciones_Desempeno
                .Any(e => e.ID_Empleado == evaluaciones_Desempeno.ID_Empleado &&
                          e.Mes_Evaluacion.Value.Year == evaluaciones_Desempeno.Mes_Evaluacion.Value.Year &&
                          e.Mes_Evaluacion.Value.Month == evaluaciones_Desempeno.Mes_Evaluacion.Value.Month &&
                          e.Mes_Evaluacion.Value.Day >= 1 && e.Mes_Evaluacion.Value.Day <= 5);

            if (evaluacionExistente)
            {
                // Si ya existe una evaluacion en los primeros 5 días, agrega un error al modelo
                ModelState.AddModelError("Mes_Evaluacion", $"Ya se realizó la evaluación correspondiente al mes de {evaluaciones_Desempeno.Mes_Evaluacion.Value.ToString("MMMM")} del año {evaluaciones_Desempeno.Mes_Evaluacion.Value.Year}, y solo se puede realizar del día 1 al 5 de cada mes");
            }
            else if (evaluaciones_Desempeno.Mes_Evaluacion.Value.Day > 5)
            {
                // Si el día ingresado es mayor a 5, agrega un error al modelo
                ModelState.AddModelError("Mes_Evaluacion", "La evaluación solo se puede realizar del día 1 al 5 de cada mes");
            }

            // Crea una lista de SelectListItem para las calificaciones
            var calificaciones = Enumerable.Range(0, 11).Select(i => new SelectListItem
            {
                Value = i.ToString(),
                Text = i.ToString()
            }).ToList();

            // Pasa la lista a la vista
            ViewBag.Calificacion = calificaciones;

            // Obtén la lista de empleados de tu base de datos
            var empleados = db.Empleados.ToList();

            // Crea una lista de SelectListItem
            var listaEmpleados = empleados.Select(e => new SelectListItem
            {
                Value = e.ID_Empleado.ToString(),
                Text = e.Nombre + " " + e.Apellido_Paterno
            }).ToList();

            // Pasa la lista a la vista
            ViewBag.ID_Empleado = listaEmpleados;

            return View(evaluaciones_Desempeno);
        }



        // GET: Evaluaciones_Desempeno/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluaciones_Desempeno evaluaciones_Desempeno = db.Evaluaciones_Desempeno.Find(id);
            if (evaluaciones_Desempeno == null)
            {
                return HttpNotFound();
            }

            // Crea una lista de SelectListItem para los empleados
            var empleados = db.Empleados.Select(e => new SelectListItem
            {
                Value = e.ID_Empleado.ToString(),
                Text = e.Nombre + " " + e.Apellido_Paterno
            }).ToList();

            // Pasa la lista a la vista
            ViewBag.ID_Empleado = new SelectList(empleados, "Value", "Text", evaluaciones_Desempeno.ID_Empleado);

            // Crea una lista de SelectListItem para las calificaciones
            var calificaciones = Enumerable.Range(0, 11).Select(i => new SelectListItem
            {
                Value = i.ToString(),
                Text = i.ToString()
            }).ToList();

            // Pasa la lista a la vista
            ViewBag.Calificacion = new SelectList(calificaciones, "Value", "Text", evaluaciones_Desempeno.Calificacion.ToString());


            return View(evaluaciones_Desempeno);
        }

        // POST: Evaluaciones_Desempeno/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Evaluacion,ID_Empleado,Mes_Evaluacion,Calificacion,Comentarios")] Evaluaciones_Desempeno evaluaciones_Desempeno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evaluaciones_Desempeno).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Crea una lista de SelectListItem para los empleados
            var empleados = db.Empleados.Select(e => new SelectListItem
            {
                Value = e.ID_Empleado.ToString(),
                Text = e.Nombre + " " + e.Apellido_Paterno
            }).ToList();

            // Pasa la lista a la vista
            ViewBag.ID_Empleado = new SelectList(empleados, "Value", "Text", evaluaciones_Desempeno.ID_Empleado);

            // Crea una lista de SelectListItem para las calificaciones
            var calificaciones = Enumerable.Range(0, 11).Select(i => new SelectListItem
            {
                Value = i.ToString(),
                Text = i.ToString()
            }).ToList();

            // Pasa la lista a la vista
            ViewBag.Calificacion = new SelectList(calificaciones, "Value", "Text", evaluaciones_Desempeno.Calificacion.ToString());

            return View(evaluaciones_Desempeno);
        }


        // GET: Evaluaciones_Desempeno/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluaciones_Desempeno evaluaciones_Desempeno = db.Evaluaciones_Desempeno.Find(id);
            if (evaluaciones_Desempeno == null)
            {
                return HttpNotFound();
            }
            return View(evaluaciones_Desempeno);
        }

        // POST: Evaluaciones_Desempeno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Evaluaciones_Desempeno evaluaciones_Desempeno = db.Evaluaciones_Desempeno.Find(id);
            db.Evaluaciones_Desempeno.Remove(evaluaciones_Desempeno);
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
