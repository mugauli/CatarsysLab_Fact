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
        public string Contactos_Cliente { get; set; }
        public string Domicilio_Cliente { get; set; }
        public string Dias_de_Pago_Cliente { get; set; }
        public string Envio_Fact_Empresa { get; set; }
        public Nullable<bool> Estado { get; set; }
    }
}
