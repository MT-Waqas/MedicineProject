using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MedicineProject.Models.BLs;
using System.Web.Mvc;

namespace MedicineProject.Controllers
{
    public class ClientRegistrationController : Controller
    {
       
        public ActionResult RegistorClient()
        {
            ViewBag.Get = BL_Client.GetClients();
            return View();
        }
        [HttpPost]
        public ActionResult RegistorClient(Client cli)
        {
            if (cli.ClientID > 0)
            {
                BL_Client.Update(cli);
                TempData["bit"] = 2;
            }
            else
            {

                BL_Client.Save(cli);
                TempData["bit"] = 1;
            
            }
         
            return RedirectToAction("RegistorClient");
        }
        public ActionResult Update(int id)
        {
            ViewBag.Get = BL_Client.GetClients();
            Client cli = BL_Client.GetClient(id);
            return View("RegistorClient",cli);
        }
        public ActionResult Delete(int id)
        {

            BL_Client.Delete(id);
            return RedirectToAction("RegistorClient");
        }
	}
}