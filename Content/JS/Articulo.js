const inpSku = document.getElementById("idSku");
const descontinuado = document.getElementById("Desactivar");
const articulo = document.getElementById("Articulo");
const marca = document.getElementById("Marca");
const modelo = document.getElementById("Modelo");
const departamento = document.getElementById("Departamento");
const clase = document.getElementById("Clase");
const familia = document.getElementById("Familia");
const stock = document.getElementById("Stock");
const cantidad = document.getElementById("Cantidad");
const fecha_registro = document.getElementById("FechaAlta");
const fecha_baja = document.getElementById("FechaBaja");
const idArticuloHidden = document.getElementById("idArt");
let departamentoid = 0;
let claseid = 0;
let familiaid = 0;


function eventos(){
    inpSku.addEventListener("click", getArticulo);
    departamento.addEventListener("change",GetClase);
    clase.addEventListener("change",GetFamilia);
}
eventos();



function llenarDatos(data){
    
    idArticuloHidden.value = data.idArticulo;
    descontinuado.checked = data.Descontinuado == 1 ? true : false;
    articulo.value = data.Articulo;
    marca.value = data.Marca;
    modelo.value = data.Modelo;
    stock.value = data.Stock;
    document.getElementById("nombreDepartamento").innerText = " Actual " + data.Departamento;
    document.getElementById("nombreClase").innerText = " Actual " + data.Clase;
    document.getElementById("nombreFamilia").innerText = "Actual " +  data.Familia;
    claseid = data.idClase;
    departamentoid = data.idDepartamento;
    familiaid = data.idFamilia;
    cantidad.value = data.Cantidad;
    fecha_registro.value = formatDate(new Date(data.FechaAlta
        ));
    fecha_baja.value = formatDate(new Date(data.FechaBaja));
   
    
}

/* Seccion: GET */
/* Obtiene los clases, y familias a partir de el departamento seleccionado*/


function getArticulo(e){
    if(idArticuloHidden.value != 0){
        limpiarForm();
    }
    const sku = e.target.value;
    
    $.ajax({
        url: "/api/Articulo/GetBySku",
        type: 'GET',
        data: { Sku: document.getElementById("idNumeroSku").value},
        dataType: 'json', // added data type
        success: function(response) {
            
            $("#guardar").hide();
            $("#editar,#eliminar,#checkDescontinuar,#divFechas").show();
            getDepartamentos();
            llenarDatos(response);
        },
        error: function(response) {
            $("#editar,#eliminar,#checkDescontinuar,#divFechas,#nombreDepartamento,#nombreClase,#nombreFamilia").hide();
            getDepartamentos();
            $("#guardar").show();
        }
    });
    
}
function getDepartamentos(){
    $.ajax({
        url: "/api/Departamento/Get",
        type: 'GET',
        dataType: 'json', // added data type
        success: function(response) {
            llenarSelectDpto(response);
        },
        error: function(response) {
            console.log(response);
        }
    });
}

function GetClase(e){
    limpiarSelect("Clase");
    $.ajax({
        url: "/api/Clase/Get",
        type: 'GET',
        data: {id : e.target.value},
        dataType: 'json', // added data type
        success: function(response) {
            limpiarSelect("Clase");
            llenarSelectCF(response,"Clase");
            
        },
        error: function(response) {
            console.log(response);
        }
    });
}

function GetFamilia(e){
    limpiarSelect("Familia");
    $.ajax({
        url: "/api/Familia/Get",
        type: 'GET',
        data: {id : e.target.value},
        dataType: 'json', // added data type
        success: function(response) {
            limpiarSelect("Familia");
            llenarSelectCF(response,"Familia");
            
        },
        error: function(response) {
            console.log(response);
        }
    });
}

function GetDCF(){
    
    $.ajax({
        url: "/api/Clase/Get",
        type: 'GET',
        data: {id : departamentoid},
        dataType: 'json', // added data type
        success: function(response) {
            limpiarSelect("Clase");
            llenarSelectCF(response,"Clase");
            
        },
        error: function(response) {
            console.log(response);
        }
    });

    $.ajax({
        url: "/api/Familia/Get",
        type: 'GET',
        data: {id : claseid},
        dataType: 'json', // added data type
        success: function(response) {
            limpiarSelect("Familia");
            llenarSelectCF(response,"Familia");
            
        },
        error: function(response) {
            console.log(response);
        }
    });
    
}





let errores;
/*Seccion Post*/
/*Agregar Articulo*/
function AddArticulo(){
    
    if(parseFloat(cantidad.value) > parseFloat(stock.value)){
        Swal.fire("Atencion","La cantidad debe ser menor que el stock","warning");
        return false;
    }
    const newArticulo = {
        Sku : inpSku.value,
        Articulo1 : articulo.value,
        Marca : marca.value,
        Modelo : modelo.value,
        idDepartamento : departamento.value != "" ? parseFloat(departamento.value) : 0,
        idClase : clase.value != "" ? parseFloat(clase.value): 0,
        idFamilia : familia.value != "" ? parseFloat(familia.value) : 0,
        FechaAlta: null,
        FechaBaja: null,
        Stock : stock.value,
        Cantidad : cantidad.value,
        Descontinuado : 0,
        isUpdate : false
    };
    console.log(newArticulo);
    $.ajax({
        type: "POST",
        url: "/api/Articulo/AddArticulo",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        data: newArticulo,
        success: function(response) {
            Swal.fire({
                title: "Guardado",
                icon: "info",
                width: "500px",
                confirmButtonText: "Aceptar",
                showCloseButton: true,
                allowOutsideClick: false
            }).then((result) => {
                window.location.reload();
            });
        },
        error: function(response) {
            Swal.fire("Atencion","Error Complete los campos!","warning");
            errores = response.responseJSON.Content;
            Object.keys(errores).forEach(key => {
                $(`#${key}`).show();
                console.log(key, errores[key][0]);
              });
        }
     });
}


/* Region: Update */

function UpdateArticulo(){
    
    if(parseFloat(cantidad.value) > parseFloat(stock.value)){
        Swal.fire("Atencion","La cantidad debe ser menor que el stock","warning");
        return false;
    }
    const newArticulo = {
        idArticulo: idArticuloHidden.value,
        Sku : inpSku.value,
        Articulo1 : articulo.value,
        Marca : marca.value,
        Modelo : modelo.value,
        idDepartamento : departamento.value != "" ?  parseFloat(departamento.value) : departamentoid,
        idClase : clase.value != "" ?  parseFloat( clase.value): claseid,
        idFamilia : familia.value != "" ? parseFloat(familia.value) : familiaid,
        FechaAlta: null,
        FechaBaja: null,
        Stock : stock.value,
        Cantidad : cantidad.value,
        Descontinuado : descontinuado.checked ? 1 : 0,
        isUpdate : false
    };
   
    $.ajax({
        type: "PUT",
        url: "/api/Articulo/UpdateArticulo",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        data: newArticulo,
        success: function(response) {
            Swal.fire({
                title: "Guardado",
                icon: "info",
                width: "500px",
                confirmButtonText: "Aceptar",
                showCloseButton: true,
                allowOutsideClick: false
            }).then((result) => {
                window.location.reload();
            });
        },
        error: function(response) {

            errores = response.responseJSON.Content;
            Object.keys(errores).forEach(key => {
                $(`#${key}`).show();
                console.log(key, errores[key][0]);
              });
        }
     });
}


/*Seccion: Delete */
/* Elimina el articulo */
function Eliminar(){
    if(parseFloat(inpSku.value) == 0 || parseFloat(idArticuloHidden.value) == 0){
        Swal.fire("Atencion","Ingrese un Sku para continuar", "warning");
        return false;
    }


    Swal.fire({
        title: 'Seguro que quieres eliminar?',
        showDenyButton: true,
        showCancelButton: false,
        confirmButtonText: 'Si',
        denyButtonText: `No`,
      }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: `/api/Articulo/Descontinuar?Sku=${parseFloat(inpSku.value)}&idArticulo=${parseFloat(idArticuloHidden.value)}`,
                type: 'DELETE',
                dataType: 'json', // added data type
                success: function(response) {
                    Swal.fire({
                        title: "Eliminado",
                        icon: "info",
                        width: "500px",
                        confirmButtonText: "Aceptar",
                        showCloseButton: true,
                        allowOutsideClick: false
                    }).then((result) => {
                        window.location.reload();
                    });
                    
                },
                error: function(response) {
                    console.log(response);
                }
            });
        } else if (result.isDenied) {
          Swal.fire('Cancelado', 'No se realizaron los cambios', 'info');
        }
      })
    
    
}

/*Utilidades*/

function llenarSelectDpto(data){
    
    data.forEach(element => {
        const option = document.createElement("option");
        option.value = element.idDepartamento;
        option.text = element.Nombre;
        departamento.appendChild(option);
    });
    
    
    
}

function limpiarSelect(tipe){
    const slct = document.getElementById(`${tipe}`);
    while(slct.lastChild){
        slct.removeChild(slct.lastChild);
    }
}

function limpiarForm(){
    
    limpiarSelect("Clase");
    limpiarSelect("Familia");
    $("#form-body :input").val('');
}

function formatDate(date) {
    return (
        [
            date.getFullYear(),
            padTo2Digits(date.getMonth() + 1),
            padTo2Digits(date.getDate()),
        ].join('-')
    );
}

function padTo2Digits(num) {
    return num.toString().padStart(2, '0');
}

function llenarSelectCF(data, tipe){
    limpiarSelect(tipe);
    data.forEach((element) => {
        const option = document.createElement("option");
        option.value = element.id;
        option.text = element.Nombre;
        document.getElementById(`${tipe}`).appendChild(option);
    });
    
}

function getAllArticulos() {
    const tbody = document.getElementById("idTdBody");
    let strBody = ``;
    $.ajax({
        url: "/api/Articulo/GetAllArticulo",
        type: 'GET',
       
        dataType: 'json', // added data type
        success: function (response) {
            console.log(response.data);
            response.data.forEach((d) => {

                strBody += `
                     <tr>
                        <td>
                            ${ d.Sku}
                        </td>
                        <td>
                            ${ d.Articulo1}
                        </td>
                        <td>
                             ${ d.Marca}
                        </td>
                        <td>
                            ${ d.Modelo}
                        </td>
                        <td>
                            ${ d.idDepartamento}
                        </td>
                        <td>
                             ${ d.idClase}
                        </td>
                        <td>
                            ${ d.idFamilia}
                        </td>
                        <td>
                             ${d.FechaAlta}
                        </td>
                        <td>
                            ${ d.Stock}
                        </td>
                        <td>
                             ${ d.Cantidad}
                        </td>
                        <td>
                             ${ d.Descontinuado}
                        </td>
                        <td>
                            ${ d.FechaBaja}
                        </td>
                    </tr>
                `;

            })
            tbody.innerHTML = strBody;
        },
        error: function (response) {
            console.log(response);
        }
    });
}


getAllArticulos();