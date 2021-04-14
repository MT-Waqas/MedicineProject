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
        public ActionResult Employee(int? ID)
        {   
            ViewBag.Employees = BL_Employee.GetEmployees(Convert.ToInt32(1));
            if (ID > 0)
            {
                Employee emp = BL_Employee.GetEmployee(Convert.ToInt32(ID));
                return View(emp);
            }
            else
            {
                return View();
            }
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
            return RedirectToAction("Employee");
        }
        public ActionResult Delete(int ID)
        {
            BL_Employee.Delete(ID);
            return RedirectToAction("Employee");
        }
    }
}