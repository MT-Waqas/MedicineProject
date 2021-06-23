using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicineProject.Models.Custom
{
    public class Actions
    {
        public static int Insert { get { return 1; } set { } }
        public static int Update { get { return 2; } set { } }
        public static int Delete { get { return 3; } set { } }
        public static int Select { get { return 4; } set { } }
        public static int Update_Credit { get { return 5; } set { } }
        public static int  Select_Remaining { get { return 6;} set { } }



    }
}