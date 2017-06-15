using ModeloDatos.Gestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace CatarsysLab_Fact.Utilerias
{
    public class CustomRoleProvider : RoleProvider
    {
        EmpreadosData _Empleados = new EmpreadosData();
        

        public CustomRoleProvider()
        {
        
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #region MetodosImplementados

        public override string[] GetRolesForUser(string idUsuario)
        {
            try
            {
              var response = _Empleados.ObtenerPermisos(short.Parse(idUsuario));

                if (response.Code < 0)
                    throw new Exception(response.Message);

                if (response.Result.Count == 0)
                {
                    return new string[] { };
                }

                string[] roleNames = new string[response.Result.Count];

                var i = 0;
                foreach (var rol in response.Result)
                {
                    roleNames[i] = rol.Id_Permiso.ToString();
                    i++;
                }

                return roleNames;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public override bool IsUserInRole(string idUsuario, string roleName)
        {
            try
            {
                var responseUsr = _Empleados.ObtenerPermisos(short.Parse(idUsuario));
                if (responseUsr.Code < 0)
                    throw new Exception(responseUsr.Message);

                var responseRol = _Empleados.ObtenRolActivo(int.Parse(roleName));
                if (responseRol.Code < 0)
                    throw new Exception(responseRol.Message);

                if (responseUsr.Result == null)
                    return false;
                if (responseRol.Result == null)
                    return false;

                
                var responseRoles = _Empleados.ObtenerPermisos(short.Parse(idUsuario));
                if (responseRoles.Code < 0)
                    throw new Exception(responseRoles.Message);

                if (responseRoles.Result.Count == 0)
                    return false;

                return responseRoles.Result.Any(r => r.Id_Permiso.ToString() == roleName);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        #endregion


        #region MetodosNoImplementados

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}