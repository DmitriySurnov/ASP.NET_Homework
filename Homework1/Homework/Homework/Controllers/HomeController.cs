using Homework.Extentions;
using Homework.Models;
using Homework.Server;
using Homework.ServerDatabase;
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
            Guid key = HttpContext.Session.Get<Guid>("PlayerGuid");
            if (key != Guid.Empty)
            {
                _database.Players.Remove(key);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult SetLobby(string playerName)
        {
            Guid key = Guid.NewGuid();
            _database.Players.Add(key, new Player(playerName));
            HttpContext.Session.Set("PlayerGuid", key);
            return RedirectToAction("Lobby");
        }

        [HttpPost]
        [HttpGet]
        public IActionResult Lobby()
        {
            Guid key = HttpContext.Session.Get<Guid>("PlayerGuid");
            if (key == Guid.Empty || !_database.Players.ContainsKey(key))
                return View("SessionExpiration");
            else
                return View(new LobbyModel(_database.Players[key], _database));
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