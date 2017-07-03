
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

        if ($("#Email").val() == '') {
            $("#fnEmail .errorMsg").html('Debe ingresar la dirección de correo electrónico de la empresa.');
            $("#Email").addClass("inputError");
            correcto = false;
        } else {
            $("#fnEmail .errorMsg").html('');
            $("#Email").removeClass("inputError");
        }

        if ($("#fecha_creacion").val() == '') {
            $("#fnFecha .errorMsg2").html('Debe ingresar la fecha de creación de la empresa.');
            $("#fecha_creacion").addClass("inputError");
            correcto = false;
        } else {
            $("#fnFecha .errorMsg2").html('');
            $("#fecha_creacion").removeClass("inputError");
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
        $("#Email").val('');
        $("#fecha_creacion").val('');
        $("#rdNO").attr("checked", true);

    }

    var cargarModal = function (info) {
        $("#idEmpresa").val(info.Id_Empresa);
        $("#Nombre").val(info.Nombre_Empresa);
        $("#RazonSocial").val(info.Razon_Social_Empresa);
        $("#RFC").val(info.RFC_Empresa);
        $("#calle").val(info.Calle_Empresa);
        $("#Exterior").val(info.No_Ext_Empresa);
        $("#Interior").val(info.No_Int_Empresa);
        $("#Colonia").val(info.Colonia_Empresa);
        $("#CP").val(info.CP_Empresa);
        $("#DelMpio").val(info.Del_Mun_Empresa);
        $("#EstadoDomicilio").val(info.Estado_Dom_Empresa);
        $("#Email").val(info.Email_Empresa);
        $("#fecha_creacion").val(info.Fecha_Creacion_EmpresaSTR);
        $("#rdNO").attr("checked", !info.Estado_Empresa);
        $("#rdSI").attr("checked", info.Estado_Empresa);
    }

    var cargarInfoModal = function (IdEmpresa) {

        ModalCargando(true);
        $.ajax({
            type: 'POST',
            url: '/Configuracion/ObtenerEmpresa',
            data: { IdEmpresa: IdEmpresa },
            success: function (response) {
                if (response.success) {
                    cargarModal(response.info[0]);
                    $("#TitleModalAsignacion").html("Detalle - Empresa");
                    $("#addModal").modal("show");
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
                url: "/Configuracion/TablePaginacionEmpresas",
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
            { "data": "Id_Empresa" },
            { "data": "Nombre_Empresa" },
            { "data": "Razon_Social_Empresa" },
            { "data": "Fecha_Creacion_Empresa" },
            { "data": "Estado_Empresa" }],
        columnDefs: [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            }]
    });

    $('#datatable tbody').on('click', 'tr', function () {
        var data = table.row(this).data();
        cargarInfoModal(data["Id_Empresa"]);
    });

    $(".conBtnAgregar").html('<button type="button" id="agregarAsg" class="btn btn-custom dropdown-toggle waves-effect waves-light" data-toggle="modal" data-target="#addModal">Agregar <span class="m-l-5"><i class="fa fa-plus-circle"></i></span></button>');

    $("#agregarAsg").click(function () {
        $("#TitleModalAsignacion").html("Alta - Empresa");
        $("#idEmpresa").val('0');
    });

    $("#sltEmpresa").change(function () {
        table.draw();
    });

    $("#Guardar").click(function () {
        //alert($("#fnGuardar").serialize());

        $("#frIdEmpresa").val($("#sltEmpresa").val());
        if (validarGuardar()) {
            ModalCargando(true);
            $.ajax({
                type: 'POST',
                url: '/Configuracion/GuardarEmpresa',
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

    $('#fecha_creacion').datepicker({
        language: "es"

    });

});