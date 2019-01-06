using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APS_API.Models
{
    public class UserFavoriteBar
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int BarId { get; set; }

        public User User { get; set; }

        public Bar Bar { get; set; }
    }
}
