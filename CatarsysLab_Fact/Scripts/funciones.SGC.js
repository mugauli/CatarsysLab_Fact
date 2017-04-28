/**
* Depende de:
* numeral.js, bootstrap.js, bootbox.js, notify.js
*
*
*
*/
$(document).on("keydown", function (e) {
    if (e.which === 8 && !$(e.target).is("input, textarea")) {
        e.preventDefault();
    }
});


appSGC = {
    /**
        Inserta una alerta bootstrap en el append del parametro selector
        @selector: etiqueta donde hará el append
        @type: danger|success|info|warning|default
        @strong: texto en strong dentro de la alerta
        @message: mensaje dentro de la alerta
    */
    alerta: function (selector, type, strong, message) {
        var html = '<div role="alert" class="alert alert-' + type + '"><button type="button" class="close" data-dismiss="alert">&times;</button>' + '<strong>' + strong + '</strong> ' + message + '</div>';
        $(selector).append(html);
    },
    /**
        Muestra una alerta bootbox en la pantalla
        @clase: la clase de la alerta danger|success|info|warning
        @title: El título de la alerta
        @mensaje: el mensaje en el body de la alerta, puedes ser html
    */
    alertaModal: function (clase, title, mensaje, callback) {
        var dialog = bootbox.dialog({
            message: mensaje,
            title: title,
            onEscape: function () {
                if (callback)
                    callback();
            },
            backdrop: true,
            closeButton: false,
            className: clase,
            buttons: {
                cancel: {
                    label: "Cerrar",
                    className: "btn-default botonjhS ",
                    callback: function () {
                        if (callback)
                            callback();
                    }
                }
            }
        });

        dialog.on('shown.bs.modal', function () {
            dialog.find("button:first").focus();
        });
    },
    /**
        Muestra una alerta bootbox en la pantalla con botones customizados
        @clase: la clase de la alerta danger|success|info|warning
        @title: El título de la alerta
        @mensaje: el mensaje en el body de la alerta, puedes ser html
        @buttons: JSON que contiene los botones de la alerta
        @callback: function que recibe por parametro el dialogo creado. ej. function(dialog) { dialog.find("select,input:first").focus(); }; 
    */
    alertaModalCustom: function (clase, title, mensaje, buttons, callback){
        var dialog = bootbox.dialog({
            message: mensaje,
            backdrop: true,
            closeButton: false,
            title: title,
            className: clase,
            buttons: buttons
        });

        dialog.on('shown.bs.modal', function () {
            dialog.find("button:first").focus();

            if (callback)
                callback(dialog);
        });
    },
    /**
    Inserta una alerta en la barra de alertas de usuario
    @alerta: objeto de la clase AlertMessageDTO
    */
    addAlertUser: function (alerta, callback, notificar)
    {
        var fnArchivaAlerta = function (idAlerta) {
            $.ajax({
                type: 'POST',
                url: '/Aplicacion/ArchivaAlerta',
                data: { idAlerta: idAlerta },
                success: function (response) {
                    if (response.success == false) {
                        var dialog = bootbox.dialog({
                            message: 'ERROR: ' + response.mensaje,
                            title: '¡ERROR!',
                            onEscape: function () {
                                //if (callback)
                                //    callback();
                            },
                            backdrop: true,
                            closeButton: false,
                            className: 'default',
                            buttons: {
                                cancel: {
                                    label: "Cerrar",
                                    className: "btn-default",
                                    callback: function () {
                                        //if (callback)
                                        //    callback();
                                    }
                                }
                            }
                        });

                        dialog.on('shown.bs.modal', function () {
                            dialog.find("button:first").focus();
                        });
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    var mensaje = 'ERROR AL OBTENER ALERTAS:' + thrownError;
                    var dialog = bootbox.dialog({
                        message: 'ERROR: ' + mensaje,
                        title: '¡ERROR!',
                        onEscape: function () {
                            //if (callback)
                            //    callback();
                        },
                        backdrop: true,
                        closeButton: false,
                        className: 'default',
                        buttons: {
                            cancel: {
                                label: "Cerrar",
                                className: "btn-default",
                                callback: function () {
                                    //if (callback)
                                    //    callback();
                                }
                            }
                        }
                    });

                    dialog.on('shown.bs.modal', function () {
                        dialog.find("button:first").focus();
                    });
                }
            });
        };

        var countAlert = Number($('#layoutContAlertas').val());

        var idAlert = "alert-message_" + countAlert;

        var smsAlert = '<div id="' + alerta.Id +'" class="alert alert-' + alerta.ClassAlert + ' alerta-msg">' +
                       '<button type="button" class="close" data-dismiss="alert">&times;</button>' +
                       '<strong> ' + (countAlert + 1) + '.- ' + alerta.Titulo + ' </strong>&nbsp;<small>' + alerta.Mensaje +
                       '&nbsp;<a class="lnk-alerta" id="' + idAlert + '" href="#">' + alerta.TextoLink + '</a></small>' +
                       '</div>';
        
        // actualizo contador de alertas hdn
        countAlert++;
        $('#layoutContAlertas').val(countAlert);

        // actualizo label de no de alertas
        var labelAlert = Number($('#alert-count').text());
        labelAlert++;
        $('#alert-count').text(labelAlert);

        $('#alertas-div').prepend(smsAlert);

        $('#' + idAlert + '.lnk-alerta').click(function () {
            callback();
        });

        $('#' + idAlert + '.lnk-alerta').closest('div').find('.close').click(function () {
            var count = Number($('#alert-count').text());
            count--;
            if(count > 0)
                $('#alert-count').text(count);
            else
                $('#alert-count').text('');

            fnArchivaAlerta(alerta.Id);
        });

        if (notificar || typeof notificar == 'undefined') {

            switch (alerta.ClassAlert) // corrijo clases para notify
            {
                case 'danger': alerta.ClassAlert = 'error';
                    break;
                case 'warning': alerta.ClassAlert = 'warn';
                    break;
            }

            $.notify(alerta.Mensaje, {
                globalPosition: 'bottom right',
                autoHideDelay: 9000,
                className: alerta.ClassAlert, // success, info, warn, error
                showDuration: 200,
                hideAnimation: 'slideUp',
                hideDuration: 200
            });

            if(alerta.AlrtRecibida == null || alerta.AlrtRecibida == 0)
            {
                $.ajax({
                    type: 'POST',
                    url: '/Aplicacion/MarcaAlertaRecibida',
                    data: { idAlerta: alerta.Id },
                    success: function (response) {
                        if (!response.success)
                            console.log(response.mensaje);
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        console.log(thrownError);
                    }
                });
            }

        }

        if(alerta.Titulo.indexOf("Referencia") > -1)
        {
            if (avanceMas)
                avanceMas();
        }
    },
    /**
    Muestra modal de espera, solo se puede cerrar con waitHide()
    */
    waitShow: function()
    {
        $('#WaitModal').modal('show');
    },
    /**
    Oculta modal de espera
    */
    waitHide: function()
    {
        $('#WaitModal').modal('hide');
    },
    /**
    Convierte una fecha Date a string con el formato 09/12/1969 06:00:00 PM
    @date: la fecha
    */
    convierteFechaAFormatoString: function(date)
    {
        //var convertDateToFormat = function (d) { // formato: dd/mm/yyyy
        //    function pad(s) { return (s < 10) ? '0' + s : s; }
        //    return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('/');
        //};

        //var convertTimeToFormat = function (d) { // hh:mm:ss a.m.|p.m.
        //    function pad(s) {
        //        if (s == '00')
        //            return s;
        //        var sint = numeral(s).value();
        //        return (sint < 10) ? '0' + s : s;
        //    }
        //    var time = d.toLocaleTimeString('en-US').replace('AM', 'a.m.').replace('PM', 'p.m.'); // formato: 6:00:00 PM

        //    var splitTime = time.split(' ');
        //    var hours = splitTime[0].split(':')[0];
        //    var mins = splitTime[0].split(':')[1];
        //    var secs = splitTime[0].split(':')[2];

        //    return [pad(hours), pad(mins), pad(secs)].join(':') + ' ' + splitTime[1];
        //};
        return moment(date).format('DD/MM/YYYY hh:mm a');  //convertDateToFormat(date) + ' ' + convertTimeToFormat(date); // formato: 09/12/1969 06:00:00 PM
    },
    /**
    Funcion que verifica si un string cumple con la expresión regular enviada por parametro
    @r: expresion regular, ej: /[0-9]{1,8}/
    @str: cadena de caracteres a validar
    */
    matchExact: function(r, str) {
        var match = str.match(r);
        return match != null && str == match[0];
    },
    /**
    Funcion que transforma un querystring a un objeto JSON 
    @qs: query string, ej: '?idSolicitud=1&idUsuario=2'
    */
    queryStringToJSON: function(qs) {            
        var pairs = qs.slice(1).split('&');
    
        var result = {};
        pairs.forEach(function(pair) {
            pair = pair.split('=');
            result[pair[0]] = decodeURIComponent(pair[1] || '');
        });

        return JSON.parse(JSON.stringify(result));
    },
    /**
    Funcion que corrige bug a la hora de hacer resize al menu actualiza el tamaño del header de las tablas
    */
    resizeGrid: function($table) {
        // Get width of parent container
        // 
        if ($('.dataTable').length > 0) {
            console.log('resizeGrid()');
            setTimeout(function () {
                $($table).width('100%');
                $('.dataTables_scrollHeadInner').width($table.width());
                $('.dataTables_scrollHeadInner .table').width($table.width());
            }, 200)
        }
    },
    //Fucnion para validar los datos de RFC y CURP sean correctos
    fValidarRFCyCURP: function (RFCManual, CURPManual, ApellidoP, ApellidoM, Nombre, FechaNacimiento, Sexo, EstadoNac) {
        
        var sMensaje = "OK";
        var sError = "<ul>";
        //Se valida que no esten vacios los campos
        if (ApellidoP == null || ApellidoM == null || Nombre == null || FechaNacimiento == null)
            return "<ul>Ingrese la informacion personal del trabajador.</ul> <li style='margin-left:80px;'> Nombre </li><li  style='margin-left:80px;'> Apellidos</li><li  style='margin-left:80px;'> Fecha Nacimiento</li> ";

        var Nom1 = Nombre.substring(0, 1);
        if (RFCManual != null) {
            //Validamos la estrucuta del rfc y curp
            if (RFCManual.substring(0, 2) != ApellidoP.substring(0, 2))
                sError += "<li style='margin-left:80px;'> Las dos primeras letras del apellido paterno no coinciden con RFC.</li>";

            if (RFCManual.substring(2, 3) != ApellidoM.substring(0, 1))
                sError += "<li style='margin-left:80px;'>La primera letra del apellido materno no coincide con RFC.</li>";

            if (RFCManual.substring(3, 4) != Nom1)
                sError += "<li style='margin-left:80px;'>La primera letra del nombre no coincide con RFC.</li>";

            //Validamos la fecha
            if (RFCManual.substring(4, 6) != FechaNacimiento.substring(8, 10))
                sError += "<li style='margin-left:80px;'>El año de nacimiento no coincide con RFC.</li>";

            if (RFCManual.substring(6, 8) != FechaNacimiento.substring(3, 5))
                sError += "<li style='margin-left:80px;'>El mes de nacimiento no coincide con RFC.</li>";

            if (RFCManual.substring(8, 10) != FechaNacimiento.substring(0, 2))
                sError += "<li style='margin-left:80px;'>El día de nacimiento no coincide con RFC.</li>";

            //Validamos si el CURP
        } else if (CURPManual != null) {

            if (Sexo == null || EstadoNac == null)
                sError += "<ul>Ingrese la informacion personal del trabajador.</ul> <li style='margin-left:80px;'>Genero</li><li  style='margin-left:80px;'> Estado de Nacimiento</li> ";

            if (CURPManual.substring(0, 2) != ApellidoP.substring(0, 2))
                sError += "<li style='margin-left:80px;'>Las dos primeras letras del apellido paterno no coinciden con CURP.</li>";

            if (CURPManual.substring(2, 3) != ApellidoM.substring(0, 1))
                sError += "<li style='margin-left:80px;'>La primera letra del apellido materno no coincide con CURP.</li>";

            if (CURPManual.substring(3, 4) != Nom1)
                sError += "<li style='margin-left:80px;'>La primera letra del nombre no coincide con CURP.</li>";

            //Validamos la fecha
            if (CURPManual.substring(4, 6) != FechaNacimiento.substring(8, 10))
                sError += "<li style='margin-left:80px;'>El año de nacimiento no coincide con CURP.</li>";

            if (CURPManual.substring(6, 8) != FechaNacimiento.substring(3, 5))
                sError += "<li style='margin-left:80px;'>El mes de nacimiento no coincide con CURP.</li>";

            if (CURPManual.substring(8, 10) != FechaNacimiento.substring(0, 2))
                sError += "<li style='margin-left:80px;'>El día de nacimiento no coincide con CURP.</li>";

            //Validamos el sexo
            if (CURPManual.substring(10, 11) != Sexo)
                sError += "<li style='margin-left:80px;'>El género no coincide con el CURP.</li>";

            var edoJ="";
             //Validacion de estado contra curp           
            switch (EstadoNac) {
                case "AGUASCALIENTES":
                    edoJ = "AS";
                    break;
                case "BAJA CALIFORNIA":
                    edoJ = "BC";
                    break;
                case "BAJA CALIFORNIA SUR":
                    edoJ = "BS";
                    break;
                case "CAMPECHE":
                    edoJ = "CC";
                    break;
                case "CHIAPAS":
                    edoJ = "CS";
                    break;
                case "CHIHUAHUA":
                    edoJ = "CH";
                    break;
                case "COAHUILA DE ZARAGOZA":
                    edoJ = "CL";
                    break;
                case "COLIMA":
                    edoJ = "CM";
                    break;
                case "DISTRITO FEDERAL":
                    edoJ = "DF";
                    break;
                case "CIUDAD DE MEXICO":
                    edoJ = "DF";
                    break;
                case "DURANGO":
                    edoJ = "DG";
                    break;
                case "GUANAJUATO":
                    edoJ = "GT";
                    break;
                case "GUERRERO":
                    edoJ = "GR";
                    break;
                case "HIDALGO":
                    edoJ = "HG";
                    break;
                case "JALISCO":
                    edoJ = "JC";
                    break;
                case "ESTADO DE MEXICO":
                case "MEXICO":
                    edoJ = "MC";
                    break;
                case "MICHOACAN DE OCAMPO":
                    edoJ = "MN";
                    break;
                case "MORELOS":
                    edoJ = "MS";
                    break;
                case "NAYARIT":
                    edoJ = "NT";
                    break;
                case "NUEVO LEON":
                    edoJ = "NL";
                    break;
                case "OAXACA":
                    edoJ = "OC";
                    break;
                case "PUEBLA":
                    edoJ = "PL";
                    break;
                case "QUERETARO ARTEAGA":
                    edoJ = "QT";
                    break;
                case "QUINTANA ROO":
                    edoJ = "QR";
                    break;
                case "SAN LUIS POTOSI":
                    edoJ = "SP";
                    break;
                case "SINALOA":
                    edoJ = "SL";
                    break;
                case "SONORA":
                    edoJ = "SR";
                    break;
                case "TABASCO":
                    edoJ = "TC";
                    break;
                case "TAMAULIPAS":
                    edoJ = "TS";
                    break;
                case "TLAXCALA":
                    edoJ = "TL";
                    break;
                case "VERACRUZ":
                    edoJ = "VZ";
                    break;
                case "YUCATAN":
                    edoJ = "YN";
                    break;
                case "ZACATECAS":
                    edoJ = "ZS";
                    break;
                case "NACIONALIDAD  EXTRANJERA":
                    edoJ = "NE";
                    break;
                default:
                    edoJ = "";
                    }
                      //Se comenta para que el sistema pueda guardar el rfc que ingreso el usuario manualmente
                    if (edoJ != CURPManual.substring(11, 13)) {
                       sError += "<li style='margin-left:80px;'>Las dos primeras letras del estado no coinciden con la CURP.</li>";
                    }                
        }

        if (sError != "<ul>") {
            sError += "</ul>";
            sMensaje = sError;
        }

        return sMensaje;

    },

    fValidarEstructuraRFCCURP: function (RFC, CURP) {
        
        var aArray = ["1","2","3","4","5","6","7","8","9","0"];
        if (RFC != null && CURP != null) {
            //Valido RFC        
            for (var i = 1; i <=4; i++) {
                if (RFC.substring(i - 1, i) == "1" || RFC.substring(i - 1, i) == "2" || RFC.substring(i - 1, i) == "3" || RFC.substring(i - 1, i) == "4"
                    || RFC.substring(i - 1, i) == "5" || RFC.substring(i - 1, i) == "6" || RFC.substring(i - 1, i) == "7" || RFC.substring(i - 1, i) == "8"
                    || RFC.substring(i - 1, i) == "9" || RFC.substring(i - 1, i) == "0")
                        return 'LA ESTRUCTURA DEL RFC CONSTA DE 4 LETRAS SEGUIDO POR 6 DÍGITOS Y 3 CARACTERES ALFANUMERÍCOS.';
            }
            
            for (var i = 5; i <= 10; i++) {
                if ($.inArray(RFC.substring(i - 1, i), aArray) == -1)
                        return 'LA ESTRUCTURA DEL RFC CONSTA DE 4 LETRAS SEGUIDO POR 6 DÍGITOS Y 3 CARACTERES ALFANUMERÍCOS.';
            }
       
            //VAlido CURP                        
            for (var i = 1; i <= 4; i++) {
                if (CURP.substring(i - 1, i) == "1" || CURP.substring(i - 1, i) == "2" || CURP.substring(i - 1, i) == "3" || CURP.substring(i - 1, i) == "4"
                    || CURP.substring(i - 1, i) == "5" || CURP.substring(i - 1, i) == "6" || CURP.substring(i - 1, i) == "7" || CURP.substring(i - 1, i) == "8"
                    || CURP.substring(i - 1, i) == "9" || CURP.substring(i - 1, i) == "0")
                    return 'LA ESTRUCTURA DEL CURP CONSTA DE 4 LETRAS SEGUIDO POR 6 DÍGITOS,6 LETRAS Y 2 CARACTERES ALFANUMERÍCOS.';
            }
            
            for (var i = 5; i <=10; i++) {
                if ($.inArray(CURP.substring(i - 1, i), aArray) == -1)
                    return 'LA ESTRUCTURA DEL CURP CONSTA DE 4 LETRAS SEGUIDO POR 6 DÍGITOS,6 LETRAS Y 2 CARACTERES ALFANUMERÍCOS.';
            }
            
            for (var i = 12; i < 16; i++) {
                if (CURP.substring(i - 1, i) == "1" || CURP.substring(i - 1, i) == "2" || CURP.substring(i - 1, i) == "3" || CURP.substring(i - 1, i) == "4"
              || CURP.substring(i - 1, i) == "5" || CURP.substring(i - 1, i) == "6" || CURP.substring(i - 1, i) == "7" || CURP.substring(i - 1, i) == "8"
              || CURP.substring(i - 1, i) == "9" || CURP.substring(i - 1, i) == "0")
                    return 'LA ESTRUCTURA DEL CURP CONSTA DE 4 LETRAS SEGUIDO POR 6 DÍGITOS,6 LETRAS Y 2 CARACTERES ALFANUMERÍCOS.';
            }
           
        }
        return "OK";
    },
    muestraAlertaCierreSession: true
}