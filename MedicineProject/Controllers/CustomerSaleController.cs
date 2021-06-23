using MedicineProject.Models.BLs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicineProject.Controllers
{
    public class CustomerSaleController : Controller
    {
        // GET: CustomerSaleInvoice here invoice will be shown means sales will be shown

        public ActionResult CustomerSales()
        
        {
            ViewBag.Sales = BL_Order.GetOrdersInvoice_s(null);
            return View();
        }
        [HttpPost]
        public ActionResult CustomerSale(Order order)
        {
            BL_Order.Save(order);
            Order ord=BL_Order.GetOrderID_Only(order.CustomerID,order.TotalAmount);
            List<SaleItems> orders = (List<SaleItems>)Session["orderslist"];
            foreach (var item in orders)
            {
                item.OrderID = ord.OrderID;
                BL_SaleItems.Save(item);
            }
            return RedirectToAction("CustomerPayment", "CustomerPayment", new { OrderID = ord.OrderID,CustomerID=order.CustomerID,TotalAmount= ord.TotalAmount});
        }
    }
}