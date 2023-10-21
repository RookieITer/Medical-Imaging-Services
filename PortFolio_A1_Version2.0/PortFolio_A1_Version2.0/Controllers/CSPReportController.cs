using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PortFolio_A1_Version2._0.Controllers
{
    public class CSPReportController : Controller
    {
        // GET: CSPReport
        [HttpPost]
        [Route("csp-violation-report-endpoint")]
        public ActionResult LogReport()
        {
            var reportData = Request.InputStream;
            string jsonString;
            using (var reader = new StreamReader(reportData))
            {
                jsonString = reader.ReadToEnd();
            }

            // Log the report or process as required.
            System.Diagnostics.Debug.WriteLine(jsonString);

            // Optionally: Save the report to a database, send an alert, etc.

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}