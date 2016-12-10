using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hack121.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Site.Provider.University.Get("123");
            return View();
        }
    }
}
