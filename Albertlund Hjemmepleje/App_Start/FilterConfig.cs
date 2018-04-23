using System.Web;
using System.Web.Mvc;

namespace Albertlund_Hjemmepleje
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
