using Homework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Homework.HtmlHelpers
{
	public static class GamePageHtmlHelpers
	{
		public static string WhoseMove(this IHtmlHelper helpers, GameDataModel dataModel)
		{
			return dataModel.MotionPlayer ?
				"Ваш ход" : "Ход противника";

		}
	}
}
