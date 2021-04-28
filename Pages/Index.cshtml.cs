using Bloggen.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bloggen.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Data.BloggenContext _context;
        public IndexModel(Data.BloggenContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Header { get; set; }

        [BindProperty]
        public string Text { get; set; }

        [BindProperty]
        public IFormFile UploadedImage { get; set; }


        [BindProperty]
        public IList<Blog> Blogs { get; set; }

        [BindProperty(SupportsGet = true)]
        public int BlogDeleteID { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Blogs = await _context.Blog.ToListAsync();

            if (BlogDeleteID != 0)
            {
                Blog blog = await _context.Blog.FindAsync(BlogDeleteID);

                if (System.IO.File.Exists("./wwwroot/img/" + blog.Image))
                {
                    System.IO.File.Delete("./wwwroot/img/" + blog.Image);
                }
                    _context.Blog.Remove(blog);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (UploadedImage != null)
            {
                var file = "./wwwroot/img/" + UploadedImage.FileName;
                using(var fileStream = new FileStream(file, FileMode.Create))
                {
                    await UploadedImage.CopyToAsync(fileStream);
                }
            }

            Blog blog = new Blog();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            blog.Date = DateTime.Now;
            blog.Header = Header;
            blog.Text = Text;
            blog.Image = UploadedImage != null ? UploadedImage.FileName : "";
            blog.UserId = userId;

            _context.Blog.Add(blog);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}
