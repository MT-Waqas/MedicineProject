using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicineProject.Models.BLs;

namespace MedicineProject.Controllers
{
    public class CustomerController : Controller
    {
       
        public ActionResult GetCutomer()
        {
            int id = 1;
            ViewBag.Get = BL_Customer.GetCustomers(id);
            return View();
        }

        [HttpPost]
        public ActionResult GetCutomer(Customer cu)
        {
            if (cu.CustomerID > 0)
            {
                BL_Customer.Update(cu);
                TempData["bit"] = 2;
            }
            else
            {
                BL_Customer.Save(cu);
                TempData["bit"] = 1;
            }
            return RedirectToAction("GetCutomer");
        }

        public ActionResult Update(int id)
        {
            int idd = 1;
            ViewBag.Get = BL_Customer.GetCustomers(idd);

            Customer c = BL_Customer.GetCustomer(id);
            return View("GetCutomer",c);
        }
        public ActionResult Delete(int id)
        {
            BL_Customer.Delete(id);
            TempData["bit"] = 3;
            return RedirectToAction("GetCutomer");
        }
	}
}