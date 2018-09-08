using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TiendaCarrito.Controllers
{
    public class CarritoController : Controller
    {
        // GET: Carrito DBEntity

        private Models.DBTiendaEntities DB = new Models.DBTiendaEntities();
        private List<Models.CarritoItem> compras;

        public ActionResult AgregarCarrito(int? id)
        {
            if (id != null)
            {
                if (Session["carrito"] == null)
                {
                    compras = new List<Models.CarritoItem> {
                    new Models.CarritoItem(DB.Producto.Find(id), 1)
                };
                    Session["carrito"] = compras;
                } else
                {
                    compras = (List<Models.CarritoItem>)Session["carrito"];
                    int IndexExistente = GetIndex(id);

                    if (IndexExistente == -1)
                        compras.Add(new Models.CarritoItem(DB.Producto.Find(id), 1));
                    else
                        compras[IndexExistente].Cantidad++;
                    Session["carrito"] = compras;
                }
            }
            return View();
        }

        
        public ActionResult Delete(int Id)
        {
            compras = (List<Models.CarritoItem>)Session["carrito"];
            compras.RemoveAt(GetIndex(Id));
            return View("AgregarCarrito");
        }
        [Authorize]
        public ActionResult FinalizarCompra()
        {
                compras = (List<Models.CarritoItem>)Session["carrito"];
                Models.Producto p = new Models.Producto();
                if (compras != null && compras.Count > 0)
                {
                    Models.Venta newVenta = new Models.Venta {
                        DiaVenta = DateTime.Now,
                        Subtotal = compras.Sum(x => x.Producto.Precio * x.Cantidad),
                        Usuario = User.Identity.Name
                    };
                    newVenta.Itbs = newVenta.Subtotal * 0.17;
                    newVenta.Total = newVenta.Subtotal + newVenta.Itbs;

                    newVenta.ListaVenta = (from producto in compras
                                           select new Models.ListaVenta {
                                               FKProducto = producto.Producto.IdProducto,
                                               Cantidad = producto.Cantidad,
                                               Total = producto.Cantidad * producto.Producto.Precio
                                           }).ToList();
                    DB.Venta.Add(newVenta);
                    DB.SaveChanges();
                    Session["carrito"] = new List<Models.CarritoItem>();
                }
            
            return View();
        }
        private int GetIndex(int? Id)
        {
            compras = (List<Models.CarritoItem>)Session["carrito"];
                    for (int i = 0; i < compras.Count; i++)
                    {
                        if (compras[i].Producto.IdProducto == Id)
                            return i;
                    }
              
                return -1;
           
        }
    }
}