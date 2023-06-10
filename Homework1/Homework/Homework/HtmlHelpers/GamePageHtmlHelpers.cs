using Homework.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Homework.HtmlHelpers
{
	public static class GamePageHtmlHelpers
	{
		public static string ShowPlayrName(this IHtmlHelper helpers, GameDataModel dataModel)
		{
			return dataModel.IsX ?
				dataModel.FirstPlayer.Name :
				dataModel.SecondPlayer.Name;
		}

		public static HtmlString ShowCell(this IHtmlHelper helpers, GameDataModel dataModel, int currenId)
		{
			var result = dataModel.Field[currenId] == "" ?
				$"<button class='Cells' name='idPole' value='{currenId}' formaction='/Home/Game'></button>"
				: dataModel.Field[currenId];
			return new HtmlString(result);
		}

		public static HtmlString DataModel(this IHtmlHelper helpers, GameDataModel dataModel)
		{
			var result = @$"
				<input type='hidden' name='IsX' value='{dataModel.IsX}' />
				<input type='hidden' name='FieldString' value='{dataModel.FieldString}' />
				<input type='hidden' name='firstPlayerName' value='{dataModel.FirstPlayer.Name}' />
				<input type='hidden' name='secondPlayerName' value='{dataModel.SecondPlayer.Name}' />";
			return new HtmlString(result);
		}
	}
}
