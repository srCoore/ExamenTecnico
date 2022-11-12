using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Proyecto.API
{
    public class DepartamentoController : ApiController
    {
        private ProyectEntities context = new ProyectEntities();
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                var departamentos = context.Departamento.Select(d => new {
                    idDepartamento = d.idDepartamento,
                    Nombre = d.Nombre
                }).ToList();
                if(departamentos.Count > 0)
                {
                    return Content(HttpStatusCode.OK, departamentos);
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, new { Mensaje = "No se encuentraron registros" });
                }

            }catch(Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, new { Message = "Error en el servidor" });
            }
        }
    }
}
