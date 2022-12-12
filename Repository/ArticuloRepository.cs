using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace Proyecto.Repository
{
    public class ArticuloRepository
    {
        private ProyectEntities context = new ProyectEntities(); 

        public ArticuloRepository()
        {
            context = new ProyectEntities();
        }

        public Consulta_Result getArticulo(int Sku)
        {
            var articulo = context.Consulta(Sku).FirstOrDefault();
            return articulo;
        }

        public List<Articulo> getAllArticulos()
        {
            var lista = context.Articulo.ToList();
            return lista;
        }

        public void AgregarArticulo(Articulo articulo)
        {
            articulo.FechaAlta = DateTime.Now;
            articulo.FechaBaja = Convert.ToDateTime("1900-01-01");
            context.Alta(articulo.Sku,articulo.Articulo1,articulo.Marca, articulo.Marca, articulo.idDepartamento, articulo.idClase, articulo.idFamilia, DateTime.Now, articulo.Stock, articulo.Cantidad, articulo.Descontinuado, articulo.FechaBaja);
            context.SaveChanges();
        }

        public void Descontinuar(int Sku, int descontinuado)
        {
            context.Baja(Sku, descontinuado);
            context.SaveChanges();
        }

        public void Actualizar(Articulo articulo)
        {
            if(articulo.Descontinuado == 1)
            {
                articulo.FechaBaja = DateTime.Now;
            }
            context.Cambio(articulo.idArticulo,articulo.Sku, articulo.Articulo1, articulo.Marca, articulo.Modelo, articulo.idDepartamento, articulo.idClase, articulo.idFamilia, articulo.Stock, articulo.Cantidad, articulo.Descontinuado, articulo.FechaBaja);
            context.SaveChanges();
        }

        public ModelStateDictionary ValidaArticulo(Articulo articulo, bool isUpdate)
        {

            ModelStateDictionary errores = new ModelStateDictionary();
            if (!isUpdate)
            {
                if (articulo.Sku < 0)
                {
                    errores.AddModelError("SkuError", "Falta agregar el SKU");
                }
            }
            if (String.IsNullOrWhiteSpace(articulo.Articulo1))
            {
                errores.AddModelError("ArticuloError", "Falta agregar el articulo");
            }
            if (String.IsNullOrWhiteSpace(articulo.Marca))
            {
                errores.AddModelError("MarcaError", "Fatla agregar la marca");
            }
            if (String.IsNullOrWhiteSpace(articulo.Modelo))
            {
                errores.AddModelError("ModeloError", "Falta agregar el modelo");
            }
            if(articulo.idDepartamento <= 0)
            {
                errores.AddModelError("DepartamentoError", "Falta agregar el departamento");
            }
            if(articulo.idClase <= 0)
            {
                errores.AddModelError("ClaseError", "Falta agregar el tipo clase");
            }
            if(articulo.idFamilia <= 0)
            {
                errores.AddModelError("FamiliaError", "Falta agregar tipo familia");
            }
           
            if(articulo.Stock < 0 || articulo.Stock < articulo.Cantidad)
            {
                errores.AddModelError("StockError", "Error al ingresar el stock");
            }
            if(articulo.Cantidad < 0)
            {
                errores.AddModelError("CantidadError", "Error al ingresar la cantidad");
            }

            if (errores.Any())
            {
                return errores;
            }
            return null;
        }
        
    }
}
