using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UploadFiles.Models;

namespace UploadFiles.Controllers
{
    public class HomeController : Controller
    {
        string IMAGES_PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\images");

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SaveImages()
        {
            LayUIVOState vo = new LayUIVOState();
            var files = HttpContext.Request.Files;
            if (files.Count == 0)
            {
                vo.code = -1;
            }
            else
            {
                HttpPostedFileBase file = files[0];
                string fileName = file.FileName;
                string fullName = Path.Combine(IMAGES_PATH, fileName);
                file.SaveAs(fullName);
                vo.data = fileName;
            }
            return Json(vo);
        }

        public FileResult GetImage(string imgName)
        {
            string path = Path.Combine(IMAGES_PATH, imgName);
            return new FileStreamResult(new FileStream(path, FileMode.Open), "image/jpeg");
        }
    }
}