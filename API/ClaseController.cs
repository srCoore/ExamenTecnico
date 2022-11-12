using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Proyecto.API
{
    public class ClaseController : ApiController
    {
        private ProyectEntities context = new ProyectEntities();

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var clases = context.Clase.Where(d => d.idDepartamento == id).Select(d => new {
                    id = d.idClase,
                    Nombre = d.NombreClase
                }).ToList();
                if(clases.Count > 0)
                {
                    return Content(HttpStatusCode.OK, clases);
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, new { Message = "No se encontraron registros" });
                }
            }catch(Exception e)
            {
                return Content(HttpStatusCode.BadRequest, new { Message = "Error en el servidor" });
            }
        }
    }
}
