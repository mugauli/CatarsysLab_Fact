using DTO.Gestion;
using DTO.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatarsysLab_Fact.Models
{
    public class EmpleadoModel
    {
        public List<EmpleadosDTO> ctEmpleados { get; set; }
        public List<PermisosDTO> ctPermisos { get; set; }
    }
}