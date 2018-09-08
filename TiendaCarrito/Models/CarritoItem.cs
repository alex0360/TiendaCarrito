using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiendaCarrito.Models
{
    public class CarritoItem
    {
        private Producto _producto;
        private int _cantidad;

        public Producto Producto { get => _producto; set => _producto = value; }
        public int Cantidad { get => _cantidad; set => _cantidad = value; }

        /// <summary>
        /// Constructor Sin Parametros
        /// </summary>
        public CarritoItem() { }

        /// <summary>
        /// Constructor Con Parametros
        /// </summary>
        /// <param name="producto">Tipo Producto</param>
        /// <param name="cantidad">Cantidad</param>
        public CarritoItem(Producto producto, int cantidad)
        {
            Producto = producto;
            Cantidad = cantidad;
        }
    }
}