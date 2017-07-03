using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Gestion
{
    public class ClientesDTO
    {
        public int Id_Cliente { get; set; }
        public Nullable<int> Id_Empresa { get; set; }
        public string Nombre_Cliente { get; set; }
        public string Razon_Social_Cliente { get; set; }
        public string RFC_Cliente { get; set; }
        public string Calle_Cliente { get; set; }
        public string Exterior_Cliente { get; set; }
        public string Interior_Cliente { get; set; }
        public string Colonia_Cliente { get; set; }
        public string DelMun_Cliente { get; set; }
        public Nullable<short> CP_Cliente { get; set; }
        public string Estado_Dom_Cliente { get; set; }
        public string Dias_de_Pago_Cliente { get; set; }
        public Nullable<bool> Estado { get; set; }
        public string Emails { get; set; }
        
        public virtual ICollection<ContactosDTO> Contactos { get; set; }
    }
}
