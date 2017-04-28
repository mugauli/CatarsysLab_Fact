using DTO.Catalogos;
using DTO.Gestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatarsysLab_Fact.Models
{
    public class AsignacionModel
    {
        public List<ClientesDTO> ctClientes { get; set; }
        public List<EmpleadosDTO> ctEmpleados { get; set; }
        public List<C_Tipo_AsignacionDTO> ctTipoAsignacion { get; set; }
        public List<C_CorteDTO> ctCorte { get; set; }
        public List<C_PeriodosDTO> ctPeriodo { get; set; }
        public List<C_MonedaDTO> ctMoneda { get; set; }
        public List<C_IVADTO> ctIva { get; set; }
    }
}