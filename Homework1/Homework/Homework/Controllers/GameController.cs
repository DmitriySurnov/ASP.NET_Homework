using Homework.Extentions;
using Homework.Models;
using Homework.Server;
using Homework.ServerDatabase;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Homework.Controllers
{
	public class GameController : Controller
    {

		private readonly ILogger<GameController> _logger;

		public GameController(ILogger<GameController> logger)
		{
			_logger = logger;
		}

		[HttpPost]
		public IActionResult StartGame(Guid tableGuid)
        {
            Guid key = HttpContext.Session.Get<Guid>("PlayerGuid");
            if (key == Guid.Empty)
                return View("../Home/SessionExpiration");

            if (Database.Players[key].NumberTable == tableGuid)
                return RedirectToAction("WaitingPlayers");
            if (Database.Players[key].NumberTable == Guid.Empty)
            {
                return JoiningTheGame(tableGuid, key);
            }
            else
            {
                ExitPlayer(Database.Players[key], key);
                Database.Players[key].NumberTable = Guid.Empty;
                return JoiningTheGame(tableGuid, key);
            }
        }

        public static void ExitPlayer(Player player, Guid playerKey)
        {

            Game game = Database.Tables[player.NumberTable];
            lock (game.ChangesLockObject)
            {
                if (game.WhichPlayerWalkingGuid == playerKey)
                {
                    game.WhichPlayerWalkingGuid = Guid.Empty;
                }
                if (game.PlayerXGuid == playerKey)
                {
                    game.PlayerXGuid = Guid.Empty;
                }
                else if (game.PlayerOGuid == playerKey)
                {
                    game.PlayerOGuid = Guid.Empty;
                }
                if (game.PlayerXGuid == Guid.Empty &&
                    game.PlayerOGuid == Guid.Empty)
                {
                    game.Field = string.Empty;
                }
            }
        }

        public IActionResult JoiningTheGame(Guid tableGuid, Guid Playerkey)
        {
            Database.Players[Playerkey].NumberTable = tableGuid;
            Game game = Database.Tables[tableGuid];
            lock (game.ChangesLockObject)
            {
                if (game.PlayerXGuid == Guid.Empty)
                {
                    game.PlayerXGuid = Playerkey;
                }
                else if (game.PlayerOGuid == Guid.Empty)
                {
                    game.PlayerOGuid = Playerkey;
                }
                if (game.WhichPlayerWalkingGuid == Guid.Empty)
                {
                    game.WhichPlayerWalkingGuid = Playerkey;
                }
                if (game.Field == string.Empty)
                {
                    var field = new Field();
                    field.FillTheField();
                    game.Field = string.Join(",", field.FieldGame);
                }
            }
            return RedirectToAction("WaitingPlayers");
        }

        [HttpPost]
        [HttpGet]
        public IActionResult WaitingPlayers()
        {
            Guid key = HttpContext.Session.Get<Guid>("PlayerGuid");
            if (key == Guid.Empty || !Database.Players.ContainsKey(key))
                return View("../Home/SessionExpiration");
            Game game = Database.Tables[Database.Players[key].NumberTable];
            if (game.PlayerXGuid != Guid.Empty &&
                game.PlayerOGuid != Guid.Empty)
            {
                return RedirectToAction("Game");
            }
            return View();
        }

		[HttpPost]
		public IActionResult FinishGame()
		{
			Guid key = HttpContext.Session.Get<Guid>("PlayerGuid");
			if (!Database.Players.ContainsKey(key))
				return View();
			Player player = Database.Players[key];
			if (player.NumberTable == Guid.Empty)
				return View();
			ExitPlayer(player, key);
			Database.Players.Remove(key);
			return View();
		}

		public IActionResult AnotherTable()
		{
			Guid key = HttpContext.Session.Get<Guid>("PlayerGuid");
			if (key == Guid.Empty)
				return View("../Home/SessionExpiration");
			Player player = Database.Players[key];
			if (player.NumberTable == Guid.Empty)
				return RedirectToAction("Lobby");
			ExitPlayer(player, key);
			player.NumberTable = Guid.Empty;
			return RedirectToAction("Lobby", 
                nameof(HomeController).Replace("Controller", ""));
		}

		[HttpGet]
		public IActionResult Game()
		{
			Guid key = HttpContext.Session.Get<Guid>("PlayerGuid");
			if (key == Guid.Empty || !Database.Players.ContainsKey(key))
				return View("../Home/SessionExpiration");
			else
				return View(new GameDataModel(key));

		}

		[HttpPost]
		public IActionResult Game(int idPole)
		{
			Guid key = HttpContext.Session.Get<Guid>("PlayerGuid");
			if (key == Guid.Empty || !Database.Players.ContainsKey(key))
				return View("../Home/SessionExpiration");
			Game game = Database.Tables[Database.Players[key].NumberTable];
			Field field = new Field(game.Field, game.PlayerXGuid == key);
			field.MakeAMove(idPole);
			game.Field = string.Join(",", field.FieldGame);
			game.WhichPlayerWalkingGuid = field.IsX ?
						game.PlayerOGuid : game.PlayerXGuid;
			return RedirectToAction("WaitingPlayers");
		}

		[HttpPost]
		public IActionResult RestartGame()
		{
			Guid key = HttpContext.Session.Get<Guid>("PlayerGuid");
			if (key == Guid.Empty || !Database.Players.ContainsKey(key))
				return View("../Home/SessionExpiration");
            Game game = Database.Tables[Database.Players[key].NumberTable];
			lock (game.ChangesLockObject)
			{
				if (game.PlayerOGuid == Guid.Empty ||
					game.PlayerXGuid == Guid.Empty)
				{
					return RedirectToAction("WaitingPlayers");
				}
				var field = new Field();
				field.FillTheField();
				game.Field = string.Join(",", field.FieldGame);
			}
			return RedirectToAction("WaitingPlayers");
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
