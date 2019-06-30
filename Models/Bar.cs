using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APS_API.Models
{
    public class Bar
{
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int ZipCode { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public DateTime OpenHour { get; set; }

        public DateTime CloseHour { get; set; }
}
}
