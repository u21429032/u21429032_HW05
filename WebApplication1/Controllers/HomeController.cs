using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase files,string value)
        {
            
            if (files != null && files.ContentLength > 0)
            {
                
                    var file = Path.GetFileName(files.FileName);
                    var path = Path.Combine(Server.MapPath("~/Media/Images/"), file);
                    files.SaveAs(path);
                    var paths = Path.Combine(Server.MapPath("~/Media/Documents/"), file);
                    files.SaveAs(paths);
                    var pat = Path.Combine(Server.MapPath("~/Media/Videos/"), file);
                    files.SaveAs(pat);

                
            }

            return RedirectToAction("Index");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}