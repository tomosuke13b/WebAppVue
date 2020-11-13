using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using WebAppVue.Models.Entity;
using WebAppVue.Models.Json;

namespace WebAppVue.Models
{
    public class BaseModel
    {
        protected readonly TestContext _context;

        public BaseModel(TestContext context = null)
        {
            this._context = context;
        }
    }
}
