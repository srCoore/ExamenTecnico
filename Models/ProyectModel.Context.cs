//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ProyectEntities : DbContext
    {
        public ProyectEntities()
            : base("name=ProyectEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Articulo> Articulo { get; set; }
        public virtual DbSet<Clase> Clase { get; set; }
        public virtual DbSet<Departamento> Departamento { get; set; }
        public virtual DbSet<Familia> Familia { get; set; }
    
        public virtual int Alta(Nullable<decimal> sku, string articulo, string marca, string modelo, Nullable<decimal> idDepartamento, Nullable<decimal> idClase, Nullable<decimal> idFamilia, Nullable<System.DateTime> fechaAlta, Nullable<decimal> stock, Nullable<decimal> cantidad, Nullable<decimal> descontinuado, Nullable<System.DateTime> fechaBaja)
        {
            var skuParameter = sku.HasValue ?
                new ObjectParameter("Sku", sku) :
                new ObjectParameter("Sku", typeof(decimal));
    
            var articuloParameter = articulo != null ?
                new ObjectParameter("Articulo", articulo) :
                new ObjectParameter("Articulo", typeof(string));
    
            var marcaParameter = marca != null ?
                new ObjectParameter("Marca", marca) :
                new ObjectParameter("Marca", typeof(string));
    
            var modeloParameter = modelo != null ?
                new ObjectParameter("Modelo", modelo) :
                new ObjectParameter("Modelo", typeof(string));
    
            var idDepartamentoParameter = idDepartamento.HasValue ?
                new ObjectParameter("idDepartamento", idDepartamento) :
                new ObjectParameter("idDepartamento", typeof(decimal));
    
            var idClaseParameter = idClase.HasValue ?
                new ObjectParameter("idClase", idClase) :
                new ObjectParameter("idClase", typeof(decimal));
    
            var idFamiliaParameter = idFamilia.HasValue ?
                new ObjectParameter("idFamilia", idFamilia) :
                new ObjectParameter("idFamilia", typeof(decimal));
    
            var fechaAltaParameter = fechaAlta.HasValue ?
                new ObjectParameter("FechaAlta", fechaAlta) :
                new ObjectParameter("FechaAlta", typeof(System.DateTime));
    
            var stockParameter = stock.HasValue ?
                new ObjectParameter("Stock", stock) :
                new ObjectParameter("Stock", typeof(decimal));
    
            var cantidadParameter = cantidad.HasValue ?
                new ObjectParameter("Cantidad", cantidad) :
                new ObjectParameter("Cantidad", typeof(decimal));
    
            var descontinuadoParameter = descontinuado.HasValue ?
                new ObjectParameter("Descontinuado", descontinuado) :
                new ObjectParameter("Descontinuado", typeof(decimal));
    
            var fechaBajaParameter = fechaBaja.HasValue ?
                new ObjectParameter("FechaBaja", fechaBaja) :
                new ObjectParameter("FechaBaja", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Alta", skuParameter, articuloParameter, marcaParameter, modeloParameter, idDepartamentoParameter, idClaseParameter, idFamiliaParameter, fechaAltaParameter, stockParameter, cantidadParameter, descontinuadoParameter, fechaBajaParameter);
        }
    
        public virtual int Baja(Nullable<decimal> sku, Nullable<int> idArticulo)
        {
            var skuParameter = sku.HasValue ?
                new ObjectParameter("Sku", sku) :
                new ObjectParameter("Sku", typeof(decimal));
    
            var idArticuloParameter = idArticulo.HasValue ?
                new ObjectParameter("idArticulo", idArticulo) :
                new ObjectParameter("idArticulo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Baja", skuParameter, idArticuloParameter);
        }
    
        public virtual int Cambio(Nullable<int> idArticulo, Nullable<decimal> sku, string articulo, string marca, string modelo, Nullable<decimal> idDepartamento, Nullable<decimal> idClase, Nullable<decimal> idFamilia, Nullable<decimal> stock, Nullable<decimal> cantidad, Nullable<decimal> descontinuado, Nullable<System.DateTime> fechaBaja)
        {
            var idArticuloParameter = idArticulo.HasValue ?
                new ObjectParameter("idArticulo", idArticulo) :
                new ObjectParameter("idArticulo", typeof(int));
    
            var skuParameter = sku.HasValue ?
                new ObjectParameter("Sku", sku) :
                new ObjectParameter("Sku", typeof(decimal));
    
            var articuloParameter = articulo != null ?
                new ObjectParameter("Articulo", articulo) :
                new ObjectParameter("Articulo", typeof(string));
    
            var marcaParameter = marca != null ?
                new ObjectParameter("Marca", marca) :
                new ObjectParameter("Marca", typeof(string));
    
            var modeloParameter = modelo != null ?
                new ObjectParameter("Modelo", modelo) :
                new ObjectParameter("Modelo", typeof(string));
    
            var idDepartamentoParameter = idDepartamento.HasValue ?
                new ObjectParameter("idDepartamento", idDepartamento) :
                new ObjectParameter("idDepartamento", typeof(decimal));
    
            var idClaseParameter = idClase.HasValue ?
                new ObjectParameter("idClase", idClase) :
                new ObjectParameter("idClase", typeof(decimal));
    
            var idFamiliaParameter = idFamilia.HasValue ?
                new ObjectParameter("idFamilia", idFamilia) :
                new ObjectParameter("idFamilia", typeof(decimal));
    
            var stockParameter = stock.HasValue ?
                new ObjectParameter("Stock", stock) :
                new ObjectParameter("Stock", typeof(decimal));
    
            var cantidadParameter = cantidad.HasValue ?
                new ObjectParameter("Cantidad", cantidad) :
                new ObjectParameter("Cantidad", typeof(decimal));
    
            var descontinuadoParameter = descontinuado.HasValue ?
                new ObjectParameter("Descontinuado", descontinuado) :
                new ObjectParameter("Descontinuado", typeof(decimal));
    
            var fechaBajaParameter = fechaBaja.HasValue ?
                new ObjectParameter("FechaBaja", fechaBaja) :
                new ObjectParameter("FechaBaja", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Cambio", idArticuloParameter, skuParameter, articuloParameter, marcaParameter, modeloParameter, idDepartamentoParameter, idClaseParameter, idFamiliaParameter, stockParameter, cantidadParameter, descontinuadoParameter, fechaBajaParameter);
        }
    
        public virtual ObjectResult<Consulta_Result> Consulta(Nullable<decimal> sku)
        {
            var skuParameter = sku.HasValue ?
                new ObjectParameter("Sku", sku) :
                new ObjectParameter("Sku", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Consulta_Result>("Consulta", skuParameter);
        }
    }
}
