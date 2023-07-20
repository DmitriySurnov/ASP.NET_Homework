using Homework.Extentions;
using Homework.Models;
using Homework.Server;
using Homework.ServerDatabasa;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Homework.Controllers
{
    public class HomeController : Controller
    {

        private Database _database;

        public HomeController(Database database)
        {
            _database = database;
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
                return this.RedirectToAddPlayerPage(dataModel);
            }
            return View(dataModel);
        }

        [HttpPost]
        [HttpGet]
        public IActionResult ToMainPage()
        {
            Guid playerGuid = HttpContext.Session.Get<Guid>("PlayerGuid");
            if (playerGuid != Guid.Empty)
            {
				if (_database.Players.ContainsKey(playerGuid))
				{
					ExitPlayer(playerGuid);
				}
			}
            return RedirectToAction("Index");
        }

		private void ExitPlayer(Guid playerGuid)
		{
			if (_database.Players.ContainsKey(playerGuid))
			{
				Player player = _database.Players[playerGuid];
				if (player.NumberTable != Guid.Empty)
				{
					GameController.ExitPlayer(player, playerGuid, _database);
				}
				_database.Players.Remove(playerGuid);
			}
		}

		[HttpGet]
        public IActionResult SetLobby(string playerName)
        {
			Guid playerGuid = HttpContext.Session.Get<Guid>("PlayerGuid");
			if (playerGuid != Guid.Empty)
			{
				ExitPlayer(playerGuid);
			}
			playerGuid = Guid.NewGuid();
            _database.Players.Add(playerGuid, new Player(playerName));
            HttpContext.Session.Set("PlayerGuid", playerGuid);
            return RedirectToAction("Lobby");
        }

        [HttpPost]
        [HttpGet]
        public IActionResult Lobby()
        {
            Guid playerGuid = HttpContext.Session.Get<Guid>("PlayerGuid");
            if (playerGuid == Guid.Empty || !_database.Players.ContainsKey(playerGuid))
                return View("SessionExpiration");
			Player player = _database.Players[playerGuid];
			if (player.NumberTable != Guid.Empty)
			{
				GameController.ExitPlayer(player, playerGuid, _database);
				player.NumberTable = Guid.Empty;
			}
			return View(new LobbyModel(_database.Players[playerGuid], _database));
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