using MedicineProject.Models.BLs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicineProject.Controllers
{
    public class CompanyController : Controller
    {
        public ActionResult GetCompany(int? ID)
        {
            ViewBag.companies = BL_Company.GetCompanies(Convert.ToInt32(1));
            if (ID > 0)
            {
                Company cmp = BL_Company.GetCompany(Convert.ToInt32(ID));
                return View(cmp);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Save(Company cmp)
        {
            if (cmp.CompanyID > 0)
            {
                BL_Company.Update(cmp);
            }
            else
            {
                BL_Company.Save(cmp);
            }
            ViewBag.companies = BL_Company.GetCompanies(Convert.ToInt32(1));
            return RedirectToAction("GetCompany");
        }
        [HttpGet]
        public ActionResult Delete(int ID)
        {
            BL_Company.Delete(ID);
            return RedirectToAction("GetCompany");
        }
    }
}