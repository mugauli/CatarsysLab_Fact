using System.Web;
using System.Web.Mvc;

namespace CatarsysLab_Fact
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
