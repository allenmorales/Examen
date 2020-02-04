using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APP.UI.Controllers
{
    public class AreaController : Controller
    {

        public ExamenEntities context = new ExamenEntities();
        // GET: Area
        public ActionResult Index()
        {
            var Area = context.Area;
            return View(Area);
        }

        // GET: Area/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Area/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Area/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                {
                    
                    var areas = new Area();
                    areas.Nombre = collection["nombre"];
                    areas.Descripcion = collection["descripcion"];

                    context.Area.Add(areas);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Area/Edit/5
        public ActionResult Edit(int id)
        {
            Area vArea = context.Area.Find(id);
            return View(vArea);
        }


        // POST: Area/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, Area vArea)
        {
            try
            {
                context.Entry(vArea).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
             
                return RedirectToAction("Index");
             }
            catch
            {
                return View(vArea);
            }
        }
    // GET: Area/Delete/5
    public ActionResult Delete(int id)
    {
            Area vArea = context.Area.Find(id);
            return View(vArea);
    }

    // POST: Area/Delete/5
    [HttpPost]
    public ActionResult Delete(int id, FormCollection collection)
    {
        try
        {
                // TODO: Add delete logic here
                Area vArea = context.Area.Find(id);
                context.Area.Remove(vArea);
                context.SaveChanges();
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
                return RedirectToAction("Index");
            }
    }
}
}
