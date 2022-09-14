
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCCrud.Models;
using MVCCrud.Models.ViewModels;

namespace MVCCrud.Controllers
{
    public class TablaController : Controller
    {
        // GET: Tabla
        public ActionResult Index()
        {
            List<ListTablaViewModel> lst;
            using (crud_MVCEntities db = new crud_MVCEntities())
            {
                lst = (from d in db.tabla
                       select new ListTablaViewModel
                       {
                           Id = d.id,
                           Nombre = d.nombre,
                           Correo = d.correo,
                           Fecha_Nacimiento = (DateTime)d.fecha_nacimiento

                       }).ToList();

            }


            return View(lst);
        }

        // Crear 
        public ActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(TablaViewModel model)
        {
            try 
            { 
                if (ModelState.IsValid)
                {
                    using (crud_MVCEntities db = new crud_MVCEntities())
                    {
                        var oTabla = new tabla();
                        oTabla.nombre = model.Nombre;
                        oTabla.correo = model.Correo;
                        oTabla.fecha_nacimiento = model.Fecha_Nacimiento;
                        
                        db.tabla.Add(oTabla);
                        db.SaveChanges();
                    }
                    return Redirect("~/Tabla/");


                }
                return View(model);

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        // Editar o Modificar
        public ActionResult Editar(int Id)
        {
            TablaViewModel model = new TablaViewModel();   
            using (crud_MVCEntities db = new crud_MVCEntities())
            {
                var oTabla = db.tabla.Find(Id);
                model.Nombre = oTabla.nombre;
                model.Correo = oTabla.correo;
                model.Fecha_Nacimiento= (DateTime)oTabla.fecha_nacimiento;
                model.Id = oTabla.id;

            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(TablaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (crud_MVCEntities db = new crud_MVCEntities())
                    {
                        var oTabla = db.tabla.Find(model.Id);
                        oTabla.nombre = model.Nombre;
                        oTabla.correo = model.Correo;
                        oTabla.fecha_nacimiento = model.Fecha_Nacimiento;

                        db.Entry(oTabla).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Tabla/");


                }
                return View(model);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        //Eliminar o Borrar
        public ActionResult Eliminar(int Id)
        {
            using (crud_MVCEntities db = new crud_MVCEntities())
            {
                var oTabla = db.tabla.Find(Id);
                db.tabla.Remove(oTabla);
                db.SaveChanges();

            }

            return Redirect("~/Tabla/");
        }
    }
}