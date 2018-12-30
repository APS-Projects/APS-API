using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace APS_API.Models
{
    public class ExisitingUser
    {
        [Required]
        [NotMapped]
        public string Username { get; set; }

        [Required]
        [NotMapped]
        public string Password { get; set; }
    }
}