using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Gestion
{
    public class ContactosDTO
    {
        public int Id_Contacto { get; set; }
        public int Id_Cliente { get; set; }
        public string Nombre_Contacto { get; set; }
        public string Puesto_Contacto { get; set; }
        public string Email_Contacto { get; set; }
        public string Telefono_Contacto { get; set; }
        public string Movil__Contacto { get; set; }
        public string Skype_Contacto { get; set; }
        public Nullable<bool> EnviaFactura_Contacto { get; set; }
        public string Comentario_Contacto { get; set; }
        public bool Estado { get; set; }
    }
}
