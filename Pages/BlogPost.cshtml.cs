using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bloggen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggen.Pages
{
    public class BlogPostModel : PageModel
    {
        private readonly Data.BloggenContext _context;
        public BlogPostModel(Data.BloggenContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Blog Blog { get; set; }

        [BindProperty (SupportsGet = true)]
        public int BlogId { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {

            if (BlogId != 0)
            {
                Blog = await _context.Blog.FindAsync(BlogId);
            }

            return Page();
        }
    }
}
