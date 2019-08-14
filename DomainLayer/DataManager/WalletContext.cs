using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace DomainLayer.DataManager
{
    public class WalletContext : IdentityDbContext<User.User, IdentityRole, string>
    {
        public WalletContext() { }
        public WalletContext(DbContextOptions<WalletContext> options) : base(options) { }

        public DbSet<Post.Post> Posts { get; set; }
        //public DbSet<Post.PostsMetaInfo> PostsMetaInfo { get; set; }
        public DbSet<Post.PostGroup> PostGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
