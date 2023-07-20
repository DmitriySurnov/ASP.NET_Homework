using Homework.Extentions;
using Homework.Models;
using Homework.Server;
using Homework.ServerDatabasa;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Homework.Controllers
{
	public class GameController : Controller
    {

        private Database _database;

        public GameController(Database database)
        {
            _database = database;
        }

        [HttpPost]
		public IActionResult StartGame(Guid tableGuid)
        {
            Guid playerGuid = HttpContext.Session.Get<Guid>("PlayerGuid");
            if (playerGuid == Guid.Empty)
                return View("../Home/SessionExpiration");

            if (_database.Players[playerGuid].NumberTable == tableGuid)
                return RedirectToAction("WaitingPlayers");
            if (_database.Players[playerGuid].NumberTable == Guid.Empty)
            {
                return JoiningTheGame(tableGuid, playerGuid);
            }
            else
            {
                ExitPlayer(_database.Players[playerGuid], playerGuid, _database);
                _database.Players[playerGuid].NumberTable = Guid.Empty;
                return JoiningTheGame(tableGuid, playerGuid);
            }
        }

        public static void ExitPlayer(Player player, Guid playerKey, Database database)
        {

            Game game = database.Tables[player.NumberTable];
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
            _database.Players[Playerkey].NumberTable = tableGuid;
            Game game = _database.Tables[tableGuid];
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
            Guid playerGuid = HttpContext.Session.Get<Guid>("PlayerGuid");
            if (playerGuid == Guid.Empty || !_database.Players.ContainsKey(playerGuid))
                return View("../Home/SessionExpiration");
			if (_database.Players[playerGuid].NumberTable == Guid.Empty)
			{
				return RedirectToAction("Lobby",
				nameof(HomeController).Replace("Controller", ""));
			}
			Guid numberTable = _database.Players[playerGuid].NumberTable;
			Game game = _database.Tables[numberTable];
			if (game.PlayerXGuid != Guid.Empty &&
                game.PlayerOGuid != Guid.Empty)
            {
                return RedirectToAction("TicTacToe");
            }
			foreach (var player in _database.Players)
			{
				if (player.Key == game.PlayerOGuid ||
	player.Key == game.PlayerXGuid || player.Key == Guid.Empty)
				{
					continue;
				}
				if (player.Value.NumberTable == numberTable)
				{
					return JoiningTheGame(numberTable, player.Key);
				}
			}
			return View();
        }

		[HttpPost]
		public IActionResult FinishGame()
		{
			Guid playerGuid = HttpContext.Session.Get<Guid>("PlayerGuid");
			if (!_database.Players.ContainsKey(playerGuid))
				return View();
			Player player = _database.Players[playerGuid];
			if (player.NumberTable == Guid.Empty)
				return View();
			ExitPlayer(player, playerGuid, _database);
			_database.Players.Remove(playerGuid);
			return View();
		}

		public IActionResult AnotherTable()
		{
			Guid playerGuid = HttpContext.Session.Get<Guid>("PlayerGuid");
			if (playerGuid == Guid.Empty)
				return View("../Home/SessionExpiration");
			Player player = _database.Players[playerGuid];
			if (player.NumberTable == Guid.Empty)
				return RedirectToAction("Lobby");
			ExitPlayer(player, playerGuid, _database);
			player.NumberTable = Guid.Empty;
			return RedirectToAction("Lobby", 
                nameof(HomeController).Replace("Controller", ""));
		}

		[HttpGet]
		public IActionResult TicTacToe()
		{
			Guid playerGuid = HttpContext.Session.Get<Guid>("PlayerGuid");
            if (playerGuid == Guid.Empty || !_database.Players.ContainsKey(playerGuid))
            {
                return View("../Home/SessionExpiration");
            }
			Player player = _database.Players[playerGuid];
			if (player.NumberTable == Guid.Empty)
			{
				return RedirectToAction("Lobby",
				nameof(HomeController).Replace("Controller", ""));
			}
			Game game = _database.Tables[_database.Players[playerGuid].NumberTable];
			if (game.PlayerXGuid == Guid.Empty ||
				game.PlayerOGuid == Guid.Empty)
			{
				return RedirectToAction("WaitingPlayers");
			}
			return View(new GameDataModel(playerGuid, _database));
		}

		[HttpPost]
		public IActionResult Game(int idPole)
		{
			Guid playerGuid = HttpContext.Session.Get<Guid>("PlayerGuid");
			if (playerGuid == Guid.Empty || !_database.Players.ContainsKey(playerGuid))
				return View("../Home/SessionExpiration");
			Game game = _database.Tables[_database.Players[playerGuid].NumberTable];
			Field field = new Field(game.Field, game.PlayerXGuid == playerGuid);
			field.MakeAMove(idPole);
			game.Field = string.Join(",", field.FieldGame);
			game.WhichPlayerWalkingGuid = field.IsX ?
						game.PlayerOGuid : game.PlayerXGuid;
			return RedirectToAction("WaitingPlayers");
		}

		[HttpPost]
		public IActionResult RestartGame()
		{
			Guid playerGuid = HttpContext.Session.Get<Guid>("PlayerGuid");
			if (playerGuid == Guid.Empty || !_database.Players.ContainsKey(playerGuid))
				return View("../Home/SessionExpiration");
            Game game = _database.Tables[_database.Players[playerGuid].NumberTable];
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
