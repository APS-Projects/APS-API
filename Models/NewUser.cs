using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace APS_API.Models
{
    public class NewUser
    {
        [Required]
        [NotMapped]
        public string Username { get; set; }

        [Required]
        [NotMapped]
        public string Password { get; set; }

        [Required]
        [NotMapped]
        public string FirstName { get; set; }

        [Required]
        [NotMapped]
        public string LastName { get; set; }
    }
}