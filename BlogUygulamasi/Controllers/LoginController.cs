using BlogUygulamasi.Data;
using BlogUygulamasi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogUygulamasi.Controllers
{
	public class LoginController : Controller
	{
		private readonly BlogDbContext _context;

		public LoginController(BlogDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(string username, string password)
		{
			if (IsValidUser(username, password))
			{
				// Kullanıcı kimlik doğrulama işlemi başarılı ise, kimliğini saklayarak oturum açın
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, username),
					// Diğer kimlik bilgilerini ekleyebilirsiniz
				};

				var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var authProperties = new AuthenticationProperties
				{
					// İstediğiniz özellikleri ekleyebilirsiniz
				};

				HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

				// Kullanıcıyı BlogIndex sayfasına yönlendirin
				return RedirectToAction("BlogIndex", "BlogIndex");
			}
			else
			{
				// Kimlik doğrulama başarısızsa hata mesajını görüntüleyin veya istediğiniz işlemi gerçekleştirin
				ViewBag.WelcomeMessage = "Authentication failed.";
				return View("Login");
			}
		}

		[HttpGet]
		public IActionResult Logout()
		{
			// Kullanıcı oturumu kapattıysa, oturum değişkenini temizleyin.
			HttpContext.Response.Cookies.Delete("Username");
			return RedirectToAction("BlogIndex", "BlogIndex");
		}

		private bool IsValidUser(string username, string password)
		{
			// Kullanıcı doğrulama mantığını burada uygulayın.
			// Örneğin, kullanıcı adı ve şifre veritabanında doğrulamayı kontrol edebilirsiniz.
			// Eğer kullanıcı doğrulanırsa true, aksi takdirde false döndürün.
			var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
			return user != null;
		}

	}
}
