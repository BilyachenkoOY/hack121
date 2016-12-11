using Hack121.Business.Entities;
using Hack121.Business.Import;
using Hack121.Mvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;

namespace Hack121.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var universities = Site.Current.University.List().ToList();
            var model = Mapper.Map<List<University>, List<UniversityModel>>(universities);
            return View(model);
        }
    }
}
