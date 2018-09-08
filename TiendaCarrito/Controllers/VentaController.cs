using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TiendaCarrito.Controllers
{
    [Authorize]
    public class VentaController : Controller
    {
        Models.DBTiendaEntities DB = new Models.DBTiendaEntities();
        // GET: Venta
        
        public ActionResult Index()
        {
            return View(DB.Venta.ToList().OrderBy(x => x.DiaVenta));
        }

        public ActionResult Detalles(int Id)
        {
            return View(DB.Venta.Find(Id));
        }

    }
}