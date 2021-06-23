using MedicineProject.Models.BLs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicineProject.Controllers
{
    public class CustomerPaymentController : Controller
    {
        // GET: CustomerPayment
        public ActionResult CustomerPayment(int? OrderID, int? CustomerID)
        
        {
            if (OrderID == null || CustomerID == null)
            {
                return RedirectToAction("CustomerSales", "CustomerSale");
            }
            else
            {
              decimal TotalAmount = Load(Convert.ToInt32(OrderID), Convert.ToInt32(CustomerID));
                CustomerPayment customer = new CustomerPayment();
                customer.TotalAmount = TotalAmount;
                return View(customer);
            }
        }
        private decimal Load(int OrderID,int CustomerID)
        {
            decimal Amount=0;
            List<CustomerPayment> customerPayments  = BL_Customer_Payment.GetCustomerpayments(Convert.ToInt32(OrderID));
            ViewBag.CustomerPayments = customerPayments;
            ViewBag.Orders = BL_SaleItems.GetItems(Convert.ToInt32(OrderID), Convert.ToInt32(CustomerID));
            Customer c = BL_Customer.GetCustomer(1);
            List<Customer> customer = new List<Customer>();
            customer.Add(c);
            ViewBag.Customers = new SelectList(customer, "CustomerID", "CustomerName", CustomerID);
            ViewBag.PaymentMethods = new SelectList(BL_PayMethods.GetPaymentMethods(), "PaymentMethodID", "PaymentMethodName");
            if (customerPayments.Count>0)
            {
                if (customerPayments.LastOrDefault<CustomerPayment>().RemainingAmount > 0 && customerPayments.Count() > 0)
                {
                    Amount = customerPayments.Last<CustomerPayment>().RemainingAmount;
                }
            }
            else
            {
                Order Totalamount = BL_Order.GetOrderInvoice_(OrderID);
                Amount = Totalamount.TotalAmount;
            }
            ViewBag.OrderID = OrderID;
            return Amount;
        }
        [HttpPost]
        public ActionResult CustomerPayment(CustomerPayment customerPayment)
        {
            if (ModelState.IsValid)
            {
                if (customerPayment.CustomerPaymentID > 0)
                {
                    BL_Customer_Payment.Update(customerPayment);
                }
                else
                {
                    BL_Customer_Payment.Save(customerPayment);
                }
                return RedirectToAction("CustomerPayment", new { OrderID = customerPayment.OrderID, CustomerID = customerPayment.CustomerID });

            }
            else
            {
                Load(Convert.ToInt32(customerPayment.OrderID), Convert.ToInt32(customerPayment.CustomerID));
                return View();
            }
           
        }
    }
}