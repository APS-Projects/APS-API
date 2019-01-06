using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using APS_API.Models;

namespace APS_API.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Bar> Bar { get; set; }
        public DbSet<BarHappyHour> BarHappyHour { get; set; }
        public DbSet<BarTrivia> BarTrivia { get; set; }
        public DbSet<Friends> Friends { get; set; }
        public DbSet<UserFavoriteBar> UserFavoriteBar { get; set; }
    }

}