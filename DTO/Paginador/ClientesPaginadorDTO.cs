using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Paginador
{
    public class ClientesPaginadorDTO
    {
        public Nullable<int> Id_Cliente { get; set; }
        public string Nombre_Cliente { get; set; }
        public string Razon_Social_Cliente { get; set; }
        public string RFC_Cliente { get; set; }
        public string Contacto { get; set; }
    }
}
