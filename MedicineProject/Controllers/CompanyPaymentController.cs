using MedicineProject.Models.BLs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicineProject.Controllers
{
    public class CompanyPaymentController : Controller
    {
        public ActionResult CompanyPayment(int? ID)
        {
            ViewBag.CompaniesPayments = BL_Company_Payment.GetCompanyPayments(Convert.ToInt32(1));
            ViewBag.Companies =new SelectList(BL_Company.GetCompanies(1),"CompanyID","CompanyName"); 
            if (ID > 0 || ID != null)
            {
                CompanyPayment cmppayment = BL_Company_Payment.GetCompanyPayment(Convert.ToInt32(ID));
                return View(cmppayment);
            }
            else
            {
                return View();
            }

        }
        [HttpPost]
        public ActionResult Save(CompanyPayment companyPayment)
        {
            if (companyPayment.CompanyPaymentID > 0)
            {
                BL_Company_Payment.Update(companyPayment);
                TempData["bit"] = 1;
            }
            else
            {
                BL_Company_Payment.Save(companyPayment);
                TempData["bit"] = 2;
            }
            return RedirectToAction("CompanyPayment");

        }
        public ActionResult Delete(int ID)
        {
            BL_Company_Payment.Delete(ID);
            TempData["bit"] = 3;
            return RedirectToAction("CompanyPayment");
        }
    }
}