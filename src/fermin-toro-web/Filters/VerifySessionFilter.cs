using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FerminToroWeb.Filters
{
    public class VerifySessionFilter
    {
        public void VerifySession(HttpContext context)
        {
            // Verificar si la sesión ha sido borrada o no existe
            if (context.Session == null || !context.Session.Keys.Any())
            {
                context.Session.SetString("SessionClosedMessage", "Acceso cerrado por inactividad");
                // Redirigir al inicio de sesión
                context.Response.Redirect("/");
            }
        }
    }
}
