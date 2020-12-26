using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WebAppVue.Models.Entity.Context
{
    public class Node
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
}
