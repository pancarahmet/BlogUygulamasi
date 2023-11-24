using BlogUygulamasi.Data;
using BlogUygulamasi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogUygulamasi.Controllers
{
	public class RegisterController : Controller
	{
		private readonly BlogDbContext _context;
		private readonly IWebHostEnvironment _hostingEnvironment;

		public RegisterController(BlogDbContext context, IWebHostEnvironment hostingEnvironment)
		{
			_context = context;
			_hostingEnvironment = hostingEnvironment;
		}

		// GET: /Register
		public IActionResult Index()
		{
			return View();
		}

		// POST: /Register
		[HttpPost]
		public async Task<IActionResult> Index(User userModel)
		{
			if (ModelState.IsValid)
			{
				// Kullanıcıyı veritabanına kaydetme işlemi
				if (!string.IsNullOrEmpty(userModel.PhotoUrl))
				{
					// Fotoğrafı işleme kodu buraya gelecek
					// Örneğin, wwwroot içinde bir klasöre kaydedebilirsiniz
					var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "CoreBlogTema/web/images");
					var fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(userModel.PhotoUrl);
					var filePath = Path.Combine(uploads, fileName);

					// Eğer dosya zaten mevcut değilse kopyala
					if (!System.IO.File.Exists(filePath))
					{
						System.IO.File.Copy(userModel.PhotoUrl, filePath);
					}

					userModel.PhotoUrl = "/CoreBlogTema/web/images/" + fileName;
				}

				_context.Users.Add(userModel);
				_context.SaveChanges();
				_context.Database.CloseConnection();

				return RedirectToAction("BlogIndex", "BlogIndex");
			}

			return View(userModel);
		}
	}
}
