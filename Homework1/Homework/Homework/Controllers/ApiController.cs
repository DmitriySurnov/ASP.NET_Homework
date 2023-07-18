using Homework.Extentions;
using Homework.Models;
using Homework.RequestDataModel;
using Homework.Server;
using Homework.ServerDatabase;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Homework.Controllers
{
    public class ApiController : Controller
    {
        private Database _database;

        public ApiController(Database database)
        {
            _database = database;
        }

        public Guid[] GetTables()
        {
            return _database.Tables.Keys.ToArray();
        }

        public Guid[] GetUsers()
        {
            return _database.Players.Keys.ToArray();
        }

        [HttpPost]
        public string GetUserNameJson([FromBody] GetUserNameRequest value)
        {
            if (_database.Players.ContainsKey(value.userGuid))
                return _database.Players[value.userGuid].Name;
            else
                return "неправильный id user";
        }

        [HttpPost]
        public string GetUserName(string userGuid)
        {
            if (_database.Players.ContainsKey(new Guid(userGuid)))
                return _database.Players[new Guid(userGuid)].Name;
            else
                return "неправильный id user";
        }

        [HttpPost]
        public string GetUserNameGuid(Guid userGuid)
        {
            if (_database.Players.ContainsKey(userGuid))
                return _database.Players[userGuid].Name;
            else
                return "неправильный id user";
        }

        public int NumberPlayersInGame()
        {
            Guid key = HttpContext.Session.Get<Guid>("PlayerGuid");
            if (key == Guid.Empty || !_database.Players.ContainsKey(key))
            {
                return 0;
            }
            Player player = _database.Players[key];
            Game game = _database.Tables[player.NumberTable];
            lock (game.ChangesLockObject)
            {
                if (game.PlayerOGuid == Guid.Empty ||
                    game.PlayerXGuid == Guid.Empty)
                {
                    return 1;
                }
            }
            return 2;
        }

        [HttpPost]
        public IActionResult GetTableHtml(
            [FromRoute(Name = "id")] Guid tableGuid,
            [FromRoute(Name = "number")] int numberTable)
        {
            Guid key = HttpContext.Session.Get<Guid>("PlayerGuid");
            if (key == Guid.Empty || !_database.Players.ContainsKey(key)
                || !_database.Tables.ContainsKey(tableGuid))
            {
                return NotFound();
            }
            Game game = _database.Tables[tableGuid];
            return PartialView("../_Partial/Table", new Table(tableGuid, numberTable, game, _database));
        }

        [HttpPost]
        public IActionResult GetUpdatingGameHtml(
            [FromRoute(Name = "id")] string field)
        {
            Guid key = HttpContext.Session.Get<Guid>("PlayerGuid");
            if (key == Guid.Empty || !_database.Players.ContainsKey(key))
            {
                return NotFound();
            }
            Player player = _database.Players[key];
            Game game = _database.Tables[player.NumberTable];
            if (game.Field == field)
            {
                return NotFound();
            }
            return PartialView("../Game/TicTacToe", new GameDataModel(key, _database));
        }

        [HttpPost]
        public IActionResult GetWatchersGameHtml()
        {
            Guid key = HttpContext.Session.Get<Guid>("PlayerGuid");
            if (key == Guid.Empty || !_database.Players.ContainsKey(key))
            {
                return NotFound();
            }
            return PartialView("../_Partial/Observers", new Observers(key, _database));
        }
    }
}
