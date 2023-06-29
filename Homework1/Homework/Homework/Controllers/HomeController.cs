using Homework.Models;
using Homework.ServerDatabasa;
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
			return View(new LoginDataModel());
		}

		[HttpPost]
		public IActionResult Index(LoginDataModel dataModel)
		{
			if (ModelState.IsValid)
			{
				//QueryBuilder queryBuilder = new QueryBuilder();
				//queryBuilder.Add(nameof(dataModel.РlayerName), dataModel.РlayerName ?? string.Empty);

				//var currentRequestUrl = HttpContext.Request.GetEncodedUrl();

				//UriBuilder uriBuilder = new UriBuilder(currentRequestUrl);
				//uriBuilder.Path = this.Url.Action(nameof(SetLobby));
				//uriBuilder.Query = queryBuilder.ToString();

				//return Redirect(uriBuilder.Uri.ToString());

				return SetLobby(dataModel.РlayerName ?? "");

			}
			return View(dataModel);
		}
		private void ExitPlayer(PlayerDataModel playerDataModel)
		{
			int numberTable = playerDataModel.NumberTable;
			if (playerDataModel.Status == 1)
			{
				lock (GamingTables.GameTable[numberTable].ChangesLockObject)
				{
					for (int i = 0; i < GamingTables.GameTable[numberTable].Players.Count; i++)
					{
						if (GamingTables.GameTable[numberTable].Players[i].Id == playerDataModel.Id)
						{
							GamingTables.GameTable[numberTable].Players.RemoveAt(i);
							break;
						}
					}
				}
			}
		}

		public IActionResult FinishGame()
		{
			PlayerDataModel? playerDataModel = HttpContext.Session.Get<PlayerDataModel>("Player");
			if (playerDataModel == null)
				return View("SessionExpiration");
			else
			{
				ExitPlayer(playerDataModel);
				playerDataModel.NumberTable = -1;
				playerDataModel.Status = -1;
				HttpContext.Session.Set("Player", playerDataModel);
				return View();
			}
		}

		public IActionResult Exit()
		{
			return View("Index");
		}

		public IActionResult AnotherTable()
		{
			PlayerDataModel? playerDataModel = HttpContext.Session.Get<PlayerDataModel>("Player");
			if (playerDataModel == null)
				return View("SessionExpiration");
			else
			{
				ExitPlayer(playerDataModel);
				playerDataModel.NumberTable = -1;
				playerDataModel.Status = 0;
				HttpContext.Session.Set("Player", playerDataModel);
				return RedirectToAction("Lobby");
			}
		}
		//[HttpPost]
		public IActionResult SetLobby(string playerName)
		{
			PlayerDataModel playerDataModel = new(playerName);
			HttpContext.Session.Set("Player", playerDataModel);
			return RedirectToAction("Lobby");
		}

		public IActionResult Lobby()
		{
			PlayerDataModel? playerDataModel = HttpContext.Session.Get<PlayerDataModel>("Player");
			if (playerDataModel == null)
				return View("SessionExpiration");
			else
				return View(new LobbyModel(playerDataModel));
		}



		[HttpGet]
		public IActionResult StartGame(int tableNumber)
		{
			PlayerDataModel? playerDataModel = HttpContext.Session.Get<PlayerDataModel>("Player");
			if (playerDataModel == null)
				return View("SessionExpiration");
			else
			{
				lock (GamingTables.GameTable[tableNumber].ChangesLockObject)
				{
					if (GamingTables.GameTable[tableNumber].Players.Count < 2)
					{
						GamingTables.GameTable[tableNumber].Players.Add(playerDataModel);
						playerDataModel.Status = 1;
					}
					else
					{
						playerDataModel.Status = 2;
					}
				}
				playerDataModel.NumberTable = tableNumber;
				HttpContext.Session.Set("Player", playerDataModel);
				return RedirectToAction("WaitingPlayers");
			}
		}

		[HttpGet]
		public IActionResult WaitingPlayers()
		{
			int numberTable;
			PlayerDataModel? playerDataModel = HttpContext.Session.Get<PlayerDataModel>("Player");
			if (playerDataModel == null)
				return View("SessionExpiration");
			else
			{
				numberTable = playerDataModel.NumberTable;
			}
			if (GamingTables.GameTable[numberTable].MotionPlayer.Status == -1 ||
				GamingTables.GameTable[numberTable].Players.Count == 1)
			{
				lock (GamingTables.GameTable[numberTable].ChangesLockObject)
				{
					if (GamingTables.GameTable[numberTable].Players.Count == 1)
					{
						return View();
					}
					GamingTables.GameTable[numberTable].MotionPlayer = GamingTables.GameTable[numberTable].Players[0];
				}
				GamingTables.GameTable[numberTable].СreateTheField();
			}
			return RedirectToAction("Game");
		}

		[HttpGet]
		public IActionResult Game()
		{
			PlayerDataModel? playerDataModel = HttpContext.Session.Get<PlayerDataModel>("Player");
			if (playerDataModel == null)
				return View("SessionExpiration");
			else
				return View(new GameDataModel(playerDataModel));

		}

		[HttpPost]
		public IActionResult Game(int idPole)
		{
			int numberTable;
			PlayerDataModel? playerDataModel = HttpContext.Session.Get<PlayerDataModel>("Player");
			if (playerDataModel == null)
				return View("SessionExpiration");
			else
				numberTable = playerDataModel.NumberTable;

			GamingTables.GameTable[numberTable].MakeAMove(idPole);
			if (GamingTables.GameTable[numberTable].Winner == -1)
			{
				if (GamingTables.GameTable[numberTable].Players[0].Id == playerDataModel.Id)
					GamingTables.GameTable[numberTable].MotionPlayer = GamingTables.GameTable[numberTable].Players[1];
				else
					GamingTables.GameTable[numberTable].MotionPlayer = GamingTables.GameTable[numberTable].Players[0];
			}
			return RedirectToAction("Game");
		}

		[HttpGet]
		public IActionResult RestartGame()
		{
			PlayerDataModel? playerDataModel = HttpContext.Session.Get<PlayerDataModel>("Player");
			if (playerDataModel == null)
				return View("SessionExpiration");
			else
			{
				GamingTables.GameTable[playerDataModel.NumberTable].СreateTheField();
				return RedirectToAction("WaitingPlayers");
			}
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