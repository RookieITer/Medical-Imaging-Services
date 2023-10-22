using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortFolio_A1_Version2._0.Models
{
    public class DoctorRating
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int DoctorId { get; set; }
        public int Score { get; set; }
        public virtual Doctor Doctor { get; set; }

        public DateTime? RatingDate { get; set; }
    }

}