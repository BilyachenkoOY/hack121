using Hack121.Business.Import;
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
        public async Task<ActionResult> Index()
        {
            var transImport = new TransactionsImport();
            transImport.Import("05407870");
            return View();
        }
    }
}
