
$(document).ready(function () {

    var order = '';
    var guardado = false;

    var validarGuardarFactura = function () {

        var correcto = true;
        var tipo = $("input[type='radio'][name='asigProy']:checked").val();

        if ($("#Cliente").val() == 0) {
            $("#fnCliente .errorMsg").html("Debe selecciónar un cliente.");
            $("#Cliente").addClass("inputError");
            correcto = false;
        } else {
            $("#fnCliente .errorMsg").html('');
            $("#Cliente").removeClass("inputError");
        }

        //Asignacion
        if ($("#fecha_inicio").val() == '' && tipo == 1) {
            $("#fnFecha .errorMsg").html('Debe agregar una fecha de inicio del proyecto.');
            $("#fecha_inicio").addClass("inputError");
            correcto = false;
        } else {
            $("#fnFecha .errorMsg").html('');
            $("#fecha_inicio").removeClass("inputError");
        }

        if ($("#fecha_fin").val() == '' && tipo == 1) {
            $("#fnFecha .errorMsg2").html('Debe agregar una fecha de fin del proyecto.');
            $("#fecha_fin").addClass("inputError");
            correcto = false;
        } else {
            $("#fnFecha .errorMsg2").html('');
            $("#fecha_fin").removeClass("inputError");
        }

        if ($("#sltAsignacion").val() == 0 && tipo == 1) {
            $("#fnAsignacion .errorMsg").html("Debe selecciónar un consultor.");
            $("#sltAsignacion").addClass("inputError");
            correcto = false;
        } else {
            $("#fnAsignacion .errorMsg").html('');
            $("#sltAsignacion").removeClass("inputError");
        }

        //Proyecto
        if ($("#Proyecto").val() == 0 && tipo != 1) {
            $("#fnNombreProyecto .errorMsg").html("Debe selecciónar un proyecto.");
            $("#Proyecto").addClass("inputError");
            correcto = false;
        } else {
            $("#fnNombreProyecto .errorMsg").html('');
            $("#Proyecto").removeClass("inputError");
        }

        //Comun

        if ($("#Monto").val() == '') {
            $("#fnCostoMoneda .errorMsg").html("Debe ingresar un monto para la factura.");
            $("#Monto").addClass("inputError");
            correcto = false;
        } else {
            $("#fnCostoMoneda .errorMsg").html('');
            $("#Monto").removeClass("inputError");
        }

        if ($("#IVA").val() == 0) {
            $("#fnCostoMoneda .errorMsg2").html("Debe selecionar IVA a facturar.");
            $("#IVA").addClass("inputError");
            correcto = false;
        } else {
            $("#fnCostoMoneda .errorMsg2").html('');
            $("#IVA").removeClass("inputError");
        }

        if ($("#Moneda").val() == 0) {
            $("#fnMonedaTipo .errorMsg").html('Debe seleccionar moneda a facturar.');
            $("#Moneda").addClass("inputError");
            correcto = false;
        } else {
            $("#fnMonedaTipo .errorMsg").html('');
            $("#Moneda").removeClass("inputError");
        }

        if ($("#Comentarios").val() == '') {
            $("#errorMsgCom").html('Debe ingresar comentarios.');
            $("#Comentarios").addClass("inputError");
            correcto = false;
        } else {
            $("#errorMsgCom").html('');
            $("#Comentarios").removeClass("inputError");
        }

        if ($("#MetodoPago").val() == 0) {
            $("#errorMsgMP").html('Debe seleccionar metodo de pago.');
            $("#MetodoPago").addClass("inputError");
            correcto = false;
        } else {
            $("#errorMsgMP").html('');
            $("#MetodoPago").removeClass("inputError");
        }

        if ($("#txtDigitos").val() == '') {
            $("#errorMsgDig").html('Debe ingresar los últimos 4 digitos.');
            $("#txtDigitos").addClass("inputError");
            correcto = false;
        } else {
            $("#errorMsgDig").html('');
            $("#txtDigitos").removeClass("inputError");
        }

        return correcto;

    }

    var limpiarModal = function () {

        //cliente
        $("#Nombre").val('');
        $("#RazonSocial").val('');
        $("#RFC").val('');
        $("#calle").val('');
        $("#Exterior").val('');
        $("#Interior").val('');
        $("#Colonia").val('');
        $("#CP").val('');
        $("#DelMpio").val('');
        $("#EstadoDomicilio").val('');
        $("#rdNO").attr("checked", true);

        //otros
        $("#Proyecto").val(0);
        $("#Cliente").val(0); 
        $("#sltAsignacion").val(0); 
        $("#NoFactura").val('');

        $("#fecha_inicio").val('');
        $("#fecha_fin").val('');

        $("#Moneda").val(0);
        $("#IVA").val(0);
        $("#Monto").val('');
        $("#TipoCambio").val(0);

        $("#MetodoPago").val(0);
        $("#Comentarios").val('');        
        $("#txtDigitos").val('');


    }

    var desactivarModal = function (action, actionArchivo) {

        $("#fnGuardarFactura input,#fnGuardarFactura select,#fnGuardarFactura textarea").prop("disabled", action);
        $("#fnDatosFiscales input,#fnDatosFiscales select").prop("disabled", action);
        if (action)
            $("#GuardarFactura").fadeOut();
        else
            $("#GuardarFactura").fadeIn();

        if (actionArchivo)
            $("#headingTwo").parent().fadeOut();
        else
            $("#headingTwo").parent().fadeIn();

    }

    var cargarModal = function (info, info2, enabled) {

        desactivarModal(true, enabled);

        $("#IdFactura").val(info.IdFactura);

        $("#Cliente").val(info.IdCliente);



        $("#rdFact").prop('checked', !info.TipoFact);
        $("#rdProy").prop('checked', info.TipoFact);

        cargarInfoSelect($("#Cliente").val(), $("input[type='radio'][name='asigProy']:checked").val());

        if (info.TipoFact) {
            $("#contProyecto").fadeIn();
            $("#conAsignacion").fadeOut();
            $("#Proyecto").val(info.IdProyecto);
            $("#NoFactura").val(info.No_Factura);

        }
        else {
            $("#conAsignacion").fadeIn();
            $("#contProyecto").fadeOut();
            $("#sltAsignacion").val(info.IdAsignacion);
            $("#contDiasLaborados").val(info.DiasLaborados);
            $("#fecha_inicio").val(info.Fecha_Inicio_FacturaSTR);
            $("#fecha_fin").val(info.Fecha_fin_FacturaSTR);
            $("#Descontar").val(info.DiasDescuento);
        }

        $("#Monto").val(info.Monto_Factura);

        $("#Moneda").val(info.C_Id_Moneda);

        $("#IVA").val(info.C_Id_IVA);

        $("#TipoCambio").val(info.C_Id_Tipo_Cambio);

        $("#txtSubTotal").html("$" + info.Monto_Factura + " " + ($("#Moneda option:selected").html()));

        $("#txtIva").html("$" + ((info.Monto_Factura * parseFloat($("#IVA option:selected").html())) / 100) + " " + ($("#Moneda option:selected").html()));

        $("#txtTotal").html("$" + (parseFloat(info.Monto_Factura) + (parseFloat(info.Monto_Factura * parseFloat($("#IVA option:selected").html())) / 100) + " " + ($("#Moneda option:selected").html())));

        $("#Comentarios").val(info.Comentarios);

        $("#MetodoPago").val(info.C_Id_Metodo_Pago);

        $("#txtDigitos").val(info.Ultimos_4_digitos_Factura);

        //Cliente

        $("#ClienteFiscal").val(info2.Id_Cliente);
        $("#txtRazonSocial").val(info2.Razon_Social_Cliente);
        $("#txtRfc").val(info2.RFC_Cliente);
        $("#calle").val(info2.Calle_Cliente);
        $("#Exterior").val(info2.Exterior_Cliente);
        $("#Interior").val(info2.Interior_Cliente);
        $("#Colonia").val(info2.Colonia_Cliente);
        $("#CP").val(info2.CP_Cliente);
        $("#DelMpio").val(info2.DelMun_Cliente);
        $("#EstadoDomicilio").val(info2.Estado_Dom_Cliente);
        $("#Email").val(info2.Emails);

        $(info.PagosFacturas).each(function (i, cont) {


            $("#datatableFacturasCargadas tbody").append('<tr>'
                + '<td> ' + cont.IdPago + '</td> '
                + '<td> ' + cont.Monto + '</td> '
                + '<td> ' + cont.Concepto + '</td> '
                + '<td> ' + cont.Descripcion + '</td> '
                + '<td>' + ObtenerBotonesDoctos(cont.DocumentosFacturas) + '</td> '
                + '</tr >');

            //alert('entro');

        });

        $(".btnDoctoView").click(function () {
            //<button type="button" data-URL="/Facturas/3/documentos.pdf" data-MontoPago="$1000.00" data-ConceptoPago="Concepto 1" data-DescDocto="Descripcion 1" data-toggle="tooltip" title="Descripción 1" class="btnDoctoView btn btn-primary">Factura 1</button>
            var URL = $(this).attr("data-url");
            var Tipo = $(this).attr("data-tipo");

            if (Tipo == "true") {
                $.ajax({
                    type: "GET",
                    url: URL,
                    dataType: "text",
                    success: function (xml) {
                        debugger;
                        $("#objVistaPreviaCont").html('<textarea class="form-control" id="txtXMLViewer" style="width:100%; min-height:300px;" disabled>' + xml + '</textarea>');

                    }
                });
            }
            else {
                $("#objVistaPreviaCont").html('<iframe id="objVistaPrevia" src="' + URL + '"></iframe>      ');

                //$("#objVistaPrevia").attr("data", $(this).attr("data-URL"));
            }
            // $("#objVistaPrevia").attr("type", "text/xml-external-parsed-entity");
        });

    }

    var llenarDatosFiscales = function (IdCliente) {

        $.ajax({
            type: 'POST',
            url: '/Gestion/ObtenerDatosFiscales',
            data: { IdCliente: IdCliente },
            success: function (response) {
                if (response.success) {
                    var info2 = response.data[0];

                    $("#txtRazonSocial").val(info2.Razon_Social_Cliente);
                    $("#txtRfc").val(info2.RFC_Cliente);
                    $("#calle").val(info2.Calle_Cliente);
                    $("#Exterior").val(info2.Exterior_Cliente);
                    $("#Interior").val(info2.Interior_Cliente);
                    $("#Colonia").val(info2.Colonia_Cliente);
                    $("#CP").val(info2.CP_Cliente);
                    $("#DelMpio").val(info2.DelMun_Cliente);
                    $("#EstadoDomicilio").val(info2.Estado_Dom_Cliente);
                    $("#Email").val(info2.Emails);

                }
                else
                    alert(response.message);

            },
            error: function (xhr, ajaxOptions, thrownError) {
                var mensaje = 'Error al obtener datos fiscales:' + thrownError;
                alert(mensaje);

            }
        });

    }

    var ObtenerBotonesDoctos = function (values) {
        var botones = "";
        var imagenXML = '<img class="imgIcon" src="/Images/doc_pdf_icon.png" />';
        var imagenPDF = '<img class="imgIcon" src="/Images/doc_xml_icon.png" />';




        $(values).each(function (i, cont) {
            var tipo = (cont.Url.indexOf("xml") >= 0);
            botones += '<button type="button" data-URL="' + cont.Url + '" data-tipo="' + tipo + '" class="btnDoctoView btn">' + (tipo ? imagenPDF : imagenXML) + '</button>';

        });

        return botones;
    }

    var cargarInfoModal = function (IdFactura, enabled) {
        ModalCargando(true);

        $.ajax({
            type: 'POST',
            url: '/Gestion/ObtenerFacturas',
            data: { IdFacturas: IdFactura },
            success: function (response) {
                if (response.success) {

                    cargarModal(response.info[0], response.cliente[0], enabled);

                    $("#TitleModalAsignacion").html("Detalle - Factura");
                    $("#addFactura").modal("show");
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

    var cargarInfoSelect = function (cliente, tipo) {

        $.ajax({
            type: 'POST',
            url: '/Gestion/ObtenerProyectosCliente',
            async: false,
            data: {
                IdCliente: cliente,
                tipo: tipo
            },
            success: function (response) {

                if (response.success) {

                    $("#sltAsignacion").html(' <option value="0">Seleccione</option>');
                    $("#Proyecto").html(' <option value="0">Seleccione</option>');

                    if (tipo == 1) {
                        $(response.data).each(function (i, cont) {
                            $("#sltAsignacion").append(' <option value="' + cont.Id + '">' + cont.Value + '</option>');
                        });

                    }
                    else {
                        $(response.data).each(function (i, cont) {
                            $("#Proyecto").append(' <option value="' + cont.Id + '">' + cont.Value + '</option>');
                        });
                    }

                }
                else
                    alert(response.message);

            },
            error: function (xhr, ajaxOptions, thrownError) {

                var mensaje = 'Error al datos de combo:' + thrownError;
                alert(mensaje);

            }
        });

    }

    var cargarInfoNumeroFactura = function (Id, tipo) {

        $.ajax({
            type: 'POST',
            url: '/Gestion/ObtenerNumeroFactura',
            data: {
                Id: Id,
                tipo: tipo
            },
            success: function (response) {

                if (response.success) {


                    if (tipo == 1) {

                        $("#NoFactura").val(response.data);
                    }
                    else {
                        $("#NoFactura").val(response.data);
                    }

                }
                else
                    alert(response.message);

            },
            error: function (xhr, ajaxOptions, thrownError) {

                var mensaje = 'Error al datos de combo:' + thrownError;
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
                url: "/Gestion/TablePaginacionFacturas",
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
            { "data": "IdFactura" },
            { "data": "Tipo" },
            { "data": "Cliente" },
            { "data": "Concepto" },
            { "data": "Monto" },
            { "data": "Facturado" },
            { "data": "DiaFacturacion" },
            { "data": "Botones" }
            //{ "defaultContent": "<button type='button' class='editar btn btn-custom'>Facturar</button><button type='button' class='editar btn btn-danger'>Cancelar</button>" }
        ],
        columnDefs: [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            }, {
                "targets": [7],
                "visible": true,
                "searchable": false
            }]
    });

    $("#datatable tbody").on("click", "button.btnPago", function () {
        var data = table.row($(this).parents("tr")).data();
        cargarInfoModal(data["IdFactura"], false);
        desactivarModal(false, false);

    });

    $("#datatable tbody").on("click", "button.btnDetalle", function () {
        var data = table.row($(this).parents("tr")).data();
        cargarInfoModal(data["IdFactura"], true);
    });

    $(".conBtnAgregar").html('<button type="button" id="btnAddFactura" class="btn btn-custom dropdown-toggle waves-effect waves-light" data-toggle="modal" data-target="#addFactura">Agregar <span class="m-l-5"><i class="fa fa-plus-circle"></i></span></button>');

    $("#btnAddFactura").click(function () {
        $("#TitleModalAsignacion").html("Alta - Factura");
        $("#IdFactura").val('0');
        limpiarModal();
    });

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

    $('#btnUpload').click(function () {

        // Checking whether FormData is available in browser  
        if (window.FormData !== undefined) {

            var fileUpload = $("#FileUpload1").get(0);
            var files = fileUpload.files;

            // Create FormData object  
            var fileData = new FormData();

            // Looping over all files and add it to FormData object  
            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }

            // Adding one more key to FormData object  
            fileData.append('IdFactura', $("#IdFactura").val());
            fileData.append('Monto', $("#txtMontoPago").val());
            fileData.append('Concepto', $("#txtConceptoArchivo").val());
            fileData.append('Descripcion', $("#txtDescripcion").val());
            ModalCargando(true);
            $.ajax({
                url: '/Gestion/UploadFiles',
                type: "POST",
                contentType: false, // Not to set any content header  
                processData: false, // Not to process data  
                data: fileData,
                success: function (result) {
                    alert(result);
                    ModalCargando(false);
                },
                error: function (err) {
                    alert(err.statusText);
                    ModalCargando(false);
                }
            });
        } else {
            alert("FormData is not supported.");
        }
    });

    $(".Only-Number").keydown(function (e) {

        var tempVal = $(this).val() + e.key;
        if (e.keyCode != 8 && e.keyCode != 9)
            if (!(/^[0-9\.]*$/.test(+tempVal))) // OR if (/^[0-9]{1,10}$/.test(+tempVal) && tempVal.length<=10) 
                e.preventDefault();

    });

    $(".Only-Moneda").keydown(function (e) {

        var tempVal = $(this).val() + e.key;
        if (e.keyCode != 8 && e.keyCode != 9)
            if (!(/^\$(((\d{1,3},)(\d{3},)*\d{3})|(\d{1,3}))\.\d{2}$/.test(+tempVal))) // OR if (/^[0-9]{1,10}$/.test(+tempVal) && tempVal.length<=10) 
                e.preventDefault();

    });

    $(".Only-Letter").keydown(function (e) {

        var tempVal = $(this).val() + e.key;
        if (e.keyCode != 8 && e.keyCode != 9)
            if (!(/^[0-9]{1,10}$/.test(+tempVal))) // OR if (/^[0-9]{1,10}$/.test(+tempVal) && tempVal.length<=10) 
                e.preventDefault();

    });

    $(".Only-Date").keydown(function (e) {

        var tempVal = $(this).val() + e.key;
        if (e.keyCode != 8 && e.keyCode != 9)
            if (!(/^[0-9/]{1,10}$/.test(+tempVal))) // OR if (/^[0-9]{1,10}$/.test(+tempVal) && tempVal.length<=10) 
                e.preventDefault();

    });

    $("#rdFact,#rdProy").change(function () {
        cargarInfoSelect($("#Cliente").val(), $("input[type='radio'][name='asigProy']:checked").val());
        if ($(this).val() == 1) {
            //alert("Asignacion");
            $("#conAsignacion").fadeIn();
            $("#contProyecto").fadeOut();
        }
        else {
            //alert("Proyectp");
            $("#contProyecto").fadeIn();
            $("#conAsignacion").fadeOut();
        }
    });

    $("#Monto, #IVA, #Moneda").change(function () {

        debugger;
        var Monto = $("#Monto").val();
        var IVA = $("#IVA").val();
        var Moneda = $("#Moneda").val();

        if (Monto == '' || IVA == 0 || Moneda == 0) {
            return false;
        }


        $("#txtSubTotal").html("$" + Monto + " " + ($("#Moneda option:selected").html()));

        $("#txtIva").html("$" + ((Monto * parseFloat($("#IVA option:selected").html())) / 100) + " " + ($("#Moneda option:selected").html()));

        $("#txtTotal").html("$" + (parseFloat(Monto) + (parseFloat(Monto * parseFloat($("#IVA option:selected").html())) / 100) + " " + ($("#Moneda option:selected").html())));

    });

    $("#Cliente").change(function () {

        if ($(this).val() == 0)
            return false;

        var tipo = $("input[type='radio'][name='asigProy']:checked").val();

        cargarInfoSelect($(this).val(), tipo);

    });

    $("#GuardarFactura").click(function () {
        ModalCargando(true);

        $("#frIdEmpresa").val($("#sltEmpresa").val());

        //alert($("#fnGuardarProyecto").serialize());

        if (validarGuardarFactura()) {

            $.ajax({
                type: 'POST',
                url: '/Gestion/GuardarFacturas',
                data: $("#fnGuardarFactura").serialize(),
                success: function (response) {
                    if (response.success) {
                        limpiarModal();
                        table.draw();
                        $("#addFactura").modal("hide");
                    }
                    else
                        alert(response.message);

                    ModalCargando(false);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    var mensaje = 'Error al guardar asignación:' + thrownError;

                    ModalCargando(false);
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

    $("#Proyecto, #sltAsignacion").change(function () {

        debugger;
        var tipo = $("input[type='radio'][name='asigProy']:checked").val();

        if ($("#IdFactura").val() == '0')
            cargarInfoNumeroFactura($(this).val(), tipo);

    });

    $("#ClienteFiscal").change(function () {
        //alert("Entro");
        llenarDatosFiscales($(this).val());
    });

});


