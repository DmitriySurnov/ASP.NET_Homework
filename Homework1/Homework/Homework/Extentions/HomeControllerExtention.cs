using Homework.Controllers;
using Homework.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Extentions
{
	public static class HomeControllerExtention
	{
		public static IActionResult RedirectToAddPlayerPage(this HomeController homeController, LoginDataModel dataModel)
		{
			QueryBuilder queryBuilder = GetQueryBuilder(dataModel);

			UriBuilder uriBuilder = GetUriBuilder(homeController, queryBuilder);

			return homeController.Redirect(uriBuilder.Uri.ToString());
		}

		private static QueryBuilder GetQueryBuilder(LoginDataModel dataModel)
		{
			return new QueryBuilder
			{
				{ nameof(dataModel.PlayerName), dataModel.PlayerName ?? string.Empty }
			};
		}

		private static UriBuilder GetUriBuilder(HomeController homeController, QueryBuilder queryBuilder)
		{
			var currentRequestUrl = homeController.HttpContext.Request.GetEncodedUrl();

			UriBuilder uriBuilder = new UriBuilder(currentRequestUrl);
			uriBuilder.Path = homeController.Url.Action(nameof(homeController.SetLobby));
			uriBuilder.Query = queryBuilder.ToString();

			return uriBuilder;
		}
	}
}
