using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaCarrito.Models;

namespace TiendaCarrito.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        private DBTiendaEntities dbTEntit = new DBTiendaEntities();
        public ActionResult Index()
        {
            return View(dbTEntit.Producto.ToList().OrderBy(x=>x.Nombre));
        }

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
    }
}