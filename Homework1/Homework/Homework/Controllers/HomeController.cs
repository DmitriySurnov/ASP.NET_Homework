using Homework.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Homework.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult FinishGame()
		{
			return View();
		}

		[HttpPost]
		public IActionResult StartGame(string firstPlayerName, string secondPlayerName)
		{
			GameDataModel.FirstPlayer = new PlayerDataModel
			{
				Name = firstPlayerName
			};
			GameDataModel.SecondPlayer= new PlayerDataModel
			{
				Name = secondPlayerName
			};
			GameDataModel gameData = new GameDataModel();
			gameData.FillTheField();
			return View("Game", gameData);
		}

		[HttpPost]
		public IActionResult Game(int idPole, string FildsString, bool isX)
		{
			GameDataModel gameData = new GameDataModel();
			gameData.FillTheField(FildsString);
			gameData.IsX = isX;
			gameData.MakeAMove(idPole);
			return View(gameData);
		}

		[HttpPost]
		public IActionResult RestartGame()
		{
			GameDataModel gameData = new GameDataModel();
			gameData.FillTheField();
			return View("Game", gameData);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}