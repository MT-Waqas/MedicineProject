using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicineProject.Models.BLs
{
    public class ViewModel
    {
        //public int MyProperty { get; set; }
        public Purchase purchase { get; set; }
        public List<Purchase> Purchases { get; set; }
        public Purchase_Order Purchase_Order { get; set; }
        public List<Purchase_Order> Purchase_Orders { get; set;}
        public Invoice Invoice { get; set; }
        public List<Invoice> Invoices { get; set; }

    }
}