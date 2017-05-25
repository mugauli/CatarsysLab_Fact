
$(document).ready(function () {

    var order = '';

    var validarGuardar = function () {

        var correcto = true;

        if ($("#Nombre").val() == '') {
            $("#fnNombre .errorMsg").html("Debe introducir el nombre del empleado.");
            $("#Nombre").addClass("inputError");
            correcto = false;
        } else {
            $("#fnNombre .errorMsg").html('');
            $("#Nombre").removeClass("inputError");
        }

        if ($("#Puesto").val() == '') {
            $("#fnPuesto .errorMsg").html("Debe introducir el puesto del empleado.");
            $("#Puesto").addClass("inputError");
            correcto = false;
        } else {
            $("#fnPuesto .errorMsg").html('');
            $("#Puesto").removeClass("inputError");
        }

        if ($("#fecha_ingreso").val() == '') {
            $("#fnFecha .errorMsg2").html("Debe ingresar la fecha de ingreso.");
            $("#fecha_ingreso").addClass("inputError");
            correcto = false;
        } else {
            $("#fnFecha .errorMsg2").html('');
            $("#fecha_ingreso").removeClass("inputError");
        }

        if ($("#fecha_nacimiento").val() == '') {
            $("#fnFecha .errorMsg").html('Debe agregar una fecha de nacimiento.');
            $("#fecha_nacimiento").addClass("inputError");
            correcto = false;
        } else {
            $("#fnFecha .errorMsg").html('');
            $("#fecha_nacimiento").removeClass("inputError");
        }

        if ($("#Email").val() == '') {
            $("#fnEmailSkype .errorMsg").html("Debe ingresar la dirección de correo electrónico.");
            $("#Email").addClass("inputError");
            correcto = false;
        } else {
            $("#fnEmailSkype .errorMsg").html('');
            $("#Email").removeClass("inputError");
        }

        if ($("#Skype").val() == '') {
            $("#fnEmailSkype .errorMsg2").html('Debe ingresar el skype.');
            $("#Skype").addClass("inputError");
            correcto = false;
        } else {
            $("#fnEmailSkype .errorMsg2").html('');
            $("#Skype").removeClass("inputError");
        }

        if ($("#Domicilio").val() == '') {
            $("#fnDomicilio .errorMsg").html("Debe ingresar el domicilio del empleado.");
            $("#Domicilio").addClass("inputError");
            correcto = false;
        } else {
            $("#fnDomicilio .errorMsg").html('');
            $("#Domicilio").removeClass("inputError");
        }

        if ($("#Usuario").val() == '' && $("#IsLogin").is("checked")) {
            $("#fnPerfilEstado .errorMsg").html('Debe ingresar el usuario si empleado tiene acceso al sistema.');
            $("#Usuario").addClass("inputError");
            correcto = false;
        } else {
            $("#fnPerfilEstado .errorMsg").html('');
            $("#Usuario").removeClass("inputError");
        }

        if ($("#Password").val() == '' && $("#IsLogin").is("checked")) {
            $("#fnPassword .errorMsg").html('Debe ingresar el password si empleado tiene acceso al sistema.');
            $("#Password").addClass("inputError");
            correcto = false;
        } else {
            $("#fnPassword .errorMsg").html('');
            $("#Password").removeClass("inputError");
        }


        if ($("#Password2").val() == '' && $("#IsLogin").is("checked")) {
            $("#fnPassword .errorMsg2").html('Debe ingresar el password si empleado tiene acceso al sistema.');
            $("#Password2").addClass("inputError");
            correcto = false;
        } else {
            $("#fnPassword .errorMsg2").html('');
            $("#Password2").removeClass("inputError");
        }


        return correcto;

    }

    var limpiarModal = function () {


        $("#Nombre").val('');
        $("#Puesto").val('');
        $("#JefeInmediato").val(0);
        $("#fecha_ingreso").val('');
        $("#fecha_nacimiento").val('');
        $("#Email").val('');
        $("#Skype").val('');
        $("#Movil").val('');
        $("#Casa ").val('');
        $("#Domicilio").val('');
        $("#Usuario").val('');
        $("#IsLogin").prop('checked', false);
        $("#Password").val('');
        $("#Password2").val('');

        $(".chkPermisoLeer").prop("checked", false);
        $(".chkPermisoEscribir").prop("checked", false);
    }

    var cargarModal = function (info) {

        $("#idEmpleado").val(info.Id_Empleado);
        $("#Nombre").val(info.Nombre_Empleado);
        $("#Puesto").val(info.Puesto_Empleado);
        $("#JefeInmediato").val(info.Id_JefeInmediato_Empleado);
        $("#fecha_ingreso").val(info.Antiguedad_EmpleadoSTR);
        $("#fecha_nacimiento").val(info.Fecha_Nacimiento_EmpleadoSTR);
        $("#Email").val(info.Email_Empleado);
        $("#Skype").val(info.Skype_Empleado);
        $("#Movil").val(info.Telefono_M_Empleado);
        $("#Casa ").val(info.Telefono_L_Empleado);
        $("#Domicilio").val(info.Domicilio_Empleado);
        $("#Usuario").val(info.Usuario_Empleado);
        $("#rdSI").attr('checked', info.Estado);
        $("#rdNO").attr('checked', !info.Estado);
        $("#rdlgSI").attr('checked', info.IsLogIn == 1);
        $("#rdlgNO").attr('checked', info.IsLogIn != 1);

        $(".chkPermisoLeer").prop("checked", false);
        $(".chkPermisoEscribir").prop("checked", false);

        $(info.EmpleadoPermiso).each(function (i, v) {
            if (v.Tipo_Permiso == 1)
                $("#cont" + v.Id_Permiso).find(".chkPermisoLeer").prop("checked", true);
            if (v.Tipo_Permiso == 2)
                $("#cont" + v.Id_Permiso).find(".chkPermisoEscribir").prop("checked", true);
            if (v.Tipo_Permiso == 3) {
                $("#cont" + v.Id_Permiso).find(".chkPermisoLeer").prop("checked", true);
                $("#cont" + v.Id_Permiso).find(".chkPermisoEscribir").prop("checked", true);
            }

        });

        $("#hdPermisos").val(createJSON());

    }

    var cargarInfoModal = function (IdEmpleado) {


        $.ajax({
            type: 'POST',
            url: '/Configuracion/ObtenerEmpleado',
            data: { IdEmpleado: IdEmpleado },
            success: function (response) {
                if (response.success) {
                    cargarModal(response.info[0]);
                    $("#TitleModalAsignacion").html("Detalle - Empleado");
                    $("#addModal").modal("show");
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
                url: "/Configuracion/TablePaginacionEmpleados",
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
            { "data": "IdEmpleado" },
            { "data": "Nombre" },
            { "data": "Puesto" },
            { "data": "Email" },
            { "data": "Movil" },
            { "data": "Antiguedad" }],
        columnDefs: [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            }]
    });

    var createJSON = function () {
        var item = {};
        jsonCompleto = [];

        jsonPermisos = [];

        $(".chkPermisos").each(function (i, v) {


          

            if ($(this).find(".chkPermisoLeer").is(":checked") && $(this).find(".chkPermisoEscribir").is(":checked")) {

                item = {};
                item["Id_Permiso"] = $(this).data("id");
                item["Tipo_Permiso"] = 3;
                jsonPermisos.push(item);
            }
            else if ($(this).find(".chkPermisoLeer").is(":checked")) {

                item = {};
                item["Id_Permiso"] = $(this).data("id");
                item["Tipo_Permiso"] = 1;
                jsonPermisos.push(item);
            }
            else if ($(this).find(".chkPermisoEscribir").is(":checked")) {

                item = {};
                item["Id_Permiso"] = $(this).data("id");
                item["Tipo_Permiso"] = 2;
                jsonPermisos.push(item);
            }
            else{

                item = {};
                item["Id_Permiso"] = $(this).data("id");
                item["Tipo_Permiso"] = 0;
                jsonPermisos.push(item);
            }

           

        });


        item = {};

        item["ltsPermisos"] = jsonPermisos;
        jsonCompleto.push(item);
        jsonString = JSON.stringify(jsonCompleto);
        return jsonString;
    }

    $('#datatable tbody').on('click', 'tr', function () {
        var data = table.row(this).data();
        cargarInfoModal(data["IdEmpleado"]);
    });

    $(".conBtnAgregar").html('<button type="button" id="agregarAsg" class="btn btn-custom dropdown-toggle waves-effect waves-light" data-toggle="modal" data-target="#addModal">Agregar <span class="m-l-5"><i class="fa fa-plus-circle"></i></span></button>');

    $("#agregarAsg").click(function () {
        $("#TitleModalAsignacion").html("Alta - Empleados");

        $("#idEmpleado").val('0');
        limpiarModal();
    });

    $("#sltEmpresa").change(function () {
        table.draw();
    });

    $("#Guardar").click(function () {
        //alert($("#fnGuardar").serialize());

        $("#frIdEmpresa").val($("#sltEmpresa").val());
        if (validarGuardar()) {

            $.ajax({
                type: 'POST',
                url: '/Configuracion/GuardarEmpleado',
                data: $("#fnGuardar").serialize(),
                success: function (response) {
                    if (response.success) {
                        limpiarModal();
                        table.draw();
                        $("#addModal").modal("hide");
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

    $('#fecha_nacimiento').datepicker({
        language: "es"

    });

    $('#fecha_ingreso').datepicker({
        language: "es"
    });

    $('#Password,#Password2').change(function () {

        if ($('#Password').val() == '' || $('#Password2').val() == '')
            return 0;


        var Password = $("#Password").val();
        var Password2 = $("#Password2").val();


        if (Password == Password2) {
            $("#fnPassword .errorMsg").html('');
            $("#Password2").removeClass("inputError");
            $("#Password").removeClass("inputError");
        }
        else {
            $("#fnPassword .errorMsg").html("Las contraseñas no coinciden");
            $("#Password").addClass("inputError");
            $("#Password2").addClass("inputError");
        }


    });

    $("#rdlgSI").change(function () {
        $(".userActive").fadeIn();
    });

    $("#rdlgNO").change(function () {
        $(".userActive").fadeOut();
    });

    $(".chkPermisoLeer, .chkPermisoEscribir").change(function () {
        $("#hdPermisos").val(createJSON());
    });

});