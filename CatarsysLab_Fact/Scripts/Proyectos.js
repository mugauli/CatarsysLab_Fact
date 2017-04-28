
$(document).ready(function () {

    var order = '';
    var validarGuardarProyecto = function () {

        var correcto = true;

        if ($("#Cliente").val() == 0) {
            $("#fnCliente .errorMsg").html("Debe selecciónar un cliente.");
            $("#Cliente").addClass("inputError");
            correcto = false;
        } else {
            $("#fnCliente .errorMsg").html('');
            $("#Cliente").removeClass("inputError");
        }

        if ($("#fecha_inicio").val() == '') {
            $("#fnFecha .errorMsg").html('Debe agregar una fecha de inicio del proyecto.');
            $("#fecha_inicio").addClass("inputError");
            correcto = false;
        } else {
            $("#fnFecha .errorMsg").html('');
            $("#fecha_inicio").removeClass("inputError");
        }

        if ($("#fecha_fin").val() == '') {
            $("#fnFecha .errorMsg2").html('Debe agregar una fecha de fin del proyecto.');
            $("#fecha_fin").addClass("inputError");
            correcto = false;
        } else {
            $("#fnFecha .errorMsg2").html('');
            $("#fecha_fin").removeClass("inputError");
        }

        if ($("#Costo").val() == '') {
            $("#fnCostoMoneda .errorMsg").html('Debe agregar un costo pactado.');
            $("#Costo").addClass("inputError");
            correcto = false;
        } else {
            $("#fnCostoMoneda .errorMsg").html('');
            $("#Costo").removeClass("inputError");
        }

        if ($("#Moneda").val() == 0) {
            $("#fnCostoMoneda .errorMsg2").html('Debe seleccionar moneda a facturar.');
            $("#Moneda").addClass("inputError");
            correcto = false;
        } else {
            $("#fnCostoMoneda .errorMsg2").html('');
            $("#Moneda").removeClass("inputError");
        }

        if ($("#IVA").val() == 0) {
            $("#fnIVA .errorMsg").html("Debe selecionar IVA a facturar.");
            $("#IVA").addClass("inputError");
            correcto = false;
        } else {
            $("#fnIVA .errorMsg").html('');
            $("#IVA").removeClass("inputError");
        }

        

        if ($("#CantidadFacturas").val() == '') {
            $("#fnCantidadTipo .errorMsg").html("Debe selecionar IVA a facturar.");
            $("#CantidadFacturas").addClass("inputError");
            correcto = false;
        } else {
            $("#fnCantidadTipo .errorMsg").html('');
            $("#CantidadFacturas").removeClass("inputError");
        }

        if ($("#TipoCambio").val() == 0) {
            $("#fnCantidadTipo .errorMsg2").html("Debe selecionar Tipo de cambio.");
            $("#TipoCambio").addClass("inputError");
            correcto = false;
        } else {
            $("#fnCantidadTipo .errorMsg2").html('');
            $("#TipoCambio").removeClass("inputError");
        }

        return correcto;

    }

    var limpiarModal = function () {


        $("#idProyecto").val(0);

        $("#Cliente").val(0);

        $("#NombreProyecto").val('');

        $("#fecha_inicio").val('');

        $("#fecha_fin").val('');

        $("#Costo").val('');

        $("#Moneda").val(0);

        $("#IVA").val(0);

        $("#CantidadFacturas").val('');

        $("#TipoCambio").val('');

        $("#datatableFacturasProyecto tbody").html('');


    }

    var cargarModal = function (info) {

        $("#idProyecto").val(info.Id_Proyectos);

        $("#Cliente").val(info.Id_Clientes_Proyectos);

        $("#NombreProyecto").val(info.Nombre_Proyectos);

        $("#fecha_inicio").val(info.Fecha_Ini_ProyectosSTR);

        $("#fecha_fin").val(info.Fecha_Fin_ProyectosSTR);

        $("#Costo").val(info.Costo_Proyectos);

        $("#Moneda").val(info.Id_Moneda_Proyectos);

        $("#IVA").val(info.Id_IVA_Proyectos);
        
        $("#CantidadFacturas").val(info.Numero_Facturas_Proyectos);

        $("#TipoCambio").val(info.Id_Tipo_Cambio_Proyectos);

        if (info.Estado == 1)
        {
            $("#rdSI").attr('checked', true);
            $("#rdNO").attr('checked', false);
        }
        else
        {
            $("#rdSI").attr('checked', false);
            $("#rdNO").attr('checked', true);
        }

    }

    var cargarInfoModal = function (IdProyecto) {


        $.ajax({
            type: 'POST',
            url: '/Gestion/ObtenerProyecto',
            data: { IdProyecto: IdProyecto },
            success: function (response) {
                if (response.success) {
                    cargarModal(response.info[0]);

                    $("#TitleModalAsignacion").html("Detalle - Proyecto");
                    $("#btnVerFacturas").fadeIn();
                    $("#btnFacturar").fadeIn();
                    $("#datatableFacturasProyecto").fadeIn();

                    $("#addProyecto").modal("show");
                }
                else
                    alert(response.message);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                var mensaje = 'Error al obtener asignación:' + thrownError;
                alert(mensaje);
            }
        });

    }

    $('#datatableFacturasProyecto').DataTable({
        dom: "rt",
    });

    var table = $('#datatable').DataTable({
        dom: "<'row itemHeader'<'col-sm-9'<'col-sm-12 conBtnAgregar'>><'col-sm-3'<'col-sm-12'f>>><'row itemHeader'<'col-sm-9'<'col-sm-12'l>><'col-sm-3'<'col-sm-12'B>>>rt<p>",
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
                url: "/Gestion/TablePaginacionProy",
                type: "POST",
                data: {
                    draw: data.draw,   // Needed for paging
                    start: data.start,    // Needed for paging
                    length: data.length, // Needed for paging
                    company: $("#sltEmpresa").val(),
                    filter2: 2,
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
            { "data": "IdProyecto" },
            { "data": "Cliente" }, 
            { "data": "NombreProyecto" }, 
            { "data": "Facturado" },
            { "data": "RestaFacturar" },
            { "data": "Total_Proyecto" },
            { "data": "Prox_Facturacion" },
            { "data": "Estado" },
            { "data": "Comentarios" }],
        columnDefs: [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            }]
    });

    $('#datatable tbody').on('click', 'tr', function () {
        var data = table.row(this).data();
        cargarInfoModal(data["IdProyecto"]);
    });

    $(".conBtnAgregar").html('<button type="button" id="agregarProy" class="btn btn-custom dropdown-toggle waves-effect waves-light" data-toggle="modal" data-target="#addProyecto">Agregar <span class="m-l-5"><i class="fa fa-plus-circle"></i></span></button>');

    $("#agregarProy").click(function () {
        $("#TitleModalAsignacion").html("Alta - Proyecto");
        $("#btnVerFacturas").fadeOut();
        $("#btnFacturar").fadeOut();
        $("#datatableFacturasProyecto").fadeOut();
        $("#idProyecto").val('0');
    });

    $("#sltEmpresa").change(function () {
        table.draw();
    });

    $("#GuardarProyecto").click(function () {
        $("#frIdEmpresa").val($("#sltEmpresa").val());

        //alert($("#fnGuardarProyecto").serialize());

        if (validarGuardarProyecto()) {

            $.ajax({
                type: 'POST',
                url: '/Gestion/GuardarProyecto',
                data: $("#fnGuardarProyecto").serialize(),
                success: function (response) {
                    if (response.success) {
                        limpiarModal();
                        table.draw();
                        $("#addProyecto").modal("hide");
                    }
                    else
                        alert(response.message);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    var mensaje = 'Error al guardar asignación:' + thrownError;
                    alert(mensaje);
                }
            });
        }
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

    $('#fecha_inicio').datepicker({
        language: "es"

    });

    $('#fecha_fin').datepicker({
        language: "es"
    });

    $('#fecha_inicio').change(function () {

        if ($('#fecha_inicio').val() == '' || $('#fecha_fin').val() == '')
            return 0;


        var start = $("#fecha_inicio").datepicker("getDate");
        var end = $("#fecha_fin").datepicker("getDate");


        var dias = (end - start) / (1000 * 60 * 60 * 24);

        if (dias < 0) {
            $("#fnFecha .errorMsg").html("La fecha de fin debe ser mayor o igual a la fecha de inicio.");
            $("#fecha_inicio").val('');
            $("#fecha_inicio").addClass("inputError");
        }
        else {
            $("#fnFecha .errorMsg").html('');
            $("#fecha_inicio").removeClass("inputError");
        }


    });

    $('#fecha_fin').change(function () {

        if ($('#fecha_fin').val() == '')
            return 0;


        var start = $("#fecha_inicio").datepicker("getDate");
        var end = $("#fecha_fin").datepicker("getDate");


        var dias = (end - start) / (1000 * 60 * 60 * 24);

        if (dias < 0) {
            $("#fnFecha .errorMsg2").html("La fecha de fin debe ser mayor o igual a la fecha de inicio.");
            $("#fecha_fin").val('');
            $("#fecha_fin").addClass("inputError");
        }
        else {
            $("#fnFecha .errorMsg2").html('');
            $("#fecha_fin").removeClass("inputError");
        }


    });

});