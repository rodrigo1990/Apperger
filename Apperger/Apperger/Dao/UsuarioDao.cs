using System;
using System.Collections.Generic;
using System.Text;
using Apperger.Data;

namespace Apperger.Dao
{
    class UsuarioDao
    {
        public static List<Usuario> lista = new List<Usuario>();

        public UsuarioDao() {

            Usuario u1 = new Usuario("mcd77.1990@gmail.com", "123");
            Usuario u2 = new Usuario("maxi@gmail.com", "proyecto2018");

            lista.Add(u1);
            lista.Add(u2);


        }


        public bool GetUsuario(Usuario usuario)
        {
            Usuario u = lista.Find(x=>x.email.Contains(usuario.email));

            if (u != null) { 

                if (u.password == usuario.password) { 

                    return true;

                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }



        }





    }
}
