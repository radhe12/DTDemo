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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile)
        {
            List<DealerTrack> customers = new List<DealerTrack>();
            string filePath = string.Empty;
            List<DealerTrack> values = null;
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Files/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);

                //Read the contents of CSV file.
                //string csvData = (System.IO.File.ReadAllLines(filePath).Skip(1)).ToString();

                ////Execute a loop over the rows.
                //foreach (string row in csvData.Split('\n'))
                //{
                //    if (!string.IsNullOrEmpty(row))
                //    {
                //        customers.Add(new DealerTrack
                //        {
                //            DealNumber = Convert.ToInt32(row.Split(',')[0]),
                //            CustomerName = row.Split(',')[1],
                //            DealershipName = row.Split(',')[2],
                //            Vehicle = row.Split(',')[3],
                //            Price =Convert.ToInt64(row.Split(',')[4]),
                //            Date =DateTime.Parse((row.Split(',')[5])
                //        });

                values = System.IO.File.ReadAllLines(filePath)
                                               .Skip(1)
                                               .Select(v => DealerTrack.fromCSV(v))
                                               .ToList();


            }

            return View(values);
        }
    }
}