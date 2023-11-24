using BlogUygulamasi.Data;
using BlogUygulamasi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogUygulamasi.Controllers
{
    public class BlogIndexController : Controller
    {
		private readonly BlogDbContext _context; // DbContext sınıfınıza göre güncelleyin

		public BlogIndexController(BlogDbContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> BlogIndex()
		{
			var posts = await _context.Posts.Include(p => p.User).ToListAsync();

			return View(posts);
		}
		public IActionResult BlogDetails(int id) 
        {
			// Blog gönderisinin verilerini veritabanından alın
			var post = _context.Posts
	.Include(p => p.Comments)
		.ThenInclude(c => c.User)
	.FirstOrDefault(p => p.PostId == id);


			if (post == null)
			{
				// Gönderi bulunamazsa, hata sayfasına yönlendirin veya istediğiniz şekilde işleyin.
				return View("Error");
			}

			var comments = post.Comments.ToList();

			// Sayfa modelini oluşturun
			var model = new BlogDetailsViewModel
			{
				Post = post,
				Comments = comments
			};

			// Blog gönderisi verilerini görüntülemek için BlogDetails view'ını döndürün
			return View(model);
		}
    }
}
