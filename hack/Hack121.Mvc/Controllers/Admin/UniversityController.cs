using Hack121.Business.Entities;
using Hack121.Business.Import;
using Hack121.Mvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hack121.Mvc.Controllers.Admin
{
    public class UniversityController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var list = Site.Current.University.List();
            return View(MapList<University, UniversityModel>(list));
        }

        [HttpGet]
        public ActionResult Import()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("uk-ua");
            var transImport = new TransactionsImport();
            var list = Site.Current.University.List();
            foreach(var u in list.Skip(1))
                transImport.Import(u.Edrpou);
            return RedirectToAction("Index");
        }
    }
}
