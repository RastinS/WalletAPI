using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DomainLayer.DataManager
{
    public class WalletContext : IdentityDbContext<User.User, IdentityRole, string>
    {
        public WalletContext() { }
        public WalletContext(DbContextOptions<WalletContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
