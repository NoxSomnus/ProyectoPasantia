using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException( string userName) : base($"No se pudo encontrar el Usuario {userName}")
        {
            UserName =userName; 
        }

        public string UserName { get;}
    }
}
