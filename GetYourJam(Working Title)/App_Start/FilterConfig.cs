using System.Web;
using System.Web.Mvc;

namespace GetYourJam_Working_Title_
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
