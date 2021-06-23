using MedicineProject.Models.BLs;
using MedicineProject.Models.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicineProject.Controllers
{
    public class CustomerOrdersController : Controller
    {
        // GET: CustomerOrders
        public ActionResult CustomerOrders()
        {
            ViewBag.Employee = new SelectList(BL_Employee.GetEmployees(1), "EmployeeID", "EmployeeName"); ;
            ViewBag.Customers = new SelectList(BL_Customer.GetCustomers(1), "CustomerID", "CustomerName");
            //List<Purchase> orders = (List<Purchase>)Session["orderslist"];
            //Session["COrdersTotal"] = Custom.Get_Total(orders);
            if (((List<SaleItems>)Session["orderslist"])==null || ((List<SaleItems>)Session["orderslist"]).Count==0)
            {
                return RedirectToAction("Stock", "Stock");
            }
            else
            {
                return View();
            }
            
        }
        [HttpPost]
        public ActionResult Increase_Decrease_Quantity(int Quantity, int MedicineID)
        {
            
            List<SaleItems> orders = (List<SaleItems>)Session["orderslist"];
            foreach (var item in orders)
            {
                if (item.MedicineID==MedicineID)
                {
                    // update of quantity
                    if (Quantity<=item.AvailableStock)
                    {
                        item.Quantity = Quantity;
                    }
                }         
            }
            Session["COrdersTotal"] = Custom.Get_Total(orders);
            Session["orderslist"] = orders;
            return Json(Session["orderslist"],JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int? ID)
        {
            //BL_Purchase.Delete(ID);
            if (ID!=null)
            {
                List<SaleItems> li = new List<SaleItems>();
                li = Session["orderslist"] as List<SaleItems>;
                li.RemoveAt(Convert.ToInt32(ID));
                Session["COrdersTotal"] = Custom.Get_Total(li);
                Session["orderslist"] = li;
                TempData["bit"] = 3;
                if (((List<SaleItems>)Session["orderslist"]) == null || ((List<SaleItems>)Session["orderslist"]).Count == 0)
                {
                    return Json(new {list = Session["orderslist"], redirecturl= "/Stock/Stock" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { list = Session["orderslist"] }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { list = Session["orderslist"] }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult refresh()
        {
            return PartialView("Details");
        }
        public ActionResult Total()
        {  
            return Json(new { Total= Session["COrdersTotal"],List= Session["orderslist"] }, JsonRequestBehavior.AllowGet);
        }
    }
}