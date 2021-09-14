using System.Web;
using System.Web.Mvc;

namespace Domican_Stephen_S00209029_Web_Application_Project
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
