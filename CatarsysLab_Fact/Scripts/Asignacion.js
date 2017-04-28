
$(document).ready(function () {

    var order = '';

    var validarGuardarAsignacion = function () {

        var correcto = true;

        if ($("#Cliente").val() == 0) {
            $("#fnCliente .errorMsg").html("Debe selecciónar un cliente.");
            $("#Cliente").addClass("inputError");
            correcto = false;
        } else {
            $("#fnCliente .errorMsg").html('');
            $("#Cliente").removeClass("inputError");
        }

        if ($("#Consultor").val() == 0) {
            $("#fnConsultor .errorMsg").html("Debe selecciónar un Consultor.");
            $("#Consultor").addClass("inputError");
            correcto = false;
        } else {
            $("#fnConsultor .errorMsg").html('');
            $("#Consultor").removeClass("inputError");
        }

        if ($("#Tipo").val() == 0) {
            $("#fnTipo .errorMsg").html("Debe selecciónar un Tipo de asignación.");
            $("#Tipo").addClass("inputError");
            correcto = false;
        } else {
            $("#fnTipo .errorMsg").html('');
            $("#Tipo").removeClass("inputError");
        }

        if ($("#fecha_inicio").val() == '') {
            $("#fnFecha .errorMsg").html('Debe agregar una fecha de inicio.');
            $("#fecha_inicio").addClass("inputError");
            correcto = false;
        } else {
            $("#fnFecha .errorMsg").html('');
            $("#fecha_inicio").removeClass("inputError");
        }

        if ($("#fecha_fin").val() == '' && $("#Tipo").val() != 2) {
            $("#fnFecha .errorMsg2").html("Debe agregar una fecha de fin.");
            $("#fecha_fin").addClass("inputError");
            correcto = false;
        } else {
            $("#fnFecha .errorMsg2").html('');
            $("#fecha_fin").removeClass("inputError");
        }

        if ($("#Corte").val() == 0) {
            $("#fnCorte .errorMsg").html("Debe selecciónar un Corte de Facturación.");
            $("#Corte").addClass("inputError");
            correcto = false;
        } else {
            $("#fnCorte .errorMsg").html('');
            $("#Corte").removeClass("inputError");
        }


        if ($("#Costo").val() == '') {
            $("#fnCostoPeriodo .errorMsg").html('Debe agregar un costo pactado.');
            $("#Costo").addClass("inputError");
            correcto = false;
        } else {
            $("#fnCostoPeriodo .errorMsg").html('');
            $("#Costo").removeClass("inputError");
        }

        if ($("#Periodo").val() == 0) {
            $("#fnCostoPeriodo .errorMsg2").html("Debe selecionar un perido.");
            $("#Periodo").addClass("inputError");
            correcto = false;
        } else {
            $("#fnCostoPeriodo .errorMsg2").html('');
            $("#Periodo").removeClass("inputError");
        }

        if ($("#Moneda").val() == 0) {
            $("#fnMonedaIva .errorMsg").html('Debe seleccionar moneda a facturar.');
            $("#Moneda").addClass("inputError");
            correcto = false;
        } else {
            $("#fnMonedaIva .errorMsg").html('');
            $("#Moneda").removeClass("inputError");
        }

        if ($("#IVA").val() == 0) {
            $("#fnMonedaIva .errorMsg2").html("Debe selecionar IVA a facturar.");
            $("#IVA").addClass("inputError");
            correcto = false;
        } else {
            $("#fnMonedaIva .errorMsg2").html('');
            $("#IVA").removeClass("inputError");
        }

        return correcto;

    }

    var limpiarModal = function () {

        
        $("#idAsignacion").val(0);

        $("#Cliente").val(0);

        $("#Consultor").val(0);

        $("#Tipo").val(0);

        $("#fecha_inicio").val('');

        $("#fecha_fin").val('');

        $("#Corte").val(0);

        $("#Costo").val('');

        $("#Periodo").val(0);

        $("#Moneda").val(0);

        $("#IVA").val(0);


    }

    var cargarModal = function (info) {

        debugger;

        $("#idAsignacion").val(info.Id_Asignacion);

        $("#Cliente").val(info.Id_Cliente_Asignacion);

        $("#Consultor").val(info.Id_Empleado_Asignacion);

        $("#Tipo").val(info.Id_Tipo_Asignacion);

        $("#fecha_inicio").val(info.Fecha_Ini_AsignacionSTR);

        $("#fecha_fin").val(info.Fecha_Fin_AsignacionSTR);

        $("#Corte").val(info.Id_Corte_Asignacion);

        $("#Costo").val(info.Costo_Pactado_Asignacion);

        $("#Periodo").val(info.Id_Periodo_Asignacion);

        $("#Moneda").val(info.Id_Moneda_Asignacion);

        $("#IVA").val(info.Id_IVA_Asignacion);

        if (info.Id_Estatus_Asignacion == 1)
        {
            $("#rdSI").attr('checked', true);
            $("#rdNO").attr('checked', false);
        }
        else {
            $("#rdSI").attr('checked', false);
            $("#rdNO").attr('checked', true);
        }

    }

    var cargarInfoModal = function (IdAsignacion) {


        $.ajax({
            type: 'POST',
            url: '/Gestion/ObtenerAsignacion',
            data: { IdAsignacion: IdAsignacion },
            success: function (response) {
                if (response.success) {
                    cargarModal(response.info[0]);
                    $("#TitleModalAsignacion").html("Detalle - Asignación");
                    $("#btnVerFacturas").fadeIn();
                    $("#addAsignacion").modal("show");
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
                url: "/Gestion/TablePaginacion",
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
            { "data": "IdAsignacion" },
            { "data": "Consultor" },
            { "data": "Cliente" },
            { "data": "Fecha_Inicio" },
            { "data": "Duracion" },
            { "data": "Fecha_Fin" },
            { "data": "Prox_Facturacion" },
            { "data": "Estado" }],
        columnDefs: [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            }]
    });

    $('#datatable tbody').on('click', 'tr', function () {
        var data = table.row(this).data();
        cargarInfoModal(data["IdAsignacion"]);
    });

    $(".conBtnAgregar").html('<button type="button" id="agregarAsg" class="btn btn-custom dropdown-toggle waves-effect waves-light" data-toggle="modal" data-target="#addAsignacion">Agregar <span class="m-l-5"><i class="fa fa-plus-circle"></i></span></button>');

    $("#agregarAsg").click(function () {
        $("#TitleModalAsignacion").html("Alta - Asignación");
        $("#btnVerFacturas").fadeOut();
        $("#idAsignacion").val('0');
    });

    $("#sltEmpresa").change(function () {
        table.draw();
    });

    $("#GuardarAsignacion").click(function () {
        $("#frIdEmpresa").val($("#sltEmpresa").val());
        if (validarGuardarAsignacion()) {
           // alert($("#fnGuardarAsignacion").serialize());
            $.ajax({
                type: 'POST',
                url: '/Gestion/GuardarAsignacion',
                data: $("#fnGuardarAsignacion").serialize(),
                success: function (response) {
                    if (response.success) {
                        limpiarModal();
                        table.draw();
                        $("#addAsignacion").modal("hide");
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

        if ($('#fecha_inicio').val() == '' || $('#fecha_fin').val() == '')
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