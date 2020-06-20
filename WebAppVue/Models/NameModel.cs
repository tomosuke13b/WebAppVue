using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WebAppVue.Models.Entity;

namespace WebAppVue.Models
{
    public class NameModel
    {
        public NameModel() {
        }

        public string[] Names() {
            using (var db = new TestContext())
            {
                //db.SaveChanges();
                return db.Names
                    .Where(name => !name.Deleted)
                    .Select(name => name.Text)
                    .ToArray();
            }
        }
    }
}
