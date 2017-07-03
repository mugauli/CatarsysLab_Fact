
$(document).ready(function () {

    var order = '';

    var validarGuardar = function () {

        var correcto = true;

        if ($("#Nombre").val() == '') {
            $("#fnNombre .errorMsg").html("Debe ingresar el nombre de la empresa.");
            $("#Nombre").addClass("inputError");
            correcto = false;
        } else {
            $("#fnNombre .errorMsg").html('');
            $("#Nombre").removeClass("inputError");
        }

        if ($("#RazonSocial").val() == '') {
            $("#fnRFC .errorMsg").html("Debe ingresar la razón social de la empresa.");
            $("#RazonSocial").addClass("inputError");
            correcto = false;
        } else {
            $("#fnRFC .errorMsg").html('');
            $("#RazonSocial").removeClass("inputError");
        }

        if ($("#RFC").val() == '') {
            $("#fnRFC .errorMsg2").html("Debe ingresar el RFC de la empresa.");
            $("#RFC").addClass("inputError");
            correcto = false;
        } else {
            $("#fnRFC .errorMsg2").html('');
            $("#RFC").removeClass("inputError");
        }

        if ($("#calle").val() == '') {
            $("#fnCalle .errorMsg").html("Debe ingresar la calle del domicilio de la empresa.");
            $("#calle").addClass("inputError");
            correcto = false;
        } else {
            $("#fnCalle .errorMsg").html('');
            $("#calle").removeClass("inputError");
        }

        if ($("#Exterior").val() == '') {
            $("#fnCalle .errorMsg2").html('Debe ingresar el número exterior del domicilio de la empresa.');
            $("#Exterior").addClass("inputError");
            correcto = false;
        } else {
            $("#fnCalle .errorMsg2").html('');
            $("#Exterior").removeClass("inputError");
        }

        if ($("#Colonia").val() == '') {
            $("#fnColonia .errorMsg").html("Debe ingresar la colonia del domicilio de la empresa.");
            $("#Colonia").addClass("inputError");
            correcto = false;
        } else {
            $("#fnColonia .errorMsg").html('');
            $("#Colonia").removeClass("inputError");
        }

        if ($("#CP").val() == '') {
            $("#fnColonia .errorMsg").html('Debe ingresar el C.P. del domicilio de la empresa.');
            $("#CP").addClass("inputError");
            correcto = false;
        } else {
            $("#fnColonia .errorMsg").html('');
            $("#CP").removeClass("inputError");
        }

        if ($("#DelMpio").val() == '') {
            $("#fnEstadoDomicilio .errorMsg").html('Debe ingresar la delegación o el municipio del domicilio de la empresa.');
            $("#DelMpio").addClass("inputError");
            correcto = false;
        } else {
            $("#fnEstadoDomicilio .errorMsg").html('');
            $("#DelMpio").removeClass("inputError");
        }

        if ($("#EstadoDomicilio").val() == '') {
            $("#fnEstadoDomicilio .errorMsg2").html('Debe ingresar el estado del domicilio de la empresa.');
            $("#EstadoDomicilio").addClass("inputError");
            correcto = false;
        } else {
            $("#fnEstadoDomicilio .errorMsg2").html('');
            $("#EstadoDomicilio").removeClass("inputError");
        }

        if ($("#diasPago").val() == '') {
            $("#fnDiasPago .errorMsg").html('Debe ingresar los días de pago.');
            $("#diasPago").addClass("inputError");
            correcto = false;
        } else {
            $("#fnDiasPago .errorMsg").html('');
            $("#diasPago").removeClass("inputError");
        }

        return correcto;

    }

    var validarContacto = function () {

        var correcto = true;

        if ($("#Nombre_Contacto").val() == '') {
            $("#fnNombre_Contacto .errorMsg").html("Debe ingresar el nombre del contacto.");
            $("#Nombre_Contacto").addClass("inputError");
            correcto = false;
        } else {
            $("#fnNombre_Contacto .errorMsg").html('');
            $("#Nombre_Contacto").removeClass("inputError");
        }

        if ($("#Puesto_Contacto").val() == '') {
            $("#fnNombre_Contacto .errorMsg2").html("Debe ingresar el puesto del contacto.");
            $("#Puesto_Contacto").addClass("inputError");
            correcto = false;
        } else {
            $("#fnNombre_Contacto .errorMsg2").html('');
            $("#Puesto_Contacto").removeClass("inputError");
        }

        if ($("#Email_Contacto").val() == '') {
            $("#fnEmail_Contacto .errorMsg").html("Debe ingresar el email del contacto.");
            $("#Email_Contacto").addClass("inputError");
            correcto = false;
        } else {
            $("#fnEmail_Contacto .errorMsg").html('');
            $("#Email_Contacto").removeClass("inputError");
        }

        if ($("#Telefono_Contacto").val() == '' || $("#Movil_Contacto").val() == '') {
            $("#fnTelefono_Contacto .errorMsg").html("Debe ingresar la calle del domicilio de la empresa.");
            $("#Telefono_Contacto").addClass("inputError");
            correcto = false;
        } else {
            $("#fnTelefono_Contacto .errorMsg").html('');
            $("#Telefono_Contacto").removeClass("inputError");
        }

        return correcto;

    }

    var limpiarModal = function () {


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

    }

    var limpiarModalContacto = function () {


        $("#Nombre_Contacto").val('');
        $("#Puesto_Contacto").val('');        
        $("#Email_Contacto").val('');
        $("#Skype_Contacto").val('');
        $("#Movil_Contacto").val('');
        $("#Telefono_Contacto").val('');
        $("#Comentario_Contacto").val('');
    }

    var cargarModal = function (info) {

        $("#Nombre").val(info.Nombre_Cliente);
        $("#idCliente").val(info.Id_Cliente);
        $("#RazonSocial").val(info.Razon_Social_Cliente);
        $("#RFC").val(info.RFC_Cliente);
        $("#calle").val(info.Calle_Cliente);
        $("#Exterior").val(info.Exterior_Cliente);
        $("#Interior").val(info.Interior_Cliente);
        $("#Colonia").val(info.Colonia_Cliente);
        $("#CP").val(info.CP_Cliente);
        $("#DelMpio").val(info.DelMun_Cliente);
        $("#EstadoDomicilio").val(info.Estado_Dom_Cliente);
        $("#diasPago").val(info.Dias_de_Pago_Cliente);   
        
        $("#rdNO").attr("checked", !info.Estado);
        $("#rdSI").attr("checked", info.Estado);

        $("#datatableContactos tbody").html('');

        $(info.Contactos).each(function (i, cont) {
            var enviar = 'NO';
            if (cont.EnviaFactura_Contacto) enviar = 'SI';

            $("#datatableContactos tbody").append('<tr class="detalleContacto" data-id="' + cont.Id_Contacto + '"  data-telefono="' + cont.Telefono_Contacto + '" data-skype="' + cont.Skype_Contacto + '" data-estado="' + cont.Estado + '" data-movil="' + cont.Movil__Contacto + '" data-envia="' + cont.EnviaFactura_Contacto + '">'
                + '<td class="nombre"> ' + cont.Nombre_Contacto + '</td> '
                + '<td class="puesto"> ' + cont.Puesto_Contacto + '</td> '
                + '<td class="email"> ' + cont.Email_Contacto + '</td> '
                + '<td class="movil"> ' + cont.Movil__Contacto + '</td> '
                + '<td class="envia"> ' + enviar + '</td> '
                + '<td class="comentario"> ' + cont.Comentario_Contacto + '</td> '
                + '</tr >');

            //alert('entro');

        });

    }

    var cargarModalContacto = function (contactos) {

        $("#idContacto").val(contactos.data("id"));
        $("#idClienteContacto").val($("#idCliente").val());        
        $("#Nombre_Contacto").val(contactos.find(".nombre").html());
        $("#Puesto_Contacto").val(contactos.find(".puesto").html());
        $("#Email_Contacto").val(contactos.find(".email").html());
        $("#Skype_Contacto").val(contactos.data("skype"));        
        $("#Movil_Contacto").val(contactos.data("movil"));
        $("#Telefono_Contacto").val(contactos.data("telefono"));
        $("#Comentario_Contacto").val(contactos.find(".comentario").html());

        $("#rdCtNO").attr("checked", !contactos.data("estado"));
        $("#rdCtSI").attr("checked", contactos.data("estado"));

        $("#efNO").attr("checked", !contactos.data("envia"));
        $("#efSI").attr("checked", contactos.data("envia"));

    }

    var cargarContactoTabla = function (contactos) {
        $("#datatableContactos tbody").html("");
        $(contactos).each(function (i, cont) {
            var enviar = 'NO';
            if (cont.EnviaFactura_Contacto) enviar = 'SI';

            $("#datatableContactos tbody").append('<tr class="detalleContacto" data-id="' + cont.Id_Contacto + '"  data-telefono="' + cont.Telefono_Contacto + '" data-skype="' + cont.Skype_Contacto + '" data-estado="' + cont.Estado + '" data-movil="' + cont.Movil__Contacto + '">'
                + '<td class="nombre"> ' + cont.Nombre_Contacto + '</td> '
                + '<td class="puesto"> ' + cont.Puesto_Contacto + '</td> '
                + '<td class="email"> ' + cont.Email_Contacto + '</td> '
                + '<td class="movil"> ' + cont.Movil__Contacto + '</td> '
                + '<td class="envia"> ' + enviar + '</td> '
                + '<td class="comentario"> ' + cont.Comentario_Contacto + '</td> '
                + '</tr >');

            //alert('entro');

        });
    }

    var cargarInfoModal = function (IdCliente) {
        ModalCargando(true);

        $.ajax({
            type: 'POST',
            url: '/Configuracion/ObtenerCliente',
            data: { IdCliente: IdCliente },
            success: function (response) {
                ModalCargando(false);
                if (response.success) {
                    cargarModal(response.info[0]);
                    $("#TitleModalAsignacion").html("Detalle - Cliente");
                    $("#addModal").modal("show");
                }
                else
                    alert(response.message);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                var mensaje = 'Error al obtener asignación:' + thrownError;
                alert(mensaje);
                ModalCargando(false);
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
                url: "/Configuracion/TablePaginacionClientes",
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
            { "data": "Id_Cliente" },
            { "data": "Nombre_Cliente" },
            { "data": "Razon_Social_Cliente" },
            { "data": "RFC_Cliente" },
            { "data": "Contacto" }],
        columnDefs: [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            }]
    });

    $('#datatable tbody').on('click', 'tr', function () {
        var data = table.row(this).data();
        cargarInfoModal(data["Id_Cliente"]);
    });

    $(".conBtnAgregar").html('<button type="button" id="agregarAsg" class="btn btn-custom dropdown-toggle waves-effect waves-light" data-toggle="modal" data-target="#addModal">Agregar <span class="m-l-5"><i class="fa fa-plus-circle"></i></span></button>');

    $("#agregarAsg").click(function () {
        $("#TitleModalAsignacion").html("Alta - Cliente");
        $("#idCliente").val('0');
    });

    $("#sltEmpresa").change(function () {
        table.draw();
    });

    $("#Guardar").click(function () {
        alert($("#fnGuardar").serialize());

        $("#frIdEmpresa").val($("#sltEmpresa").val());
        if (validarGuardar()) {
            ModalCargando(true);
            $.ajax({
                type: 'POST',
                url: '/Configuracion/GuardarCliente',
                data: $("#fnGuardar").serialize(),
                success: function (response) {
                    if (response.success) {
                        limpiarModal();
                        table.draw();
                        $("#addModal").modal("hide");
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

    $("#datatableContactos tbody").on('click', 'tr', function () {
        $("#addModal").modal("hide");
        cargarModalContacto($(this));
        $("#addModalContacto").modal("show");
    });

    $('#addModalContacto').on('hidden.bs.modal',  function () {
        $("#addModal").modal("show");
    });

    //$("#CancelarContacto").click(function () {
    //    $("#addModal").modal("show");
    //});

    $("#GuardarContacto").click(function () {
        alert($("#fnContacto").serialize());

        $("#frIdEmpresa").val($("#sltEmpresa").val());
        if (validarGuardar()) {
            ModalCargando(true);
            $.ajax({
                type: 'POST',
                url: '/Configuracion/GuardarContacto',
                data: $("#fnContacto").serialize(),
                success: function (response) {
                    if (response.success) {
                        cargarContactoTabla(response.contactos);
                        limpiarModalContacto();
                        $("#addModalContacto").modal("hide");
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

    $("#agregarAsgContacto").click(function () {
        $("#addModal").modal("hide");

        $("#idContacto").val(0);
        $("#idClienteContacto").val($("#idCliente").val());   
        
    });


});