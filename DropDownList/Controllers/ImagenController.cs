using EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DropDownList.Controllers
{
    public class ImagenController : Controller
    {
        // GET: Imagen
        public ActionResult AgregarImagen()
        {
            return View();
        }

        public ActionResult Guardar(Models.ImagenModelo modelo, HttpPostedFileBase foto)
        {
            if(!ModelState.IsValid || (foto == null && modelo.IdImagen == 0))
            {
                byte[] fotoDB = null;
                if(foto != null)
                {
                    BinaryReader lector = new BinaryReader(foto.InputStream);
                    fotoDB = lector.ReadBytes((int)foto.ContentLength);
                }
                //conexion
                using(var bd = new EF.PaisesEntities())
                {
                    Imagenes imagenes = new Imagenes();
                    imagenes.IdImagen = modelo.IdImagen;
                    imagenes.NombreFoto = modelo.NombreFoto;
                    imagenes.FOTO = fotoDB;
                    bd.Imagenes.Add(imagenes);
                    int res = bd.SaveChanges();
                    if (res == 1)
                    {
                        ViewBag.Message = "La imagen se subio correctamente";
                    }
                    else
                    {
                        ViewBag.Message = "La imagen no se subio correctamente :(";
                    }
                }
            }
            return PartialView("validacion");
        }
    }
}