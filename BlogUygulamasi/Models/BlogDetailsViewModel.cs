using System.Collections.Generic;
using BlogUygulamasi.Models;
namespace BlogUygulamasi.Models
{
	public class BlogDetailsViewModel
	{
		public Post? Post { get; set; }
		public List<Comment>? Comments { get; set; }
	}
}
