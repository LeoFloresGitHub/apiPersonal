using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NegocioApi.Models;

namespace NegocioApi.Controllers
{[Route("api/[controller]")]
        [ApiController]

    public class CategoriaController : Controller
    {

        private readonly MiNegocioBdContext _dbContext;

        public CategoriaController(MiNegocioBdContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetCategorias()
        {
            List<Categoria> ListaCategorias = new List<Categoria>();

            try
            {
                ListaCategorias = _dbContext.Categoria.ToList();
                return StatusCode(StatusCodes.Status200OK, ListaCategorias);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }


        }

        [HttpGet]
        [Route("/api/[controller]/{idEntrada}")]
        public IActionResult GetCategoriaxID(int idCategoria)
        {
            Categoria objCategoria = _dbContext.Categoria.Find(idCategoria);
            if (objCategoria == null)
            {
                return BadRequest("Categoria no encontrado");
            }

            try
            {
                objCategoria = _dbContext.Categoria.FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, objCategoria);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }


        }

        [HttpPost]
        [Route("/api/[controller]/subir")]
        public IActionResult SubirCategoria(Categoria categoria)
        {
            if (categoria == null)
            {
                return BadRequest("No se encontro categoria a subir");
            }

            try
            {
                _dbContext.Categoria.Add(categoria);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, categoria);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("/api/[controller]/actualizar")]
        public IActionResult ActualizarCategoria(Categoria categoria)
        {
            Categoria objCategoria = _dbContext.Categoria.Find(categoria.IdCategoria);

            if (objCategoria == null)
            {
                return BadRequest("No se encontro categoria a actualizar");
            }

            try
            {
                objCategoria.Nombre = categoria.Nombre is null ? objCategoria.Nombre : categoria.Nombre;
                
                _dbContext.Categoria.Update(objCategoria);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Categoria actualizada con exito" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }


        }

        [HttpDelete]
        [Route("/api/[controller]/eliminar/{idCategoria}")]
        public IActionResult EliminarCategoria(int idCategoria)
        {
            Categoria objCategoria = _dbContext.Categoria.Find(idCategoria);

            if (objCategoria == null)
            {
                return BadRequest("No se encontro categoria a actualizar");
            }

            try
            {


                _dbContext.Categoria.Remove(objCategoria);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Categoria eliminada con exito" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }


    }
}
