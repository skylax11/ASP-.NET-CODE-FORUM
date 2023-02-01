using letsDoThis.EF;
using letsDoThis.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace letsDoThis.EF
{
    public class MyDB : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }

        public MyDB()
        {
            Database.SetInitializer(new MyInitilaizer());
        }
    }
}