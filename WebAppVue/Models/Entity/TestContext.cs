using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAppVue.Models.Entity
{
    public class TestContext : DbContext
    {
        public DbSet<Name> Names { get; set; }
        public DbSet<Description> Descriptions { get; set; }
        public DbSet<Image> Images { get; set; }

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
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public Int64 Id { get; set; }
            [DefaultValue(false)]
            public bool Deleted { get; set; }
            public int Sort { get; set; }
            public string Text { get; set; }
            public string ImageIds { get; set; }
            public int Date { get; set; }
            public int Time { get; set; }
            [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
            public DateTime Created_at { get; set; }
            [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
            public DateTime Updated_at { get; set; }
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public DateTime? Delete_at { get; set; }
        }

        public class Description
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public Int64 NamesId { get; set; }
            [DefaultValue(false)]
            public bool Deleted { get; set; }
            public string Text { get; set; }
            [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
            public DateTime Created_at { get; set; }
            [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
            public DateTime Updated_at { get; set; }
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public DateTime? Delete_at { get; set; }

        }

        public class Image
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public Int64 Id { get; set; }
            [DefaultValue(false)]
            public bool Deleted { get; set; }
            public byte[] Data { get; set; }
            public string ContentType { get; set; }
            [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
            public DateTime Created_at { get; set; }
            [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
            public DateTime Updated_at { get; set; }
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public DateTime? Delete_at { get; set; }
        }
    }
}
