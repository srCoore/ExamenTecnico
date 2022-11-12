using Proyecto.Models;
using Proyecto.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Proyecto.API
{
    public class ArticuloController : ApiController
    {
        private ArticuloRepository articuloRepository = new ArticuloRepository();

        [HttpGet]
        public IHttpActionResult GetBySku(int Sku)
        {
            try
            {
                var articulo = articuloRepository.getArticulo(Sku);
                if(articulo != null)
                {
                    return Content(HttpStatusCode.OK, articulo);
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, new { Message = "No se encontraron resultados" });
                }
            }
            catch(Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult AddArticulo([FromBody] Articulo articulo)
        {
            try
            {
                bool isUpdate = true;
                if(articulo.idArticulo == 0 && articulo.Sku > 0)
                {
                    isUpdate = false;
                }
                var errores = articuloRepository.ValidaArticulo(articulo, isUpdate);
                if(errores != null)
                {
                    ModelState.Merge(errores);
                    var errorDictionary = ModelState.ToDictionary(
                        keyvalue => keyvalue.Key,
                        keyvalue => keyvalue.Value.Errors.Select(p=>p.ErrorMessage).ToArray()
                    );
                    var objectResult = new
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        From = "api/Articulo/AddArticulo",
                        Method = "Post",
                        Content = errorDictionary
                    };
                    return Content(HttpStatusCode.BadRequest, objectResult);
                }
                else
                {
                    
                    articuloRepository.AgregarArticulo(articulo);
                    var objectResult = new
                    {
                        StatusCode = HttpStatusCode.OK,
                        From = "api/Articulo/AddArticulo",
                        Method = "Post",
                        Content = "Articulo Agregado",
                        objArticulo = articulo
                    };
                    return Content(HttpStatusCode.OK, articulo);
                }
            }catch(Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, articulo);
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateArticulo([FromBody]Articulo articulo)
        {
            try
            {
                bool isUpdate = true;
                if (articulo.idArticulo == 0 && articulo.Sku > 0)
                {
                    isUpdate = false;
                }
                var errores = articuloRepository.ValidaArticulo(articulo, isUpdate);
                if(errores != null)
                {
                    ModelState.Merge(errores);
                    var errorDictionary = ModelState.ToDictionary(
                        keyvalue => keyvalue.Key,
                        keyvalue => keyvalue.Value.Errors.Select(p => p.ErrorMessage).ToArray()
                    );
                    var objectResult = new
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        From = "api/Articulo/UpdateArticulo",
                        Method = "Put",
                        Content = errorDictionary
                    };
                    return Content(HttpStatusCode.BadRequest, objectResult);

                }
                else
                {
                    
                    articuloRepository.Actualizar(articulo);
                    var objectResult = new
                    {
                        StatusCode = HttpStatusCode.OK,
                        From = "api/Articulo/UpdateArticulo",
                        Method = "Put",
                        Content = "Articulo Actualizado",
                        objArticulo = articulo
                    };
                    return Content(HttpStatusCode.OK, articulo);
                }
            }catch(Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, new { Message = "Error en el servidor" });
            }
        }

        [HttpDelete]
        public IHttpActionResult Descontinuar(int Sku, int idArticulo)
        {
            try
            {
                articuloRepository.Descontinuar(Sku, idArticulo);
                return Content(HttpStatusCode.OK, new { Message = "Se actualizo correctamente" });
            }catch(Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, new { Message = "Error en el servidor" });
            }
        }
    }
}
