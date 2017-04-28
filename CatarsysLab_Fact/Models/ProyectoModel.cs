using DTO.Catalogos;
using DTO.Gestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatarsysLab_Fact.Models
{
    public class ProyectoModel
    {
        public List<ClientesDTO> ctClientes { get; set; }
        public List<C_MonedaDTO> ctMoneda { get; set; }
        public List<C_IVADTO> ctIva { get; set; }
        public List<C_Tipo_CambioDTO> ctTipoCambio { get; set; }
        public List<FacturasDTO> Facturas { get; set; }


    }
}