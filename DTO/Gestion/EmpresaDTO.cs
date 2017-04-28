using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Gestion
{
    public class EmpresaDTO
    {
        public int Id_Empresa { get; set; }
        public string Nombre_Empresa { get; set; }
        public string Razon_Social_Empresa { get; set; }
        public string RFC_Empresa { get; set; }
        public string Calle_Empresa { get; set; }
        public string No_Ext_Empresa { get; set; }
        public string No_Int_Empresa { get; set; }
        public string Colonia_Empresa { get; set; }
        public Nullable<short> CP_Empresa { get; set; }
        public string Del_Mun_Empresa { get; set; }
        public string Email_Empresa { get; set; }
        public Nullable<System.DateTime> Fecha_Creacion_Empresa { get; set; }
        public Nullable<bool> Estado_Empresa { get; set; }
    }
}
