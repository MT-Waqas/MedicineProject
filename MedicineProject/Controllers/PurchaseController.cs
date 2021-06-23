using MedicineProject.Models.BLs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MedicineProject.Controllers
{
    public class PurchaseController : Controller
    {
        // GET: Purchase
        public ActionResult Purchase(int? ID, ViewModel viewModel)
        {

            LoadingDropDowns();
            if (TempData["cart"] == null)
            {
                TempData["Total"] = 0;
            }
            else
            {
                GetTotal();
            }
            TempData.Keep();
            
            if (TempData["orderdate"]==null)
            {
                return View();
            }
            else
            {
                purchase_Order(viewModel);
                return View(viewModel);  
            }
            
        }
        public void GetTotal()
        {
            List<Purchase> li = new List<Purchase>();
            li = TempData["cart"] as List<Purchase>;
            decimal total = 0;
            for (int i = 0; i < li.Count; i++)
            {
                total += (li[i].PurchasePrice * li[i].Quantity);
            }
            TempData["Total"] = total.ToString("0.0");
        }
        public void purchase_Order(ViewModel viewModel)
        {
            Purchase_Order p_o = new Purchase_Order();
            Invoice I = new Invoice();
            p_o.OrderID= Convert.ToInt32(TempData["orderid"]);
            I.OrderDate = Convert.ToDateTime(TempData["orderdate"]);
            viewModel.Purchase_Order = p_o;
            viewModel.Invoice = I;
            TempData.Keep("orderid");
            TempData.Keep("orderdate");
        }
        [HttpPost]
        public ActionResult Purchase(ViewModel viewModel)
        {
            TempData.Keep("cart");
            LoadingDropDowns();
            if (ModelState.IsValid)
            {
                Company c = BL_Company.GetCompany(viewModel.purchase.CompanyID);
                Medicine m = BL_Medicine.GetMedicine(viewModel.purchase.MedicineID);
                viewModel.purchase.CompanyName = c.CompanyName;
                viewModel.purchase.MedicineName = m.MedicineName;
                if (TempData["cart"] == null)
                {
                    List<Purchase> li = new List<Purchase>();
                    li.Add(viewModel.purchase);
                    TempData["cart"] = li;
                }
                else
                {
                    List<Purchase> li = TempData["cart"] as List<Purchase>;
                    li.Add(viewModel.purchase);
                    TempData["cart"] = li;
                }
                return RedirectToAction("Purchase");
            }
            else
            {
                purchase_Order(viewModel);
                return View(viewModel);
            }
            //return RedirectToAction("Purchase");
        }
        public ActionResult Delete(int ID)
        {
            //BL_Purchase.Delete(ID);
            List<Purchase> li = new List<Purchase>();
            li = TempData["cart"] as List<Purchase>;
            li.RemoveAt(ID);
            TempData["cart"] = li;
            TempData.Keep("cart");
            TempData["bit"] = 3;
            return RedirectToAction("Purchase");
        }
        public ActionResult CreateOrder(ViewModel viewModel)
        {
            int? orderid;
            //DateTime orderdate;
            if (ModelState.IsValid)
            {
                BL_Purchase_Order.Save(viewModel.Purchase_Order);
                viewModel.Purchase_Order = BL_Purchase_Order.GetPurchaseOrder(1);
                orderid = viewModel.Purchase_Order.OrderID;
                //orderdate =
                TempData["orderid"] = orderid;
                TempData["orderdate"] = viewModel.Invoice.OrderDate;
                TempData.Keep("orderid");
                TempData.Keep("orderdate");
                //TempData.Keep();
                return RedirectToAction("Purchase");
            }
            else
            {
                LoadingDropDowns();
                return View("Purchase");
            }


            //return RedirectToAction("Purchase", "Purchase", new { "statementList" = stetementList });
        }
        public ActionResult Purchase_Invoice()
        {
            List<Purchase> purchases = TempData["cart"] as List<Purchase>;
            Invoice invoice = new Invoice();
            invoice.OrderID = Convert.ToInt32(TempData["orderid"]);
            invoice.OrderDate = Convert.ToDateTime(TempData["orderdate"]);
            GetTotal();
            invoice.TotalAmount = Convert.ToDecimal(TempData["Total"]);
            if (ModelState.IsValid)
            {
                foreach (Purchase pr in purchases)
                {
                    BL_Purchase.Save(pr);
                }
            }
            BL_Invoice.Save(invoice);
            Tempclear();
            return RedirectToAction("Purchase");
        }
        public ActionResult Clear()
        {
            Tempclear();
            return RedirectToAction("Purchase");
        }
        public ActionResult All_Purchases(int orderid,int invoiceid,int cid)
        {
            ViewBag.purchase = BL_Purchase.GetPurchases(1,orderid);
            ViewBag.CompaniesPayments = BL_Company_Payment.GetCompanyPayments(1,invoiceid,cid);
            //invoice.InvoiceID =  invoiceid;
            Invoice invoice = BL_Invoice.GetInvoice(invoiceid);
            return View("All_Purchases",invoice);
        }
        public ActionResult Payment()
        {
            
            return View();
        }
        public JsonResult Details(int id)
        {
            ViewBag.purchaseDetails = BL_Purchase.GetPurchases(1, Convert.ToInt32(id));
            var json = JsonConvert.SerializeObject(ViewBag.purchaseDetails);
            return Json(json,JsonRequestBehavior.AllowGet);
        }
        // loading functions.........
        public void LoadingDropDowns()
        {
            ViewBag.Companies = new SelectList(BL_Company.GetCompanies(1,null), "CompanyID", "CompanyName");
            ViewBag.Medicines = new SelectList(BL_Medicine.GetMedicines(1), "MedicineID", "MedicineName");
            ViewBag.invoice = BL_Invoice.GetInvoices(1,null);
        }
        public void Tempclear()
        {
            TempData.Clear();
        }
    }
}