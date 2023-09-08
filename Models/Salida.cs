using System;
using System.Collections.Generic;

namespace NegocioApi.Models;

public partial class Salida
{
    public int IdSalida { get; set; }

    public DateTime? Fecha { get; set; }

    public int? IdProducto { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Descuento { get; set; }

    public decimal? Total { get; set; }

    public decimal? Delivery { get; set; }

    public decimal? Precio { get; set; }

    public virtual Producto? oProducto { get; set; }
}
