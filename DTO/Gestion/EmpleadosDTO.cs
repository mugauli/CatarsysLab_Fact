using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Gestion
{
    public class EmpleadosDTO
    {
        public int Id_Empleado { get; set; }
        public Nullable<int> Id_Empresa { get; set; }
        public string Nombre_Empleado { get; set; }
        public string Email_Empleado { get; set; }
        public string Puesto_Empleado { get; set; }
        public Nullable<System.DateTime> Fecha_Nacimiento_Empleado { get; set; }
        public Nullable<System.DateTime> Antiguedad_Empleado { get; set; }
        public string Fecha_Nacimiento_EmpleadoSTR { get; set; }
        public string Antiguedad_EmpleadoSTR { get; set; }
        public string Skype_Empleado { get; set; }
        public string Domicilio_Empleado { get; set; }
        public string Telefono_L_Empleado { get; set; }
        public string Telefono_M_Empleado { get; set; }
        public Nullable<int> Id_JefeInmediato_Empleado { get; set; }
        public Nullable<int> IsLogIn { get; set; }
        public string Usuario_Empleado { get; set; }
        public string Password_Empleado { get; set; }
        public Nullable<bool> Estado { get; set; }
        public Nullable<int> Id_Perfil { get; set; }

    }
}
