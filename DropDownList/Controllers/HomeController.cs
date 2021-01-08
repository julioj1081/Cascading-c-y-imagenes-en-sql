using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace DropDownList.Controllers
{
    public class HomeController : Controller
    {

        EF.PaisesEntities db = new EF.PaisesEntities();
        public ActionResult Index()
        {
            List<EF.Paises> PaisesList = db.Paises.ToList();
            ViewBag.PaisesList = new SelectList(PaisesList, "IdPais", "Pais");
            return View();
        }

        public JsonResult GetEstados(int IdPais)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<EF.Estados> EstadosList = db.Estados.Where(x => x.IdPais == IdPais).ToList();
            return Json(EstadosList, JsonRequestBehavior.AllowGet);
        }
        
    }
}