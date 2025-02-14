using System.Web;
using System.Web.Mvc;

namespace web_app_mensajes_ws
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
