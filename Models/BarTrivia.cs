using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APS_API.Models
{
    public class BarTrivia
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BarId { get; set; }

        [Required]
        public int DayOfWeek { get; set; }

        [Required]
        [Range (0, 2400)]
        public int Hours { get; set; }

        [Required]
        public string Description { get; set; }

        public Bar Bar { get; set; }
    }
}
