using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APP.UI.Controllers
{
    public class EmpleadosController : Controller
    {
        public ExamenEntities context = new ExamenEntities();
        // GET: Empleados
        public ActionResult Index()
        {

            var vEmpleado = context.Empleado;
            return View(vEmpleado);
    

        }

        // GET: Empleados/Details/5
        public ActionResult Details(int id)
        {
            Empleado vEmpleados1 = context.Empleado.Find(id);

            if (vEmpleados1.Foto == null)
            {
                ViewBag.objfoto = "";
            }
            else
            { 
            string base64String = Convert.ToBase64String(vEmpleados1.Foto, 0, vEmpleados1.Foto.Length);
            ViewBag.objfoto = "data:image/png;base64," + base64String;
            }
            return View(vEmpleados1);
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {
            var vEmpleado = context.Empleado.GroupBy(x => x.IdJefe).Select(x => new { IdJefe = x.Key }).ToList();
            
            IEnumerable<SelectListItem> jefeList = vEmpleado.Select(x => new SelectListItem
            {
                Value = x.IdJefe.ToString(),
                Text = "Jefe " + x.IdJefe
            });
            ViewBag.jefeList = jefeList;

            var vArea = context.Area.Select(x => new { IdArea = x.IdArea, Nombre = x.Nombre }).ToList();
            IEnumerable<SelectListItem> areaList = vArea.Select(x => new SelectListItem
            {
                Value = x.IdArea.ToString(),
                Text = x.IdArea +" - " + x.Nombre
            });
            ViewBag.areaList = areaList;

            return View();

        }

        // POST: Empleados/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                var Empleados = new Empleado();
                Empleados.NombreCompleto = collection["nombrecompleto"];
                Empleados.Cedula = collection["cedula"];
                Empleados.Correo = collection["correo"];
                Empleados.FechaNacimiento = Convert.ToDateTime(collection["fechanacimiento"]);
                Empleados.FechaIngreso = Convert.ToDateTime(collection["fechaingreso"]);
                Empleados.IdJefe = Convert.ToInt32(collection["idjefe"]);
                Empleados.IdArea = Convert.ToInt32(collection["idarea"]);

                HttpPostedFileBase file = Request.Files["ImageData"];

                using (var reader = new System.IO.BinaryReader(file.InputStream))
                {
                    Empleados.Foto = reader.ReadBytes(file.ContentLength);
                }



                context.Empleado.Add(Empleados);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Crear");
            }
        }

        // GET: Empleados/Edit/5
        public ActionResult Edit(int id)
        {
            Empleado vEmpleado1 = context.Empleado.Find(id);

            var vEmpleado = context.Empleado.GroupBy(x => x.IdJefe).Select(x => new { IdJefe = x.Key }).ToList();

            IEnumerable<SelectListItem> jefeList = vEmpleado.Select(x => new SelectListItem
            {
                Value = x.IdJefe.ToString(),
                Text = "Jefe " + x.IdJefe
            });
            ViewBag.jefeList = jefeList;

            var vArea = context.Area.Select(x => new { IdArea = x.IdArea, Nombre = x.Nombre }).ToList();
            IEnumerable<SelectListItem> areaList = vArea.Select(x => new SelectListItem
            {
                Value = x.IdArea.ToString(),
                Text = x.IdArea + " - " + x.Nombre
            });
            ViewBag.areaList = areaList;

            return View(vEmpleado1);
        }

        // POST: Empleados/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, Empleado vEmpleado)
        {
            try
            {
                // TODO: Add update logic here
                

                HttpPostedFileBase file = Request.Files["ImageData"];
                using (var reader = new System.IO.BinaryReader(file.InputStream))
                {
                    vEmpleado.Foto = reader.ReadBytes(file.ContentLength);
                }


                context.Entry(vEmpleado).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Empleados/Delete/5
        public ActionResult Delete(int id)
        {
            Empleado vEmpleados1 = context.Empleado.Find(id);
            return View(vEmpleados1);
        }

        // POST: Empleados/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Empleado vEmpleados1 = context.Empleado.Find(id);
                context.Empleado.Remove(vEmpleados1);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
