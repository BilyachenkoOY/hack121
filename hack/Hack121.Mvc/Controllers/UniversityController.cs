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
        public ActionResult Details(string id)
        {
            var university = Site.Current.University.Get(id);

            var viewModel = Mapper.Map<University, UniversityModel>(university);
            viewModel.Transactions = Site.Current.Transaction.GetPayerTransactions(university.Edrpou);
            viewModel.Categories = Site.Current.Category.Dictionary();
            return View(viewModel);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult GetPeymentTransactionInfo(string edrpo)
        {
            var transactions = Site.Current.Transaction.GetPayerTransactions(edrpo);
            return View(transactions);
        }
    }
}
