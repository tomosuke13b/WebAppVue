using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAppVue.Models.Entity
{
    public class TestContext : DbContext
    {
        public DbSet<Name> Names { get; set; }
        public DbSet<Description> Descriptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=webappvue_mssql;
                  Database=TestDB;
                  User ID=sa;Password=1234qwerAS;"
                );
        }

        public class Name
        {
            public int Id { get; set; }
            public bool Deleted { get; set; }
            public int Sort { get; set; }
            public string Text { get; set; }
            public int Date { get; set; }
            public int Time { get; set; }
            public DateTime Created_at { get; set; }
            public DateTime Updated_at { get; set; }
            public DateTime Delete_at { get; set; }
            public Description Description { get; set; }
        }

        public class Description
        {
            public int NameId { get; set; }
            public bool Deleted { get; set; }
            public string Text { get; set; }
            public DateTime Created_at { get; set; }
            public DateTime Updated_at { get; set; }
            public DateTime Delete_at { get; set; }

        }
    }
}
