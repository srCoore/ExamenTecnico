using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Proyecto.API
{
    public class FamiliaController : ApiController
    {
        private ProyectEntities context = new ProyectEntities();

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var clases = context.Familia.Where(d => d.idClase == id).Select(d => new {
                    id = d.idFamilia,
                    Nombre = d.NombreFamilia
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
                return Content(HttpStatusCode.InternalServerError, new { Message = "Error en el servidor" });
            }
        }
    }
}
