using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APS_API.Models
{
    public class Friends
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public Guid FriendId { get; set; }

        public User User { get; set; }

        public User Friend { get; set; }
    }
}
