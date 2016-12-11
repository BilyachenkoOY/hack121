using Hack121.Business.Entities;
using Hack121.Business.Import;
using Hack121.Business.Search;
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
            foreach(var u in list)
                transImport.Import(u.Edrpou);

            InitSearchIndexes(list);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult IndexLucene()
        {
           var list = Site.Current.University.List();
           InitSearchIndexes(list);
           
           return RedirectToAction("Learning");
        }

        
        [HttpGet]
        public ActionResult Learning()
        {
            return View();    
        }


        [HttpPost]
        public ActionResult Learning(string pattern)
        {
            var transactions = LuceneSearch.SearchByCategory(pattern);
            return PartialView("_Learning", transactions);
        }

        [HttpPost]
        public JsonResult AddCategory(string pattern, string categoryName)
        {
            if (pattern.IsEmpty() || categoryName.IsEmpty())
                return Json(null);
            var transactions = LuceneSearch.SearchByCategory(pattern);
            if (!transactions.Any())
                return Json(null);

            var paymentCategory = new PaymentCategory() { Title = categoryName, Keywords = pattern };
            Site.Current.Category.Create(paymentCategory);

            var transManager = Site.Current.Transaction;
            var trans = transManager.GetByIdList(transactions.Select(t => t.Id).ToList());
            trans.AsParallel().ForAll(t => t.TransactionId = paymentCategory.Id);
            transManager.Create(trans);
            LuceneSearch.AddUpdateLuceneIndex(trans);

            return Json(null);
        }

        public void InitSearchIndexes(IList<University> universitites)
        {
            foreach (var u in universitites)
                InitSearchIndexes(u.Edrpou);
        }

        protected void InitSearchIndexes(string erdpou)
        {
            var trans = Site.Current.Transaction.GetPayerTransactions(erdpou);
            LuceneSearch.AddUpdateLuceneIndex(trans);
        }
    }
}
