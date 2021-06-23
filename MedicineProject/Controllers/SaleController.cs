using MedicineProject.Models.BLs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicineProject.Controllers
{
    public class SaleController : Controller
    {
        // GET: Sale
        public ActionResult Sale(int? id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Sale()
        {
            return View();
        }
        public JsonResult CreateOrder(Order order)
        {
            BL_Order.Save(order);
            return Json("true", JsonRequestBehavior.AllowGet);
        }
    }
}