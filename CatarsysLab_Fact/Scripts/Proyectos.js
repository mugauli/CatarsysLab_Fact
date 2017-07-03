
$(document).ready(function () {

    var order = '';
    var guardado = false;

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
            $("#fnCantidadTipo .errorMsg").html("Debe agregar la cantidad de facturas a generar.");
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

        limpiarModal();

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

        if (info.Estado == 1) {
            $("#rdSI").attr('checked', true);
            $("#rdNO").attr('checked', false);
        }
        else {
            $("#rdSI").attr('checked', false);
            $("#rdNO").attr('checked', true);
        }

        table2.destroy();

        $("#datatableFacturasProyecto tbody").html("");

        $(info.facturas).each(function (i, v) {

            var EstadoFat = v.Id_Estado_Factura == 2 ? "SI" : v.Id_Estado_Factura == 1 ? "NO" : "CANCELADA";

            $("#datatableFacturasProyecto tbody").append('<tr data-IdFactura="' + v.IdFactura + '"><td class="IdFactura">' + v.No_Factura + '</td><td><input ' + (v.Id_Estado_Factura == 1 ? '' : 'readonly = "readonly"') + ' type="text" class="monto" value="' + v.Monto_Factura + '"></td><td><input type="text" ' + (v.Id_Estado_Factura == 1 ? '' : 'readonly = "readonly"') + '  class="fecha" value="' + v.Fecha_fin_FacturaSTR + '"></td><td class="Fact">' + EstadoFat + '</td></tr>');

        });

        table2 = $('#datatableFacturasProyecto').DataTable({
            dom: "t",
        });
    }

    var cargarInfoModal = function (IdProyecto) {

        ModalCargando(true);

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
                ModalCargando(false);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                var mensaje = 'Error al obtener asignación:' + thrownError;
                alert(mensaje);
                ModalCargando(false);
            }
        });

    }

    var diaNumero = function (fecha) {

        var from = fecha.split("/");
        var f = new Date(from[2], from[1] - 1, from[0]);
        return f.getDay();

    }

    var addDias = function (fecha_ini, dias) {

        var from_I = fecha_ini.split("/");
        var fecha = new Date(from_I[2], from_I[1] - 1, from_I[0]);

        fecha.setDate(fecha.getDate() + dias);



        var dia = (fecha.getDate() + '').length == 1 ? '0' + fecha.getDate() : fecha.getDate();
        var mes = ((parseInt(fecha.getMonth()) + 1) + '').length == 1 ? '0' + (parseInt(fecha.getMonth()) + 1) : parseInt(fecha.getMonth() + 1);

        return dia + "/" + mes + "/" + fecha.getFullYear();
    }

    var obtenerFecha = function (fecha_ini, numFact, periodo) {



        var dias = numFact * periodo;

        var fechaResult = addDias(fecha_ini, dias);

        return fechaResult;

    }

    var getDias = function (fecha_ini, fecha_fin) {

        var from_I = fecha_ini.split("/");
        var start = new Date(from_I[2], from_I[1] - 1, from_I[0]);

        var from_F = fecha_fin.split("/");
        var end = new Date(from_F[2], from_F[1] - 1, from_F[0]);

        return (end - start) / (1000 * 60 * 60 * 24)

    }

    var createJSON = function () {
        var item = {};
        jsonCompleto = [];

        jsonFacturas = [];

        $("#datatableFacturasProyecto tbody tr").each(function (i, v) {

           
            item = {};
            //alert($(this).attr("data-IdFactura"));
            item["IdFactura"] = $(this).attr("data-IdFactura");
            item["NoFactura"] = $(this).find(".IdFactura").html();
            item["Monto"] = $(this).find(".monto").val();
            item["Fecha"] = $(this).find(".fecha").val();
            item["Estado"] = $(this).find(".Fact").html();
            jsonFacturas.push(item);

        });


        item = {};

        item["ltsFacturas"] = jsonFacturas;
        jsonCompleto.push(item);
        jsonString = JSON.stringify(jsonCompleto);
        return jsonString;
    }

    var generarTabla = function () {

        debugger;

        var start = $("#fecha_inicio").datepicker("getDate");
        var end = $("#fecha_fin").datepicker("getDate");
        var fecha_ini = $("#fecha_inicio").val();
        var fecha_fin = $("#fecha_fin").val();
        var fecha_ini_2 = $("#fecha_inicio").val();
        var cant_Facturas = $("#CantidadFacturas").val();
        var monto = $("#Costo").val();
        var dias = 0;
        var periodo = 0;
        var inicioTabla = 0;

        var tabla = $("#datatableFacturasProyecto tbody");

        if (fecha_ini == '' || fecha_fin == '' || cant_Facturas == '' || monto == '') {
            return false;
        }

        table2.destroy();

        $("#datatableFacturasProyecto tbody tr").each(function (i, v) {

            if ($(this).find(".Fact").html() != "SI") {
                alert($(this).find(".Fact").html());

                $(this).remove();
            }
            else {
                monto -= parseFloat($(this).find(".monto").val());
                fecha_ini_2 = $(this).find(".fecha").val();
                inicioTabla++;
            }

        });


        if (cant_Facturas - inicioTabla < 1) {

            cant_Facturas = inicioTabla + 1;
            $("#fnCantidadTipo .errorMsg").html("La catidad de facturas debe incluir las ya facturadas.");
            $("#CantidadFacturas").addClass("inputError");
            $("#CantidadFacturas").val(cant_Facturas);

        } else {

            $("#fnCantidadTipo .errorMsg").html('');
            $("#CantidadFacturas").removeClass("inputError");
        }


        cant_Facturas = cant_Facturas - inicioTabla;

        var EstadoFat = "NO";

        if (inicioTabla > 0) {

            dias = getDias(fecha_ini_2, fecha_fin);
            periodo = dias / cant_Facturas;
            fecha_ini = addDias(fecha_ini_2, periodo);
            dias = getDias(fecha_ini_2, fecha_fin);

        }

      

        if (cant_Facturas == 1) {

            diaNo = diaNumero(fecha_ini);

            if (diaNo < 6) {
                tabla.append('<tr data-IdFactura="0"><td class="IdFactura">' + (1 + inicioTabla) + '</td><td><input type="text" class="monto" value="' + monto + '"></td><td><input type="text" class="fecha" value="' + fecha_fin + '"></td><td class="Fact">' + EstadoFat + '</td></tr>');
            }
            else if (diaNo == 6) {
                //alert("funciono Sabado");
                tabla.append('<tr data-IdFactura="0"><td class="IdFactura">' + (1 + inicioTabla) + '</td><td><input type="text" class="monto" value="' + monto + '"></td><td><input type="text" class="fecha" value="' + addDias(fecha_fin, 1) + '"></td><td class="Fact">' + EstadoFat + '</td></tr>');
            }
            else {
                //alert("funciono Domigo");
                tabla.append('<tr data-IdFactura="0"><td class="IdFactura">' + (1 + inicioTabla) + '</td><td><input type="text" class="monto" value="' + monto + '"></td><td><input type="text" class="fecha" value="' + addDias(fecha_fin, 2) + '"></td><td class="Fact">' + EstadoFat + '</td></tr>');
            }
        }
        else {

            dias = getDias(fecha_ini, fecha_fin);
            periodo = dias / (cant_Facturas - 1);




            var montoFactura = monto / cant_Facturas;



            if (dias > 0) {
                var fecha = fecha_ini;

                for (var i = 0; i < cant_Facturas; i++) {

                    fecha = obtenerFecha(fecha_ini, i, periodo);

                    diaNo = diaNumero(fecha);

                    if (diaNo < 6) {
                        tabla.append('<tr data-IdFactura="0"><td class="IdFactura">' + (i + 1 + inicioTabla) + '</td><td><input type="text" class="monto" value="' + montoFactura + '"></td><td><input type="text" class="fecha" value="' + fecha + '"></td><td class="Fact">' + EstadoFat + '</td></tr>');
                    }
                    else if (diaNo == 6) {
                        //alert("funciono Sabado");
                        tabla.append('<tr data-IdFactura="0"><td class="IdFactura">' + (i + 1 + inicioTabla) + '</td><td><input type="text" class="monto" value="' + montoFactura + '"></td><td><input type="text" class="fecha" value="' + addDias(fecha, 1) + '"></td><td class="Fact">' + EstadoFat + '</td></tr>');
                    }
                    else {
                        //alert("funciono Domigo");
                        tabla.append('<tr data-IdFactura="0"><td class="IdFactura">' + (i + 1 + inicioTabla) + '</td><td><input type="text" class="monto" value="' + montoFactura + '"></td><td><input type="text" class="fecha" value="' + addDias(fecha, 2) + '"></td><td class="Fact">' + EstadoFat + '</td></tr>');
                    }
                }

            }
        }
        $("#tablaJSON").val(createJSON());
        guardado = false;

  

        table2 = $('#datatableFacturasProyecto').DataTable({
            dom: "t",
        });

    }

    var table2 = $('#datatableFacturasProyecto').DataTable({
        dom: "t",
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
        generarTabla();
        //        $("#datatableFacturasProyecto").fadeOut();
        $("#idProyecto").val('0');
    });

    $("#sltEmpresa").change(function () {
        table.draw();
    });

    $("#GuardarProyecto").click(function () {
        $("#frIdEmpresa").val($("#sltEmpresa").val());

        //alert($("#fnGuardarProyecto").serialize());

        if (validarGuardarProyecto()) {
            ModalCargando(true);
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
                    ModalCargando(false);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    var mensaje = 'Error al guardar asignación:' + thrownError;
                    alert(mensaje);
                    ModalCargando(false);
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

    $('#CantidadFacturas,#Costo,#fecha_fin,#fecha_inicio').change(function () {
        generarTabla();
    });

});