using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using apcrshr_site.Filters;
using Site.Core.Service.Contract;
using Site.Core.Service.Implementation;

namespace apcrshr_site.Areas.Administrator.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionHistoryService _transaction;

        public TransactionController()
        {
            ViewBag.CurrentNode = "Transaction";
            _transaction = new TransactionHistoryService();
        }

        //
        // GET: /Administrator/Transaction/

        [SessionFilter]
        [AuthorizationFilter]
        public ActionResult Index()
        {
            var trans = _transaction.GetAlls();
            return View(trans.Items);
        }

    }
}
