using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaCarrito.Models;
namespace TiendaCarrito.Controllers
{
    public class HomeController : Controller
    {
        private DBTiendaEntities dbTEntit = new DBTiendaEntities();
        // Get: Imagen
        public ActionResult Imagen(int id)
        {
            var context = new DBTiendaEntities();
            byte[] imageData = context.Producto.FirstOrDefault(i => i.IdProducto == id).Image;
            if (imageData != null)
            {
                return File(imageData, "imagen/png,jpg");
            }
            return null;
        }
        
        public ActionResult Index()
        {
            return View(dbTEntit.Producto.ToList().OrderBy(x => x.FKCategoria));
        }

        public ActionResult Matenimiento()
        {
            return View();
        }
    }
}