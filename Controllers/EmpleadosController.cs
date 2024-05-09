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
    public class EmpleadosController : Controller
    {
        private DbModels db = new DbModels();

        // GET: Empleados
        public ActionResult Index()
        {
            var empleados = db.Empleados.Include(e => e.Departamentos).Include(e => e.Puestos).Include(e => e.Horarios);
            return View(empleados.ToList());
        }

        // GET: Empleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {
            ViewBag.ID_Departamento = new SelectList(db.Departamentos, "ID_Departamento", "Nombre");
            ViewBag.ID_Puesto = new SelectList(db.Puestos, "ID_Puesto", "Nombre");
            ViewBag.ID_Turno = new SelectList(db.Horarios, "ID_Turno", "Nombre_Turno");

            // Crea una lista de SelectListItem para el género
            var generos = new List<SelectListItem>
            {
                new SelectListItem { Value = "H", Text = "Hombre" },
                new SelectListItem { Value = "M", Text = "Mujer" }
            };

            // Pasa la lista a la vista
            ViewBag.Genero = generos;


            return View();
        }


        // POST: Empleados/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Empleado,Nombre,Apellido_Paterno,Apellido_Materno,Fecha_Nacimiento,Genero,Direccion,Ciudad,Pais,Telefono,Correo_Electronico,Fecha_Contratacion,Salario,ID_Departamento,ID_Puesto,ID_Turno")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                // Verifica si hay empleados en el mismo turno y puesto
                var empleadoExistente = db.Empleados.Any(e => e.ID_Turno == empleados.ID_Turno && e.ID_Puesto == empleados.ID_Puesto);

                if (empleadoExistente)
                {
                    // Muestra un mensaje de error o tomar la acción adecuada
                    ModelState.AddModelError("", "Ya existe un empleado en este puesto y turno. Por favor, elige otro puesto o turno.");
                    ViewBag.ID_Departamento = new SelectList(db.Departamentos, "ID_Departamento", "Nombre", empleados.ID_Departamento);
                    ViewBag.ID_Puesto = new SelectList(db.Puestos, "ID_Puesto", "Nombre", empleados.ID_Puesto);
                    ViewBag.ID_Turno = new SelectList(db.Horarios, "ID_Turno", "Nombre_Turno", empleados.ID_Turno);

                    // Asegúrate de que ViewBag.Genero se vuelve a establecer
                    var generos = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "H", Text = "Hombre" },
                        new SelectListItem { Value = "M", Text = "Mujer" }
                    };
                    
                    ViewBag.Genero = generos;

                    return View(empleados);
                }

                // Si no hay empleados en el mismo turno y puesto, agregar el nuevo empleado
                db.Empleados.Add(empleados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Si ModelState no es válido, retornar la vista con los datos
            ViewBag.ID_Departamento = new SelectList(db.Departamentos, "ID_Departamento", "Nombre", empleados.ID_Departamento);
            ViewBag.ID_Puesto = new SelectList(db.Puestos, "ID_Puesto", "Nombre", empleados.ID_Puesto);
            ViewBag.ID_Turno = new SelectList(db.Horarios, "ID_Turno", "Nombre_Turno", empleados.ID_Turno);

            // Asegúrate de que ViewBag.Genero se vuelve a establecer
            var generosInvalidModel = new List<SelectListItem>
            {
                new SelectListItem { Value = "H", Text = "Hombre" },
                new SelectListItem { Value = "M", Text = "Mujer" }
            };
            
            ViewBag.Genero = generosInvalidModel;

            return View(empleados);
        }

        // GET: Empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }

            // Crea una lista de SelectListItem para el género
            var generos = new List<SelectListItem>
            {
                new SelectListItem { Value = "H", Text = "Hombre" },
                new SelectListItem { Value = "M", Text = "Mujer" }
            };

            // Pasa la lista a la vista
            ViewBag.Genero = new SelectList(generos, "Value", "Text");


            ViewBag.ID_Departamento = new SelectList(db.Departamentos, "ID_Departamento", "Nombre", empleados.ID_Departamento);
            ViewBag.ID_Puesto = new SelectList(db.Puestos, "ID_Puesto", "Nombre", empleados.ID_Puesto);
            ViewBag.ID_Turno = new SelectList(db.Horarios, "ID_Turno", "Nombre_Turno", empleados.ID_Turno);
            return View(empleados);
        }

        // POST: Empleados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Empleado,Nombre,Apellido_Paterno,Apellido_Materno,Fecha_Nacimiento,Genero,Direccion,Ciudad,Pais,Telefono,Correo_Electronico,Fecha_Contratacion,Salario,ID_Departamento,ID_Puesto,ID_Turno")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Departamento = new SelectList(db.Departamentos, "ID_Departamento", "Nombre", empleados.ID_Departamento);
            ViewBag.ID_Puesto = new SelectList(db.Puestos, "ID_Puesto", "Nombre", empleados.ID_Puesto);
            ViewBag.ID_Turno = new SelectList(db.Horarios, "ID_Turno", "Nombre_Turno", empleados.ID_Turno);
            return View(empleados);
        }

        // GET: Empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleados empleados = db.Empleados.Find(id);
            db.Empleados.Remove(empleados);
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
