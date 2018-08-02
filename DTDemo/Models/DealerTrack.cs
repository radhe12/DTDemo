using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace DTDemo.Models
{
    public class DealerTrack
    {
        public int DealNumber { get; set; }

        public string CustomerName { get; set; }

        public string DealershipName { get; set; }

        public string Vehicle { get; set; }

        public string Price { get; set; }

        public DateTime Date { get; set; }

        public static DealerTrack fromCSV(string csvData)
        {
            CultureInfo ci = new CultureInfo("en-CA");
            
            string[] values = Regex.Split(csvData, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
            DealerTrack dt = new DealerTrack();
            dt.DealNumber = Convert.ToInt32(values[0]);
            dt.CustomerName = values[1];
            dt.DealershipName = values[2].Replace("\"", "");
            dt.Vehicle = values[3];
            dt.Price = String.Format(ci, "{0:#.00}", values[4].Replace("\"",""));
            dt.Date = DateTime.Parse(values[5]);
            return dt;
        }
    }

   
}