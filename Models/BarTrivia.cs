using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APS_API.Models
{
    public class BarTrivia
    {
        public int Id { get; set; }

        public int BarId { get; set; }

        public int DayOfWeek { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        [Range (0, 2400)]
        public int Hours { get; set; }

        public string Description { get; set; }

        public Bar Bar { get; set; }
    }
}
