using MedicineProject.Models.BLs;
using MedicineProject.Models.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicineProject.Controllers
{
    public class MedicineController : Controller
    {

        public ActionResult Medicine(int? ID)
        {
            ViewBag.Companies = new SelectList(BL_Company.GetCompanies(1, null), "CompanyID", "CompanyName");
            ViewBag.Medicines = BL_Medicine.GetMedicines(1);
            if (ID > 0)
            {
                Medicine med = BL_Medicine.GetMedicine(Convert.ToInt32(ID));
                return View(med);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Medicine(Medicine med)
        {
            
            string path = Server.MapPath("~/Content/Images");
            med.ImageName = Image.ImageUpload(med.UploadFile,path);

            if (ModelState.IsValid)
            {
                if (med.MedicineID > 0)
                {
                    BL_Medicine.Update(med);
                    TempData["bit"] = 2;
                }
                else
                {
                    BL_Medicine.Save(med);
                    TempData["bit"] = 1;
                }
                ModelState.Clear();
            }
            ViewBag.Companies = new SelectList(BL_Company.GetCompanies(1, null), "CompanyID", "CompanyName");
            ViewBag.Medicines = BL_Medicine.GetMedicines(1);
            return View("Medicine");
            //return RedirectToAction("Medicine");
        }
        public ActionResult Delete(int ID)
        {
            BL_Medicine.Delete(Convert.ToInt32(ID));
            TempData["bit"] = 3;
            return RedirectToAction("Medicine");
        }
    }
}