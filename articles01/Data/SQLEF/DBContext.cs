using articles01.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace articles01.Data.SQLEF
{
    public class DBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=.\SQLEXPRESS;Database=ArticlesProjectDB-53bc9b9d-9d6a-45d4-8429-2a2761773502;Trusted_Connection=True;MultipleActiveResultSets=true"
            );
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<AuthorPost> AuthorPosts { get; set; }
    }
}
