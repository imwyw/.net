using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UploadDemo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FormUpload()
        {
            return View();
        }

        /// <summary>
        /// 处理表单上传的附件
        /// myFile为上传的附件，或者使用Request.Files[0]获取上传附件，同样的作用
        /// </summary>
        /// <param name="myFile">myFile与表单中上传控件的name属性对应</param>
        /// <returns></returns>
        public JsonResult GetFormFile(HttpPostedFileBase myFile)
        {
            // 构造上传附件保存的目录，AppDomain.CurrentDomain.BaseDirectory为程序基路径
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UploadFiles");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            // 将文件保存至服务端，文件方式存储
            myFile.SaveAs(Path.Combine(path, myFile.FileName));
            return Json(new { Status = true });
        }

        /// <summary>
        /// 处理ajax上传的文件
        /// </summary>
        /// <param name="myFile"></param>
        /// <returns></returns>
        public JsonResult GetAjaxFile(HttpPostedFileBase myFile, string name)
        {
            // 构造上传附件保存的目录，AppDomain.CurrentDomain.BaseDirectory为程序基路径
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UploadFiles");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            // 将文件保存至服务端，文件方式存储
            myFile.SaveAs(Path.Combine(path, myFile.FileName));
            return Json(new { Status = true });
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public FileResult DownFile(string filePath)
        {
            // 获取文件的contentType
            string mimetype = MimeMapping.GetMimeMapping(filePath);
            return File(filePath, mimetype, Path.GetFileName(filePath));
        }
    }
}