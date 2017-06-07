using AutoMapper;
using DTO.Catalogos;
using DTO.Gestion;
using DTO.Paginador;
using DTO.Seguridad;
using ModeloDatos.Gestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloDatos
{
    public class AutoMapperBootstrapperModeloDatos
    {

        public static void Configurar()
        {
            #region Asignacion
            //DB --> DTO
            Mapper.CreateMap<Asignacion, AsignacionDTO>()
                .ForMember(dest => dest.Usuarios, opt => opt.MapFrom(src => Mapper.Map<EmpleadosDTO>(src.Empleados))).ForMember(dest => dest.Clientes, opt => opt.MapFrom(src => Mapper.Map<ClientesDTO>(src.Clientes)))
                .ForMember(dest => dest.C_Estatus, opt => opt.MapFrom(src => Mapper.Map<C_EstatusDTO>(src.C_Estatus)))
                .ForMember(dest => dest.Fecha_Fin_AsignacionSTR, opt => opt.MapFrom(src => src.Fecha_Fin_Asignacion != null ? src.Fecha_Fin_Asignacion.Value.ToString("dd/MM/yyyy") : ""))
                .ForMember(dest => dest.Fecha_Ini_AsignacionSTR, opt => opt.MapFrom(src => src.Fecha_Ini_Asignacion != null ? src.Fecha_Ini_Asignacion.Value.ToString("dd/MM/yyyy") : ""))
                .ForMember(dest => dest.Duracion, opt => opt.ResolveUsing(typeof(DuracionAsignacionResolver)));
            Mapper.AssertConfigurationIsValid();

            //DTO --> DB

            Mapper.CreateMap<AsignacionDTO, Asignacion>()
               .ForMember(dest => dest.Clientes, opt => opt.Ignore())
               .ForMember(dest => dest.C_Corte, opt => opt.Ignore())
               .ForMember(dest => dest.C_Estatus, opt => opt.Ignore())
               .ForMember(dest => dest.C_IVA, opt => opt.Ignore())
               .ForMember(dest => dest.C_Moneda, opt => opt.Ignore())
               .ForMember(dest => dest.C_Periodos, opt => opt.Ignore())
               .ForMember(dest => dest.C_Tipo_Asignacion, opt => opt.Ignore())
               .ForMember(dest => dest.Empresa, opt => opt.Ignore())
               .ForMember(dest => dest.Facturas, opt => opt.Ignore())
               .ForMember(dest => dest.Empleados, opt => opt.Ignore());
            Mapper.AssertConfigurationIsValid();
            #endregion

            #region EmpleadoPermiso
            //DB --> DTO
            Mapper.CreateMap<EmpleadoPermiso, EmpleadoPermisoDTO>();
            Mapper.AssertConfigurationIsValid();
            //DTO --> DB

            Mapper.CreateMap<EmpleadoPermisoDTO, EmpleadoPermiso>()
                .ForMember(dest => dest.ctPermisos, opt => opt.Ignore())
            .ForMember(dest => dest.Empleados, opt => opt.Ignore());
            Mapper.AssertConfigurationIsValid();
            #endregion

            #region Empleados
            //DB --> DTO
            Mapper.CreateMap<Empleados, EmpleadosDTO>()
                 .ForMember(dest => dest.Fecha_Nacimiento_EmpleadoSTR, opt => opt.MapFrom(src => src.Fecha_Nacimiento_Empleado == null ? string.Empty : src.Fecha_Nacimiento_Empleado.Value.ToString("dd/MM/yyyy")))
                  .ForMember(dest => dest.Antiguedad_EmpleadoSTR, opt => opt.MapFrom(src => src.Antiguedad_Empleado == null ? string.Empty : src.Antiguedad_Empleado.Value.ToString("dd/MM/yyyy")))
                  .ForMember(dest => dest.EmpleadoPermiso, opt => opt.ResolveUsing(src => Mapper.Map<List<EmpleadoPermisoDTO>>(src.EmpleadoPermiso)));
            Mapper.AssertConfigurationIsValid();



            //DTO --> DB

            Mapper.CreateMap<EmpleadosDTO, Empleados>()
                 .ForMember(dest => dest.Empresa, opt => opt.Ignore())
                  .ForMember(dest => dest.Empleados1, opt => opt.Ignore())
                   .ForMember(dest => dest.Empleados2, opt => opt.Ignore())
                   .ForMember(dest => dest.EmpleadoPermiso, opt => opt.Ignore())
               .ForMember(dest => dest.Asignacion, opt => opt.Ignore());



            Mapper.AssertConfigurationIsValid();
            #endregion

            #region Empresas
            //DB --> DTO
            Mapper.CreateMap<Empresa, EmpresaDTO>()
                .ForMember(dest => dest.Fecha_Creacion_EmpresaSTR, opt => opt.MapFrom(src => src.Fecha_Creacion_Empresa == null ? string.Empty : src.Fecha_Creacion_Empresa.Value.ToString("dd/MM/yyyy")));
            Mapper.AssertConfigurationIsValid();

            //DTO --> DB

            Mapper.CreateMap<EmpresaDTO, Empresa>()
                 .ForMember(dest => dest.Asignacion, opt => opt.Ignore())
                  .ForMember(dest => dest.Clientes, opt => opt.Ignore())
                   .ForMember(dest => dest.Empleados, opt => opt.Ignore())
                   .ForMember(dest => dest.Facturas, opt => opt.Ignore())
               .ForMember(dest => dest.Proyectos, opt => opt.Ignore());
            Mapper.AssertConfigurationIsValid();
            #endregion

            #region Corte
            //DB --> DTO
            Mapper.CreateMap<C_Corte, C_CorteDTO>();
            Mapper.AssertConfigurationIsValid();

            //DTO --> DB

            Mapper.CreateMap<C_CorteDTO, C_Corte>()
               .ForMember(dest => dest.Asignacion, opt => opt.Ignore());
            Mapper.AssertConfigurationIsValid();
            #endregion

            #region C_Estatus
            //DB --> DTO
            Mapper.CreateMap<C_Estatus, C_EstatusDTO>();
            Mapper.AssertConfigurationIsValid();

            //DTO --> DB

            Mapper.CreateMap<C_EstatusDTO, C_Estatus>()
               .ForMember(dest => dest.Asignacion, opt => opt.Ignore());
            Mapper.AssertConfigurationIsValid();
            #endregion

            #region C_IVA
            //DB --> DTO
            Mapper.CreateMap<C_IVA, C_IVADTO>();
            Mapper.AssertConfigurationIsValid();

            //DTO --> DB

            Mapper.CreateMap<C_IVADTO, C_IVA>()
               .ForMember(dest => dest.Asignacion, opt => opt.Ignore())
               .ForMember(dest => dest.Proyectos, opt => opt.Ignore())
               .ForMember(dest => dest.Facturas, opt => opt.Ignore());
            Mapper.AssertConfigurationIsValid();
            #endregion

            #region C_Moneda
            //DB --> DTO
            Mapper.CreateMap<C_Moneda, C_MonedaDTO>();
            Mapper.AssertConfigurationIsValid();

            //DTO --> DB

            Mapper.CreateMap<C_MonedaDTO, C_Moneda>()
               .ForMember(dest => dest.Asignacion, opt => opt.Ignore())
               .ForMember(dest => dest.Proyectos, opt => opt.Ignore())
               .ForMember(dest => dest.Facturas, opt => opt.Ignore());
            Mapper.AssertConfigurationIsValid();
            #endregion

            #region C_Periodos
            //DB --> DTO
            Mapper.CreateMap<C_Periodos, C_PeriodosDTO>();
            Mapper.AssertConfigurationIsValid();

            //DTO --> DB

            Mapper.CreateMap<C_PeriodosDTO, C_Periodos>()
               .ForMember(dest => dest.Asignacion, opt => opt.Ignore());
            Mapper.AssertConfigurationIsValid();
            #endregion

            #region C_Tipo_Asignacion
            //DB --> DTO
            Mapper.CreateMap<C_Tipo_Asignacion, C_Tipo_AsignacionDTO>();
            Mapper.AssertConfigurationIsValid();

            //DTO --> DB

            Mapper.CreateMap<C_Tipo_AsignacionDTO, C_Tipo_Asignacion>()
               .ForMember(dest => dest.Asignacion, opt => opt.Ignore());
            Mapper.AssertConfigurationIsValid();
            #endregion

            #region C_Tipo_Cambio
            //DB --> DTO
            Mapper.CreateMap<C_Tipo_Cambio, C_Tipo_CambioDTO>();
            Mapper.AssertConfigurationIsValid();

            //DTO --> DB

            Mapper.CreateMap<C_Tipo_CambioDTO, C_Tipo_Cambio>()
               .ForMember(dest => dest.Proyectos, opt => opt.Ignore())
               .ForMember(dest => dest.Facturas, opt => opt.Ignore());
            Mapper.AssertConfigurationIsValid();
            #endregion

            #region C_Metodo_Pago
            //DB --> DTO
            Mapper.CreateMap<C_Metodo_Pago, C_Metodo_PagoDTO>();
            Mapper.AssertConfigurationIsValid();

            //DTO --> DB

            Mapper.CreateMap<C_Metodo_PagoDTO, C_Metodo_Pago>()
               .ForMember(dest => dest.Facturas, opt => opt.Ignore());
            Mapper.AssertConfigurationIsValid();
            #endregion

            #region Clientes
            //DB --> DTO
            Mapper.CreateMap<Clientes, ClientesDTO>()
                .ForMember(dest => dest.Contactos, opt => opt.MapFrom(src => Mapper.Map<List<ContactosDTO>>(src.Contactos)));
            Mapper.AssertConfigurationIsValid();

            //DTO --> DB

            Mapper.CreateMap<ClientesDTO, Clientes>()
               .ForMember(dest => dest.Proyectos, opt => opt.Ignore())
               .ForMember(dest => dest.Empresa, opt => opt.Ignore())
                .ForMember(dest => dest.Asignacion, opt => opt.Ignore())
                .ForMember(dest => dest.Facturas, opt => opt.Ignore())
                .ForMember(dest => dest.Contactos, opt => opt.MapFrom(src => Mapper.Map<List<Contactos>>(src.Contactos)));
            Mapper.AssertConfigurationIsValid();
            #endregion

            #region Proyectos
            //DB --> DTO
            Mapper.CreateMap<Proyectos, ProyectosDTO>()
                  .ForMember(dest => dest.Fecha_Ini_ProyectosSTR, opt => opt.MapFrom(src => src.Fecha_Ini_Proyectos == null ? string.Empty : src.Fecha_Ini_Proyectos.Value.ToString("dd/MM/yyyy")))
                  .ForMember(dest => dest.Fecha_Fin_ProyectosSTR, opt => opt.MapFrom(src => src.Fecha_Fin_Proyectos == null ? string.Empty : src.Fecha_Fin_Proyectos.Value.ToString("dd/MM/yyyy")))
            .ForMember(dest => dest.facturas, opt => opt.MapFrom(src => Mapper.Map<List<FacturasDTO>>(src.Facturas)));
            Mapper.AssertConfigurationIsValid();

            //DTO --> DB

            Mapper.CreateMap<ProyectosDTO, Proyectos>()
               .ForMember(dest => dest.C_IVA, opt => opt.Ignore())
               .ForMember(dest => dest.C_Moneda, opt => opt.Ignore())
               .ForMember(dest => dest.C_Tipo_Cambio, opt => opt.Ignore())
               .ForMember(dest => dest.Empresa, opt => opt.Ignore())
               .ForMember(dest => dest.Clientes, opt => opt.Ignore())
               .ForMember(dest => dest.Facturas, opt => opt.Ignore());
            Mapper.AssertConfigurationIsValid();
            #endregion

            #region Facturas
            //DB --> DTO
            Mapper.CreateMap<Facturas, FacturasDTO>()
                .ForMember(dest => dest.Fecha_Inicio_FacturaSTR, opt => opt.MapFrom(src => src.Fecha_Inicio_Factura == null ? string.Empty : src.Fecha_Inicio_Factura.Value.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.Fecha_fin_FacturaSTR, opt => opt.MapFrom(src => src.Fecha_fin_Factura == null ? string.Empty : src.Fecha_fin_Factura.Value.ToString("dd/MM/yyyy")));
            Mapper.AssertConfigurationIsValid();

            //DTO --> DB

            Mapper.CreateMap<FacturasDTO, Facturas>()
                     .ForMember(dest => dest.Asignacion, opt => opt.Ignore())
                    .ForMember(dest => dest.C_Estado_Factura, opt => opt.Ignore())
                    .ForMember(dest => dest.C_IVA, opt => opt.Ignore())
                    .ForMember(dest => dest.C_Metodo_Pago, opt => opt.Ignore())
                    .ForMember(dest => dest.C_Moneda, opt => opt.Ignore())
                    .ForMember(dest => dest.C_Tipo_Cambio, opt => opt.Ignore())
                    .ForMember(dest => dest.Clientes, opt => opt.Ignore())
                    .ForMember(dest => dest.Empresa, opt => opt.Ignore())
                    .ForMember(dest => dest.Proyectos, opt => opt.Ignore())
                    .ForMember(dest => dest.DocumentosFacturas, opt => opt.Ignore());
            Mapper.AssertConfigurationIsValid();
            #endregion

            #region DocumentosFacturas
            //DB --> DTO
            Mapper.CreateMap<DocumentosFacturas, DocumentosFacturasDTO>();
            Mapper.AssertConfigurationIsValid();

            //DTO --> DB

            Mapper.CreateMap<DocumentosFacturasDTO, DocumentosFacturas>()
               .ForMember(dest => dest.Facturas, opt => opt.Ignore());
            Mapper.AssertConfigurationIsValid();
            #endregion

            #region Contactos
            //DB --> DTO
            Mapper.CreateMap<Contactos, ContactosDTO>();
            Mapper.AssertConfigurationIsValid();

            //DTO --> DB

            Mapper.CreateMap<ContactosDTO, Contactos>()
               .ForMember(dest => dest.Clientes, opt => opt.Ignore());
            Mapper.AssertConfigurationIsValid();
            #endregion

            #region Perfil
            //DB --> DTO
            Mapper.CreateMap<ctPermisos, PermisosDTO>();
            Mapper.AssertConfigurationIsValid();
            //DTO --> DB

            Mapper.CreateMap<PermisosDTO, ctPermisos>()
                .ForMember(dest => dest.EmpleadoPermiso, opt => opt.Ignore());
            Mapper.AssertConfigurationIsValid();
            #endregion

            #region PaginadorAsignacion
            //DB --> DTO
            Mapper.CreateMap<sp_GetAsigPaginacion_Result, AsignacionPaginadorDTO>()
                  .ForMember(dest => dest.Fecha_Inicio, opt => opt.MapFrom(src => src.Fecha_Inicio == null ? string.Empty : src.Fecha_Inicio.Value.ToString("dd/MMMM/yyyy")))
                  .ForMember(dest => dest.Fecha_Fin, opt => opt.MapFrom(src => src.Fecha_Fin == null ? string.Empty : src.Fecha_Fin.Value.ToString("dd/MMMM/yyyy")))
                  .ForMember(dest => dest.Prox_Facturacion, opt => opt.MapFrom(src => src.Prox_Facturacion == null ? string.Empty : src.Prox_Facturacion.Value.ToString("dd/MMMM/yyyy")))
                  .ForMember(dest => dest.Duracion, opt => opt.ResolveUsing(typeof(DuracionAsignacionDiaResolver)));
            Mapper.AssertConfigurationIsValid();


            //DTO --> DB

            Mapper.CreateMap<AsignacionPaginadorDTO, sp_GetAsigPaginacion_Result>();
            Mapper.AssertConfigurationIsValid();
            #endregion

            #region PaginadorProyectos
            //DB --> DTO
            Mapper.CreateMap<sp_GetProyectosPaginacion_Result, ProyectosPaginadorDTO>()
                  .ForMember(dest => dest.Prox_Facturacion, opt => opt.MapFrom(src => src.Prox_Facturacion == null ? string.Empty : src.Prox_Facturacion.Value.ToString("dd/MMMM/yyyy")));
            Mapper.AssertConfigurationIsValid();


            //DTO --> DB

            Mapper.CreateMap<ProyectosPaginadorDTO, sp_GetProyectosPaginacion_Result>();
            Mapper.AssertConfigurationIsValid();
            #endregion

            #region PaginadorEmpleados
            //DB --> DTO
            Mapper.CreateMap<sp_GetEmpleadosPaginacion_Result, EmpleadosPaginadorDTO>()
                  .ForMember(dest => dest.Antiguedad, opt => opt.MapFrom(src => src.Antiguedad == null ? string.Empty : src.Antiguedad.Value.ToString("dd MMMM yyyy")));
            Mapper.AssertConfigurationIsValid();
            //DTO --> DB

            Mapper.CreateMap<EmpleadosPaginadorDTO, sp_GetEmpleadosPaginacion_Result>();
            Mapper.AssertConfigurationIsValid();
            #endregion

            #region PaginadorEmpresas
            //DB --> DTO
            Mapper.CreateMap<sp_GetEmpresaPaginacion_Result, EmpresasPaginadorDTO>()
                  .ForMember(dest => dest.Fecha_Creacion_Empresa, opt => opt.MapFrom(src => src.Fecha_Creacion_Empresa == null ? string.Empty : src.Fecha_Creacion_Empresa.Value.ToString("dd MMMM yyyy")))
                  .ForMember(dest => dest.Estado_Empresa, opt => opt.MapFrom(src => src.Estado_Empresa == true ? "Activo" : "Inactivo"));
            Mapper.AssertConfigurationIsValid();
            //DTO --> DB

            Mapper.CreateMap<EmpresasPaginadorDTO, sp_GetEmpresaPaginacion_Result>();
            Mapper.AssertConfigurationIsValid();
            #endregion

            #region PaginadorClientes
            //DB --> DTO
            Mapper.CreateMap<sp_GetClientesPaginacion_Result, ClientesPaginadorDTO>();
            Mapper.AssertConfigurationIsValid();
            //DTO --> DB

            Mapper.CreateMap<ClientesPaginadorDTO, sp_GetClientesPaginacion_Result>();
            Mapper.AssertConfigurationIsValid();
            #endregion

            #region PaginadorFacturas
            //DB --> DTO
            Mapper.CreateMap<sp_GetFacturasPaginacion_Result, FacturasPaginadorDTO>()
                .ForMember(dest => dest.DiaFacturacion, opt => opt.MapFrom(src => src.DiaFacturacion == null ? string.Empty : src.DiaFacturacion.Value.ToString("dd/MM/yyyy")));
            Mapper.AssertConfigurationIsValid();
            //DTO --> DB

            Mapper.CreateMap<FacturasPaginadorDTO, sp_GetFacturasPaginacion_Result>();
            Mapper.AssertConfigurationIsValid();
            #endregion




            //EJEMPLO:
            //Mapper.CreateMap<Ticket, TicketDto>()
            //    .ForMember(dest => dest.ClienteActivoNavigation, opt => opt.ResolveUsing(src => Mapper.Map<ClienteActivoDto>(src.ClienteActivo1)))
            //    .ForMember(dest => dest.EventosDelTicket, opt => opt.ResolveUsing(src => Mapper.Map<IEnumerable<EventoTicketDto>>(src.EventoTicket)))
            //    .ForMember(dest => dest.UltimoEventoDelTicket, opt => opt.ResolveUsing(typeof(UltimoEventoTicketResolver)))
            //    .ForMember(dest => dest.UltimaFechaContacto, opt => opt.ResolveUsing(typeof(UltimaFechaContactoDeTicketResolver)))
            //    .ForMember(dest => dest.TipoTicketNavigation, opt => opt.ResolveUsing(src => Mapper.Map<TipoTicketDto>(src.TipoTicket1)))
            //    .ForMember(dest => dest.EstatusNavigation, opt => opt.ResolveUsing(src => Mapper.Map<EstatusTicketDto>(src.EstatusTicket)))
            //    .ForMember(dest => dest.AreaNavigation, opt => opt.ResolveUsing(src => Mapper.Map<AreaDto>(src.Area1)))
            //    .ForMember(dest => dest.SucursalNavigation, opt => opt.ResolveUsing(src => Mapper.Map<SucursalesClienteActivoDto>(src.SucursalesClienteActivo)))
            //    .ForMember(dest => dest.GrupoAsignadoNavigation, opt => opt.ResolveUsing(src => Mapper.Map<GrupoDto>(src.Grupo)))
            //    .ForMember(dest => dest.PrimerDocNombre, opt => opt.Ignore())
            //    .ForMember(dest => dest.PrimerDocTipo, opt => opt.Ignore())
            //    .ForMember(dest => dest.PrimerDocVersion, opt => opt.Ignore())
            //    .ForMember(dest => dest.PrimerDocComentarios, opt => opt.Ignore())
            //    .ForMember(dest => dest.UsuarioOGrupoAsignado, opt => opt.Ignore())
            //    .ForMember(dest => dest.EsGrupoAsignado, opt => opt.Ignore());
            //Mapper.AssertConfigurationIsValid();

        }
    }

    public class DuracionAsignacionResolver : ValueResolver<Asignacion, String>
    {
        protected override String ResolveCore(Asignacion source)
        {
            Int32 anios;
            Int32 meses;
            Int32 dias;
            String str = "";

            if (source.Fecha_Fin_Asignacion == null || source.Fecha_Ini_Asignacion == null)
            {
                return "Indefinido";
            }

            var newdt = (DateTime)source.Fecha_Fin_Asignacion;
            var olddt = (DateTime)source.Fecha_Ini_Asignacion;

            anios = (newdt.Year - olddt.Year);
            meses = (newdt.Month - olddt.Month);
            dias = (newdt.Day - olddt.Day);

            if (meses < 0)
            {
                anios -= 1;
                meses += 12;
            }
            if (dias < 0)
            {
                meses -= 1;
                dias += DateTime.DaysInMonth(newdt.Year, newdt.Month);
            }

            if (anios < 0)
            {
                return "Fecha Invalida";
            }

            if (anios > 0)
                str = str + anios.ToString() + " año" + (anios > 1 ? "s " : " ");
            if (meses > 0)
                str = str + meses.ToString() + " mes" + (meses > 1 ? "es " : " ");
            if (dias > 0)
                str = str + dias.ToString() + " dia" + (dias > 1 ? "s " : " ");

            return str;
        }
    }

    public class DuracionAsignacionDiaResolver : ValueResolver<sp_GetAsigPaginacion_Result, String>
    {
        protected override String ResolveCore(sp_GetAsigPaginacion_Result source)
        {
            Int32 anios = 0;
            Int32 meses = 0;
            Int32 dias = 0;
            String str = "";

            if (source.Duracion == null)
            {
                return "Indefinido";
            }

            dias = source.Duracion.Value;

            if (dias > 365)
            {
                anios = dias / 365;
                dias = dias % 365;
            }

            if (dias > 30)
            {
                meses = dias / 30;
                dias = dias % 30;
            }

            if (anios > 0)
                str = str + anios.ToString() + " año" + (anios > 1 ? "s " : " ");
            if (meses > 0)
                str = str + meses.ToString() + " mes" + (meses > 1 ? "es " : " ");
            if (dias > 0)
                str = str + dias.ToString() + " dia" + (dias > 1 ? "s " : " ");

            return str;
        }
    }
}

