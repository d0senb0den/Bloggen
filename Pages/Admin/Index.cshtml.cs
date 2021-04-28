using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Bloggen.Data;
using Bloggen.Models;

namespace Bloggen.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly Bloggen.Data.BloggenContext _context;

        public IndexModel(Bloggen.Data.BloggenContext context)
        {
            _context = context;
        }

        public IList<Blog> Blog { get;set; }

        public async Task OnGetAsync()
        {
            Blog = await _context.Blog.ToListAsync();
        }
    }
}
