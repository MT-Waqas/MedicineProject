using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicineProject.Models.BLs;
using MedicineProject.Models.Custom;

namespace MedicineProject.Controllers
{
    public class StockController : Controller
    {
        // GET: AllMedicineStock
        public ActionResult Stock()
        {
            ViewBag.AllStock = BL_Stock.GetMedicines(1);
            return View();
        }
        public ActionResult Checking_Cart()
        {
            ViewBag.AllStock = BL_Stock.GetMedicines(1);
            var returnField = new { PurchaseStock = ViewBag.AllStock, Cart = Session["orderslist"] };
            return Json(returnField, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CustomerOrders(int? MedicineID)
        {
            if (MedicineID != null)
            {
                List<SaleItems> orders = new List<SaleItems>();
                SaleItems items = Custom.GetItem(Convert.ToInt32(MedicineID));

                if (((List<SaleItems>)Session["orderslist"]) != null)
                {
                    orders = (List<SaleItems>)Session["orderslist"];

                    //-------------------checking that the Cart item already Exists or not.----------------------------------

                    if (Custom.AlreadyExist(orders, Convert.ToInt32(MedicineID)))
                    {
                        orders.Add(items);
                    }
                }
                else
                {
                    orders.Add(items);
                }
                Session["orderslist"] = orders;
                Session["COrdersTotal"] = Custom.Get_Total(orders);
            }
            return Json(Session["orderslist"], JsonRequestBehavior.AllowGet);
        } 
    }
}