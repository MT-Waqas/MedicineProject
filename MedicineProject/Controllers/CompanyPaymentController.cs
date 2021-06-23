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
        public ActionResult CompanyPayment(int? ID, int? InvoiceId,int? cid)
        {
            if ((cid!=null&&InvoiceId!=null)&(cid!=0&&InvoiceId!=0))
            {
                Company company = BL_Company.GetCompany(Convert.ToInt32(cid));
                Session["Credit"] = company.Credit;
                Session["cid"] = cid;
                Session["InvoiceID"] = InvoiceId;   
            }
            loading(Convert.ToInt32(Session["cid"]), Convert.ToInt32(Session["InvoiceID"]));
            if (ID != 0 & ID != null)
            {
                CompanyPayment cmppayment = BL_Company_Payment.GetCompanyPayment(Convert.ToInt32(ID),null);
                return View(cmppayment);
            }
            else if (InvoiceId > 0 || InvoiceId != null)
            {
                BL_Company_Payment.GetRemaining(Convert.ToInt32(InvoiceId));
                List<Invoice> invc = BL_Invoice.GetInvoices(1, Convert.ToInt32(InvoiceId));
                if (invc.Count>0)
                {
                    invc[0].RemainingAmount = BL_Company_Payment.GetRemaining(Convert.ToInt32(InvoiceId));
                    Session["InvoiceDetail"] = invc[0];
                }
                return View();
            }
            else
            {
                return View();
            }

        }
        [HttpPost]
        public ActionResult CompanyPayment(CompanyPayment companyPayment)
        {
            if (ModelState.IsValid)
            {
                if (companyPayment.CompanyPaymentID> 0)
                {
                    BL_Company_Payment.Update(companyPayment);
                    TempData["bit"] = 2;
                }
                else
                {
                    //decimal surplus;

                    //if (companyPayment.PaidAmount >companyPayment.TotalAmount)
                    //{
                    //  surplus= Math.Abs(companyPayment.TotalAmount-companyPayment.PaidAmount);

                    //  BL_Company.UpdateCredit_Debit(companyPayment.CompanyID,Convert.ToDecimal(surplus));
                    //}
                    //else
                    //{
                    //    Remaining = Math.Abs(companyPayment.TotalAmount - companyPayment.PaidAmount);
                    //    company.Credit = Remaining;
                    //    BL_Company.UpdateCredit_Debit(company); 
                    //}
                   
                    companyPayment.InvoiceID = companyPayment.InvoiceID==0 ? companyPayment.InvoiceID = Convert.ToInt32(Session["InvoiceID"]) : companyPayment.InvoiceID;
                 
                    BL_Company_Payment.Save(companyPayment);
                    Company company =BL_Company.GetCompany(companyPayment.CompanyID);
                    Session["Credit"]=company.Credit;
                    TempData["bit"] = 1;
                }
                ModelState.Clear();
                return RedirectToAction("CompanyPayment",new { ID = 0, InvoiceId=Convert.ToInt32(Session["InvoiceID"]),cid=Convert.ToInt32(Session["cid"])});
            }
            else
            {
                loading(Convert.ToInt32(Session["cid"]), Convert.ToInt32(Session["InvoiceID"]));
                return RedirectToAction("CompanyPayment", new { ID = 0, InvoiceId = Convert.ToInt32(Session["InvoiceID"]), cid = Convert.ToInt32(Session["cid"]) });
                //return View();
            }

        }
        public void loading(int cid,int InvoiceID) 
        {
            ViewBag.CompaniesPayments = BL_Company_Payment.GetCompanyPayments(Convert.ToInt32(1),InvoiceID, cid);
            ViewBag.Companies = new SelectList(BL_Company.GetCompanies(1,Convert.ToInt32(cid)), "CompanyID", "CompanyName",cid);
        }
        public ActionResult Delete(int ID)
        {
            BL_Company_Payment.Delete(ID);
            TempData["bit"] = 3;
            return RedirectToAction("CompanyPayment");
        }
    }
}