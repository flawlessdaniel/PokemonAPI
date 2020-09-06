using DanielAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DanielAPI.Logic
{
    public class AuthLogic
    {
        public AuthLogic() { }

        public A001Usuario ValidaUsuario(A001Usuario user)
        {
            try
            {
                A001Usuario objresult = null;

                if (user != null && user.Usuario.ToUpper().Equals("ADMIN") && user.Password.Equals("ADMIN"))
                {
                    objresult = new A001Usuario()
                    {
                        Email = "admin@gmail.com",
                        Id = 1,
                        Usuario = "ADMIN",
                        Password = "ADMIN"
                    };
                }

                return objresult;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool UsuarioValido(A001Auth auth)
        {
            try
            {
                bool flagresult = true;

                //AQUI SE PUEDE PONER ALGUNA VALIDACION DE ACUERDO A LOGICA DE AUTHENTICACION
                //PARA RENOVAR TOKENS.
                //POR EJEMPLO USAR LA COMBINACION DEL CLAIM GENERADO EN EL PRIMER TOKEN

                return flagresult;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
