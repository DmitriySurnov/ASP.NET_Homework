using Homework.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Homework.HtmlHelpers
{
	public static class GamePageHtmlHelpers
	{
		public static string WhoseMove(this IHtmlHelper helpers, PlayerDataModel dataModel)
		{
			return StaticGameDataModelcs.GameDataModel.MotionPlayer.Id == dataModel.Id ?
				"Ваш ход" : "Ход противника";

		}
	}
}
