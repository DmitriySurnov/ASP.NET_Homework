using Homework.Models;
using Microsoft.AspNetCore.Html;
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

        public static HtmlString GetListName(this IHtmlHelper _, List<string> listName)
        {
            var result = "<div>";
            foreach (var userName in listName)
            {
                result = $"{result}<div>{userName}</div>";
            }
            result = $"{result}</div>";
            return new HtmlString(result);
        }
    }
}
