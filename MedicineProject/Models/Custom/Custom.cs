using MedicineProject.Models.BLs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicineProject.Models.Custom
{
    public class Custom
    {
        public static UInt64 Get_Total(List<SaleItems> p)
        {
             UInt64 cordersTotal = 0;
            foreach (var item in p)
            {
                cordersTotal += (Convert.ToUInt64(item.Quantity)) * (Convert.ToUInt64(item.SalePrice));
            }
            return cordersTotal;
        }
        public static bool AlreadyExist(List<SaleItems> p,int MedicineID)
        {
           bool IsNotExists = true;
            foreach (var item in p)
            {
                if (item.MedicineID==MedicineID)
                {
                    IsNotExists = false;
                }
            }
            return IsNotExists;
        }
        public static SaleItems GetItem(int MedicineID)
        {
            Purchase purchase = BL_Stock.GetMedicine(null, Convert.ToInt32(MedicineID));
            SaleItems items = new SaleItems();
            items.MedicineID = purchase.MedicineID;
            items.MedicineName = purchase.MedicineName;
            items.CompanyID = purchase.CompanyID;
            items.CompanyName = purchase.CompanyName;
            items.Quantity = 1;
            items.AvailableStock = purchase.Quantity;
            items.SalePrice = purchase.SalePrice;
            return items;
        }
    }
}