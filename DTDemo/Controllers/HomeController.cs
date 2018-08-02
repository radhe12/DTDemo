using DTDemo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new List<DealerTrack>());
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile)
        {
            List<DealerTrack> customers = new List<DealerTrack>();
            string filePath = string.Empty;
            List<DealerTrack> values = null;
            if (postedFile != null)
            {
                //Save file to local directory
                string path = Server.MapPath("~/Files/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                try
                {
                    filePath = path + Path.GetFileName(postedFile.FileName);
                    string extension = Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(filePath);

                    // Read CSV file lines by skiping header line nad convert string to object list
                    values = System.IO.File.ReadAllLines(filePath)
                                                   .Skip(1)
                                                   .Select(v => DealerTrack.fromCSV(v))
                                                   .ToList();

                }
                catch (Exception ex)
                {
                    ViewData["Message"] = "Please try again";
                }
            }
            return View(values);
        }
    }
}