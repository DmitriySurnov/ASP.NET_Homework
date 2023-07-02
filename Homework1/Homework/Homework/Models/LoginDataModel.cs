using System.ComponentModel.DataAnnotations;

namespace Homework.Models
{
	public class LoginDataModel
	{
		[Required(ErrorMessage = "Введите имя игрока")]
		public string? PlayerName { get; set; }
	}
}
