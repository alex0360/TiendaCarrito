using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TiendaCarrito.Models;

namespace TiendaCarrito.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ProductoesController : Controller
    {
        private DBTiendaEntities DB = new DBTiendaEntities();

        // Subir imagen Aumento
        public byte[] FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
                
                    file.InputStream.CopyTo(memoryStream);
                    byte[] array = memoryStream.GetBuffer();

                    var context = new DBTiendaEntities();
                    context.Producto.Add(new Producto(){ Image = array});
                    return array;
                
            }
            return null;
        }
        // Get: Imagen
        public ActionResult Imagen(int id)
        {
            var context = new DBTiendaEntities();
            byte[] imageData = context.Producto.FirstOrDefault(i => i.IdProducto == id).Image;
            if(imageData != null)
            {
                return File(imageData, "imagen/png,jpg");
            }
            return null;
        }
        // GET: Productoes
        public ActionResult Index()
        {
            var producto = DB.Producto.Include(p => p.Categoria);
            return View(producto.ToList());
        }

        // GET: Productoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = DB.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Productoes/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.FKCategoria = new SelectList(DB.Categoria, "IdCategoria", "Nombre");
            return View();
        }

        // POST: Productoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="producto">Productos</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProducto,Nombre,Precio,FechaCreacion,Foto,FKCategoria,Image")]Producto producto)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["file"];
                producto.Image = FileUpload(file);
                producto.FechaCreacion = DateTime.Now;
                DB.Producto.Add(producto);
                DB.SaveChanges();
                return RedirectToAction("Index","Productoes");
            }

            ViewBag.FKCategoria = new SelectList(DB.Categoria, "IdCategoria", "Nombre", producto.FKCategoria);
            return View(producto);
        }

        // GET: Productoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = DB.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.FKCategoria = new SelectList(DB.Categoria, "IdCategoria", "Nombre", producto.FKCategoria);
            return View(producto);
        }

        // POST: Productoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProducto,Nombre,Precio,FechaCreacion,Foto,FKCategoria,Image")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                DB.Entry(producto).State = EntityState.Modified;

                HttpPostedFileBase file = Request.Files["file"];
                producto.Image = FileUpload(file);

                DB.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FKCategoria = new SelectList(DB.Categoria, "IdCategoria", "Nombre", producto.FKCategoria);
            return View(producto);
        }

        // GET: Productoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = DB.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = DB.Producto.Find(id);
            DB.Producto.Remove(producto);
            DB.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DB.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
