using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PortFolio_A1_Version2._0.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime StartTime { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime EndTime { get; set; }  
        public string Description { get; set; }  

        // Optional
        // public string Location { get; set; }

        
        public string UserId { get; set; }
        // ForeignKey
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        // Other special cases
        // public int EventTypeId { get; set; }
        // public virtual EventType EventType { get; set; }
    }
}