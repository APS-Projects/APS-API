using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APS_API.Models
{
    public class User : IdentityUser
    {
        [NotMapped]
        [Key]
        public string UserId { get; set; }

        public string AvatarUrl { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int DefaultLocation { get; set; }
    }
}