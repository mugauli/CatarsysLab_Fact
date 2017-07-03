
var order = '';
var moneda = "$"
var tPintadas = 0;

//Estados en los que puede estar un boton 
var ServiciosActuales = {
    ASIGNACIONES: 1,
    PROYECTOS: 2
};

$(document).ready(function () {


    //Asignamos datepichers
    $("#datepickerPendientes").datepicker('setDate', new Date());
    $("#datepickerIngresos").datepicker('setDate', new Date());


    //PENDIENTES POR FACTURAR -- IndexTabla = 1
    var table = $('#datatable').DataTable({
        dom: "<'row itemHeader'<'col-sm-6'<'col-sm-12'>><'col-sm-6'<'col-sm-12'f>>><'row itemHeader'<'col-sm-6'<'col-sm-12'l>><'col-sm-6'<'col-sm-12'B>>>rt<p>",
        buttons: [{ extend: "copy", className: "btn-sm" }, { extend: "csv", className: "btn-sm" }, { extend: "excel", className: "btn-sm" }, { extend: "pdf", className: "btn-sm" }, { extend: "print", className: "btn-sm" }],
        language: {
            processing: "Procesando...",
            search: "Buscar",
            emptyTable: "No hay datos disponibles.",
            zeroRecords: "No hay coincidencias con la busqueda.",
            decimal: ".",
            lengthMenu: "_MENU_ ",
            paginate: {
                first: "Primero",
                previous: "Anterior",
                next: "Siguiente",
                last: "Último"
            }
        },
        serverSide: true,
        ajax: function (data, callback, settings) {  // Make the Ajax call ourselves
            $.ajax({
                url: "/Home/TablePendientesPorFacturarPaginacion",
                type: "POST",
                data: {
                    draw: data.draw,   // Needed for paging
                    start: data.start,    // Needed for paging
                    length: data.length, // Needed for paging
                    company: $("#sltEmpresa").val(),
                    search: $("#datatable_filter input").val(),
                    order: order
                }
            }).done(function (data, textStatus, jqXHR) {

                // Callback function that must be executed when the required data
                // has been obtained.
                // That data should be passed into the callback as the only parameter.
                callback(data);
                order = table.order();


            })

        },
        columns: [
            { "data": "IdFactura" },
            { "data": "DiaFacturacion" },
            { "data": "Cliente" },
            { "data": "Concepto" },
            { "data": "Monto" },
            { "data": "estado" }],
        columnDefs: [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [5],
                "visible": true,
                "searchable": true,
                "class": "semaphoreValue"
            }],
        drawCallback: function () {
            //SE CALCULA EL TOTAL DE MONTO
            var api = this.api(), data;
            var intVal = function (i) {
                return typeof i === 'string' ? i.replace(/[\$,]/g, '') * 1 : typeof i === 'number' ? i : 0;
            };

            total_page_salary = api.column(4, { page: 'current' }).data().reduce(function (a, b) {
                return intVal(a) + intVal(b);
            }, 0)

            $(".tFacVencidas").text(moneda + total_page_salary);

            //Pinar simbolo monetario y semaforo
            pimp_simbolo_moneda(1);
            asign_semaphore(1);

        }
    });

    //SERVICIOS ACTUALES -- IndexTabla = 2
    //SE CARGAN LAS ASIGNACIONES INICIALMENTE
    loadTableServActuales(ServiciosActuales.ASIGNACIONES);

    //PROXIMOS INGRESOS -- IndexTabla = 3
    var table3 = $('#datatableProxIngresos').DataTable({
        dom: "<'row itemHeader'<'col-sm-6'<'col-sm-12'>><'col-sm-6'<'col-sm-12'f>>><'row itemHeader'<'col-sm-6'<'col-sm-12'l>><'col-sm-6'<'col-sm-12'B>>>rt<p>",
        buttons: [{ extend: "copy", className: "btn-sm" }, { extend: "csv", className: "btn-sm" }, { extend: "excel", className: "btn-sm" }, { extend: "pdf", className: "btn-sm" }, { extend: "print", className: "btn-sm" }],
        language: {
            processing: "Procesando...",
            search: "Buscar",
            emptyTable: "No hay datos disponibles.",
            zeroRecords: "No hay coincidencias con la busqueda.",
            decimal: ".",
            lengthMenu: "_MENU_ ",
            paginate: {
                first: "Primero",
                previous: "Anterior",
                next: "Siguiente",
                last: "Último"
            }
        },
        serverSide: true,
        ajax: function (data, callback, settings) {  // Make the Ajax call ourselves
            $.ajax({
                url: "/Home/TableProximosIngresosPaginacion",
                type: "POST",
                data: {
                    draw: data.draw,   // Needed for paging
                    start: data.start,    // Needed for paging
                    length: data.length, // Needed for paging
                    company: $("#sltEmpresa").val(),
                    search: $("#datatable_filter input").val(),
                    order: order
                }
            }).done(function (data, textStatus, jqXHR) {

                // Callback function that must be executed when the required data
                // has been obtained.
                // That data should be passed into the callback as the only parameter.
                callback(data);
                order = table3.order();

            })

        },
        columns: [
            { "data": "IdFactura" },
            { "data": "Fecha" },
            { "data": "Cliente" },
            { "data": "Factura_Concepto" },
            { "data": "Monto" }],
        columnDefs: [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            }],
        drawCallback: function () {
            //SE CALCULA EL TOTAL DE MONTO
            var api = this.api(), data;
            var intVal = function (i) {
                return typeof i === 'string' ? i.replace(/[\$,]/g, '') * 1 : typeof i === 'number' ? i : 0;
            };

            total_page_salary = api.column(4, { page: 'current' }).data().reduce(function (a, b) {
                return intVal(a) + intVal(b);
            }, 0)

            $(".tProxIngresos").text(moneda + total_page_salary);
            //Pinar simbolo monetario y semaforo
            pimp_simbolo_moneda(3);
            asign_semaphore(3);
        }
    });

    //CARTERA VENCIDA -- IndexTabla = 4
    var table4 = $('#datatableCarVencida').DataTable({
        dom: "<'row itemHeader'<'col-sm-6'<'col-sm-12'>><'col-sm-6'<'col-sm-12'f>>><'row itemHeader'<'col-sm-6'<'col-sm-12'l>><'col-sm-6'<'col-sm-12'B>>>rt<p>",
        buttons: [{ extend: "copy", className: "btn-sm" }, { extend: "csv", className: "btn-sm" }, { extend: "excel", className: "btn-sm" }, { extend: "pdf", className: "btn-sm" }, { extend: "print", className: "btn-sm" }],
        language: {
            processing: "Procesando...",
            search: "Buscar",
            emptyTable: "No hay datos disponibles.",
            zeroRecords: "No hay coincidencias con la busqueda.",
            decimal: ".",
            lengthMenu: "_MENU_ ",
            paginate: {
                first: "Primero",
                previous: "Anterior",
                next: "Siguiente",
                last: "Último"
            }
        },
        serverSide: true,
        ajax: function (data, callback, settings) {  // Make the Ajax call ourselves
            $.ajax({
                url: "/Home/TableCarteraVencidaPaginacion",
                type: "POST",
                data: {
                    draw: data.draw,   // Needed for paging
                    start: data.start,    // Needed for paging
                    length: data.length, // Needed for paging
                    company: $("#sltEmpresa").val(),
                    search: $("#datatable_filter input").val(),
                    order: order
                }
            }).done(function (data, textStatus, jqXHR) {

                // Callback function that must be executed when the required data
                // has been obtained.
                // That data should be passed into the callback as the only parameter.
                callback(data);
                order = table4.order();



            })

        },
        columns: [
            { "data": "Cliente" },
            { "data": "IdFactura" },
            { "data": "Dias" },
            { "data": "Monto" },
            { "data": "Semaforo" }],
        columnDefs: [
            {
                "targets": [4],
                "visible": true,
                "searchable": true,
                "class": "semaphoreCVencida"
            }],
        drawCallback: function () {
            //SE CALCULA EL TOTAL DE MONTO
            var api = this.api(), data;
            var intVal = function (i) {
                return typeof i === 'string' ? i.replace(/[\$,]/g, '') * 1 : typeof i === 'number' ? i : 0;
            };

            total_page_salary = api.column(3, { page: 'current' }).data().reduce(function (a, b) {
                return intVal(a) + intVal(b);
            }, 0)

            $(".tCarteraVencidaResult").text(moneda + total_page_salary);

            //Pinar simbolo monetario y semaforo
            pimp_simbolo_moneda(4);
            asign_semaphore(4);
        }
    });



    //SE ESCUCHA ALGUN CAMBIO DE SELECCION
    $('input[type=radio][name=servTyperadio]').change(function () {
        if (this.value == 'A') {
            loadTableServActuales(ServiciosActuales.ASIGNACIONES);
        }
        else if (this.value == 'P') {
            $("#typeServicio").text("Nombre");
            loadTableServActuales(ServiciosActuales.PROYECTOS);
        }
    });




    //$('#datatable tbody').on('click', 'tr', function () {
    //    var data = table.row(this).data();
    //    cargarInfoModal(data["IdEmpleado"]);
    //});


    $("#sltEmpresa").change(function () {
        table.draw();
    });


    $.fn.datepicker.dates['es'] = {
        days: ["Domingo", "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado"],
        daysShort: ["Dom", "Lun", "Mar", "Mie", "Jue", "Vie", "Sab"],
        daysMin: ["D", "L", "M", "M", "J", "V", "S"],
        months: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
        monthsShort: ["ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"],
        today: "Hoy",
        clear: "Limpiar",
        format: "dd/mm/yyyy",
        titleFormat: "MM yyyy", /* Leverages same syntax as 'format' */
        weekStart: 1
    };

    //$('#datatable tbody').on('click', 'tr', function () {
    //    var data = table2.row(this).data();
    //    cargarInfoModal(data["IdEmpleado"]);
    //});



});

function loadTableServActuales(tipoServicio) {
    //1 -> Asignaciones , 2 -> Proyecto
    var tipo = "";
    switch (tipoServicio) {
        case ServiciosActuales.ASIGNACIONES:
            tipo = "TableServiciosActualesPaginacion_Asignaciones";
            break;
        case ServiciosActuales.PROYECTOS:
            tipo = "TableServiciosActualesPaginacion_Proyectos";
            break;
    }

    //SERVICIOS ACTUALES
    var table2 = $('#datatableServActuales').DataTable({
        destroy: true,
        dom: "<'row itemHeader'<'col-sm-6'<'col-sm-12'>><'col-sm-6'<'col-sm-12'f>>><'row itemHeader'<'col-sm-6'<'col-sm-12'l>><'col-sm-6'<'col-sm-12'B>>>rt<p>",
        buttons: [{ extend: "copy", className: "btn-sm" }, { extend: "csv", className: "btn-sm" }, { extend: "excel", className: "btn-sm" }, { extend: "pdf", className: "btn-sm" }, { extend: "print", className: "btn-sm" }],
        language: {
            processing: "Procesando...",
            search: "Buscar",
            emptyTable: "No hay datos disponibles.",
            zeroRecords: "No hay coincidencias con la busqueda.",
            decimal: ".",
            lengthMenu: "_MENU_ ",
            paginate: {
                first: "Primero",
                previous: "Anterior",
                next: "Siguiente",
                last: "Último"
            }
        },
        serverSide: true,
        ajax: function (data, callback, settings) {  // Make the Ajax call ourselves
            $.ajax({
                url: "/Home/" + tipo,
                type: "POST",
                data: {
                    draw: data.draw,   // Needed for paging
                    start: data.start,    // Needed for paging
                    length: data.length, // Needed for paging
                    company: $("#sltEmpresa").val(),
                    search: $("#datatable_filter input").val(),
                    order: order
                }
            }).done(function (data, textStatus, jqXHR) {

                // Callback function that must be executed when the required data
                // has been obtained.
                // That data should be passed into the callback as the only parameter.
                callback(data);
                order = table2.order();


            })

        },
        columns: [
            { "data": "IdProyecto_Asignacion" },
            { "data": "Nombre" },
            { "data": "Cliente" },
            { "data": "Ultima_Factura" },
            { "data": "Termino" },
            { "data": "Semaforo" }],
        columnDefs: [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [5],
                "visible": true,
                "searchable": true,
                "class": "semaphoreServicio"
            }],
        drawCallback: function () {
            //Pinar simbolo monetario y semaforo
            pimp_simbolo_moneda(2);
            asign_semaphore(2);
        }
    });
}

//Asigna el color al td representativo del semaforo
function asign_semaphore(IndexTabla) {
    if (IndexTabla == 1) {
        $('#datatable').find('tr').each(function (index) {
            $(this).children("td").each(function (index2) {
                //BUSCAMOS DIRECTAMENTE LA COLUMNA DE ESTADO
                if (index2 == 4) {
                    var vencida = $(this).text();
                    //validamos su vigencia
                    if (vencida == "1") {
                        $(this).parent('tr').addClass('facVencida');
                    } else {
                        $(this).parent('tr').addClass('facAtiempo');
                    }
                }

            })
        });
    }

    else if (IndexTabla == 2) {
        $('#datatableServActuales').find('tr').each(function (index) {
            $(this).children("td").each(function (index2) {
                //BUSCAMOS DIRECTAMENTE LA COLUMNA DE ESTADO
                if (index2 == 4) {

                    var color = $(this).text();
                    //debugger;

                    if (color == "Red") {
                        $(this).parent('tr').addClass("facVencida");
                    }
                    else {
                        $(this).parent('tr').addClass("facAtiempo");
                    }
                }

            })
        });

    }

    else if (IndexTabla == 4) {
        $('#datatableCarVencida').find('tr').each(function (index) {
            $(this).children("td").each(function (index2) {
                //BUSCAMOS DIRECTAMENTE LA COLUMNA DE ESTADO
                if (index2 == 4) {
                    var color = $(this).text();
                    //debugger;

                    if (color == "Red") {
                        $(this).parent('tr').addClass("facVencida");
                    }
                    else {
                        $(this).parent('tr').addClass("facAtiempo");
                    }

                }

            })
        });
    }


}

//Concatena el simbolo de moneda deseado a los valores de la columna deseada
function pimp_simbolo_moneda(IndexTabla) {

    if (IndexTabla == 1) {
        $('#datatable').find('tr').each(function (index) {
            $(this).children("td").each(function (index2) {
                //BUSCAMOS DIRECTAMENTE LA COLUMNA DE MONTO
                if (index2 == 3) {
                    var monto = $(this).text();
                    $(this).text(moneda + monto);
                }
            })
        });
    }

    else if (IndexTabla == 3) {
        $('#datatableProxIngresos').find('tr').each(function (index) {
            $(this).children("td").each(function (index2) {
                //BUSCAMOS DIRECTAMENTE LA COLUMNA DE MONTO
                if (index2 == 3) {
                    var monto = $(this).text();
                    $(this).text(moneda + monto);
                }
            })
        });
    }

    else if (IndexTabla == 4) {
        $('#datatableCarVencida').find('tr').each(function (index) {
            $(this).children("td").each(function (index2) {
                //BUSCAMOS DIRECTAMENTE LA COLUMNA DE MONTO
                if (index2 == 3) {
                    var monto = $(this).text();
                    $(this).text(moneda + monto);
                }
            })
        });
    }
}
