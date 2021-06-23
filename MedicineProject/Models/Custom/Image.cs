using MedicineProject.Models.BLs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicineProject.Models.Custom
{
    public class Image
    {
        public static string ImageUpload(HttpPostedFileBase UploadFile,string path)
        { 
            //string path = HttpContext.Server.MapPath("~/Images");
            string filename = Path.GetFileName(UploadFile.FileName);
            string fullpath = Path.Combine(path, filename);
            UploadFile.SaveAs(fullpath);
            return filename;
        }
    }
}