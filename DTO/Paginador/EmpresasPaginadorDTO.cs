using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Paginador
{
    public class EmpresasPaginadorDTO
    {
        public Nullable<int> Id_Empresa { get; set; }
        public string Nombre_Empresa { get; set; }
        public string Razon_Social_Empresa { get; set; }
        public string Fecha_Creacion_Empresa { get; set; }
        public string Estado_Empresa { get; set; }
    }
}
