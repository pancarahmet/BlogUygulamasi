using BlogUygulamasi.Data;
using BlogUygulamasi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogUygulamasi.Controllers
{
	public class CommentController : Controller
	{
		private readonly BlogDbContext _context;
		private readonly IConfiguration _configuration;
		private readonly ILogger<CommentController> _logger;

		public CommentController(BlogDbContext context, IConfiguration configuration, ILogger<CommentController> logger)
		{
			_context = context;
			_configuration = configuration;
			_logger = logger;
		}

		[HttpPost]
		public IActionResult AddComment(BlogDetailsViewModel model)
		{
			if (User.Identity.IsAuthenticated)
			{
				// Kullanıcı giriş yapmışsa ve yorum içeriği boş değilse devam et
				if (model != null && model.Comments != null)
				{
					var user = _context.Users.SingleOrDefault(u => u.Username == User.Identity.Name);

					if (user != null)
					{
						// Yeni bir Comment oluştur
						var newComment = new Comment
						{
							Content = model.Comments.First().Content, // Örnek olarak sadece ilk yorumu alıyoruz
							UserId = user.UserId,
							PostId = model.Post.PostId
						};

						// Veritabanına yeni yorumu ekle
						_context.Comments.Add(newComment);
						_context.SaveChanges();

						_logger.LogInformation($"User {User.Identity.Name} added a new comment.");

						// Yorum eklendikten sonra BlogDetails sayfasına geri yönlendir
						return RedirectToAction("BlogDetails", "BlogIndex", new { id = model.Post.PostId });
					}
				}
			}

			// Kullanıcı giriş yapmamışsa veya yorum içeriği boşsa hata mesajı döndür
			TempData["ErrorMessage"] = "Error adding comment. Please make sure you are logged in and the comment is not empty.";
			return RedirectToAction("BlogDetails", "BlogIndex", new { id = model.Post.PostId });
		}
	}
}
