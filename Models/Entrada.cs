using System;
using System.Collections.Generic;

namespace NegocioApi.Models;

public partial class Entrada
{
    public int IdEntrada { get; set; }

    public DateTime? Fecha { get; set; }

    public int? IdProducto { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Costo { get; set; }

    public virtual Producto? oProducto { get; set; }
}
