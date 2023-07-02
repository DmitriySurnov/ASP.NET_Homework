using Homework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Homework.HtmlHelpers
{
	public static class GamePageHtmlHelpers
	{
		public static string WhoseMove(this IHtmlHelper _, GameDataModel dataModel)
		{
			return dataModel.IsCurrentPlayerMove ?
				"Ваш ход" : "Ход противника";

		}
	}
}
