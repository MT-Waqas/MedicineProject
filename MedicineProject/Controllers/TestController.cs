using MedicineProject.Models.BLs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicineProject.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index(int orderid, int invoiceid ,int cid)
        {
            ViewBag.purchase = BL_Purchase.GetPurchases(1, orderid);
            ViewBag.CompaniesPayments = BL_Company_Payment.GetCompanyPayments(1, invoiceid,cid);
            //invoice.InvoiceID =  invoiceid;
            Invoice invoice = BL_Invoice.GetInvoice(invoiceid);
            return View(invoice);
        }
    }
}