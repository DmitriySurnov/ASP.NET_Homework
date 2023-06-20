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

		[HttpGet]
		public IActionResult FinishGame()
		{
			return View();
		}

		[HttpPost]
		public IActionResult StartGame(string PlayerName)
		{
			lock (StaticGameDataModelcs.ListPlayerLockObject)
			{
				if (StaticGameDataModelcs.GameDataModel.ListPlayer.Count < 2)
				{
					PlayerDataModel playerDataModel = new PlayerDataModel(PlayerName);
					StaticGameDataModelcs.GameDataModel.ListPlayer.Add(playerDataModel);
					if (StaticGameDataModelcs.GameDataModel.ListPlayer.Count == 1)
						StaticGameDataModelcs.GameDataModel.MotionPlayer = playerDataModel;
					HttpContext.Session.Set("Player", playerDataModel);
					return RedirectToAction("WaitingPlayers");
				}
			}
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult WaitingPlayers()
		{
			lock (StaticGameDataModelcs.ListPlayerLockObject)
			{
				if (StaticGameDataModelcs.GameDataModel.ListPlayer.Count != 2)
					return View();
			}
			return RedirectToAction("Game");
		}

		[HttpGet]
		public IActionResult Game()
		{
			PlayerDataModel? playerDataModel = HttpContext.Session.Get<PlayerDataModel>("Player");
			return playerDataModel != null ?
				View("Game", playerDataModel) :
				View("Game", new PlayerDataModel());
		}

		[HttpPost]
		public IActionResult Game(int idPole)
		{
			StaticGameDataModelcs.GameDataModel.MakeAMove(idPole);
			if (StaticGameDataModelcs.GameDataModel.Winner == -1)
				if (StaticGameDataModelcs.GameDataModel.ListPlayer[0] == StaticGameDataModelcs.GameDataModel.MotionPlayer)
					StaticGameDataModelcs.GameDataModel.MotionPlayer = StaticGameDataModelcs.GameDataModel.ListPlayer[1];
				else
					StaticGameDataModelcs.GameDataModel.MotionPlayer = StaticGameDataModelcs.GameDataModel.ListPlayer[0];
			return RedirectToAction("Game");
		}

		[HttpGet]
		public IActionResult RestartGame()
		{
			StaticGameDataModelcs.GameDataModel.FillTheField();
			return RedirectToAction("Game");
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