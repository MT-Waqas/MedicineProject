using MedicineProject.Models.BLs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicineProject.Controllers
{
    public class PurchaseController : Controller
    {
        // GET: Purchase
        public ActionResult Purchase(int? ID)
        {
            ViewBag.Companies = new SelectList(BL_Company.GetCompanies(1), "CompanyID", "CompanyName");
            ViewBag.Medicines = new SelectList(BL_Medicine.GetMedicines(1), "MedicineID", "MedicineName");
            ViewBag.Purchases = BL_Purchase.GetPurchases(1);
            if (ID>0||ID!=null)
            {
                Purchase purchase= BL_Purchase.GetPurchase(Convert.ToInt32(ID));
                return View(purchase);
            }
            else
            {
                return View();
            }
        }
        public ActionResult Save(Purchase purchase)
        {
            if (purchase.PurchaseID > 0)
            {
                BL_Purchase.Update(purchase);
                TempData["bit"] = 2;
            }
            else
            {
                BL_Purchase.Save(purchase);
                TempData["bit"] = 1;
            }
            return RedirectToAction("Purchase");
        }
        public ActionResult Delete(int ID)
        {
            BL_Purchase.Delete(ID);
            TempData["bit"] = 3;
            return RedirectToAction("Purchase");
        }
    }
}