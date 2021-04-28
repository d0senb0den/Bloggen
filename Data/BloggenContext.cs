using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bloggen.Models;

namespace Bloggen.Data
{
    public class BloggenContext : DbContext
    {
        public BloggenContext (DbContextOptions<BloggenContext> options)
            : base(options)
        {
        }

        public DbSet<Bloggen.Models.Blog> Blog { get; set; }
    }
}
