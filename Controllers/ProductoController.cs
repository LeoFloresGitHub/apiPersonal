using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NegocioApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;


namespace NegocioApi.Controllers
{
    [EnableCors("ReglasCors")]

    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        public readonly MiNegocioBdContext _dbContext;

        public ProductoController(MiNegocioBdContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetProductos()
        {
            List<Producto> listaProductos = new List<Producto>();
            try
            {
                listaProductos = _dbContext.Productos.Include(c => c.oCategoria).ToList();
                return StatusCode(StatusCodes.Status200OK, new {mensaje= "ok", response = listaProductos});
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        [Route("/api/[controller]/{idProducto}")]
        public IActionResult GetProductoxID(int idProducto)
        {
            Producto objProducto = _dbContext.Productos.Find(idProducto);
            if(objProducto == null)
            {
                return BadRequest("Producto no encontrado");
            }


            try
            {
                //Incluyendo la categoria al producto
                objProducto = _dbContext.Productos.Include(c => c.oCategoria).Where(p => p.IdProducto == idProducto).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, objProducto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = objProducto });
            }
        }

        [HttpPost]
        [Route("/api/[controller]/guardar")]
        public IActionResult SubirProducto(Producto objProducto)
        {

            if(objProducto == null)
            {
                return BadRequest("No se indico datos del producto a guardar");
            }

            try
            {
                _dbContext.Productos.Add(objProducto);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Producto guardado con exito" });

            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message});
            }
        }

        [HttpPut]
        [Route("/api/[controller]/actualizar")]
        public IActionResult ActualizarProducto(Producto objProducto)
        {
            //Verificamos si el producto exite
            Producto obj = _dbContext.Productos.Find(objProducto.IdProducto);

            if (obj == null)
            {
                return BadRequest("No se indico datos completos del producto a actualizar");
            }

            try
            {   //Llenamos los campos de la actualización 

                obj.Nombre = objProducto.Nombre is null ? obj.Nombre: objProducto.Nombre.ToString().Trim();
                obj.Marca = objProducto.Marca is null ? obj.Marca : objProducto.Marca.ToString().Trim();
                obj.IdCategoria = objProducto.IdCategoria is null ? obj.IdCategoria : objProducto.IdCategoria;
                obj.Cantidad = objProducto.Cantidad is null ? obj.Cantidad : objProducto.Cantidad;
                obj.Precio = objProducto.Precio is null ? obj.Precio : objProducto.Precio;


                _dbContext.Productos.Update(obj);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Producto actualizado con exito" });

            }
            catch (Exception ex)
            {
                var innerExceptionMessage = ex.InnerException?.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"Error al guardar los cambios: {innerExceptionMessage}" });
            }
        }


        [HttpDelete]
        [Route("/api/[controller]/eliminar/{idProducto}")]
        public IActionResult EliminarProducto(int idProducto)
        {
            //Verificamos si el producto exite
            Producto obj = _dbContext.Productos.Find(idProducto);

            if (obj == null)
            {
                return BadRequest("No se indico datos completos del producto a eliminar");
            }

            try
            {   

                _dbContext.Productos.Remove(obj);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Producto eliminado con exito" });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

    }
}
