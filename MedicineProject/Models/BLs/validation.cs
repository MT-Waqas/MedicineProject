using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MedicineProject.Models.BLs
{
    public class validation
    {
        public static bool IsNullOrEmpty(ViewModel viewModel)
        {   
            if (viewModel.Invoice==null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}