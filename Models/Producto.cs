using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NegocioApi.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? Nombre { get; set; }

    public string? Marca { get; set; }

    public int? IdCategoria { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Precio { get; set; }
    [JsonIgnore]
    public virtual ICollection<Entrada> Entrada { get; set; } = new List<Entrada>();

    public virtual Categoria? oCategoria { get; set; }
    [JsonIgnore]
    public virtual ICollection<Salida> Salida { get; set; } = new List<Salida>();
}
