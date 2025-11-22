using System;
using System.ComponentModel.DataAnnotations;//c'est pour key and required

namespace MealFinder.Models
{
	public class Commentaire
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Contenu { get; set; }
		public DateTime DateCommentaire { get; set; } = DateTime.Now;
	}
}