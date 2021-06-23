using MedicineProject.Models.BLs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicineProject.Controllers
{
    public class PaymenthMethodController : Controller
    {
        // GET: PaymenthMethod
        public ActionResult PaymentMethod(int? ID)
        {
            ViewBag.PayMethods = BL_PayMethods.GetPaymentMethods();
            if (ID != null|| ID != 0)
            {
                PayMethods payMethods = BL_PayMethods.GetPaymentMethod(Convert.ToInt32(ID));
                return View(payMethods); 
            }
            else
            {
                return View();
            }
            
        }
        [HttpPost]
        public ActionResult PaymentMethod(PayMethods pay)
        {
            if (pay.PaymentMethodID>0)
            {
                BL_PayMethods.Update(pay);
            }
            else
            {
                BL_PayMethods.Save(pay);
            }
            ViewBag.PayMethods = BL_PayMethods.GetPaymentMethods();
            ModelState.Clear();
            return View();
        }
        public ActionResult Delete(int ID)
        {
            BL_PayMethods.Delete(ID);
            return RedirectToAction("PaymentMethod");
        }
    }
}