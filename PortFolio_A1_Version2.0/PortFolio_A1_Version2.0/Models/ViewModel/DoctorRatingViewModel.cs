using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace PortFolio_A1_Version2._0.Models.ViewModel
{
    public class DoctorRatingViewModel
    {
        public int DoctorId { get; set; }
        public String DoctorName { get; set; }
        public double Score { get; set; }
        public string ErrorMessage { get; set; }
        public List<SelectListItem> Doctors { get; set; }
    }
}