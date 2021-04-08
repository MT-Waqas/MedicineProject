using MedicineProject.Models.BLs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicineProject.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult GetEmployee(int? ID)
        {
            if (ID > 0)
            {
                BL_Employee.GetEmployee(Convert.ToInt32(ID));
            }
            ViewBag.Employees = BL_Employee.GetEmployees(Convert.ToInt32(1));
            return View();
        }
        [HttpPost]
        public ActionResult Save(Employee emp)
        {
            if (emp.EmployeeID > 0)
            {
                BL_Employee.Update(emp);
            }
            else
            {
                BL_Employee.Save(emp);
            }

            return RedirectToAction("GetEmployee");
        }
        public ActionResult Delete(int ID)
        {
            BL_Employee.Delete(ID);
            return RedirectToAction("GetEmployee");
        }
    }
}