using AutoMapper;
using Hack121.Business.Entities;
using Hack121.Mvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hack121.Mvc.Controllers
{
    public class UniversityController : Controller
    {
        public ActionResult Details()
        {          
            return View();
        }
    }
}
