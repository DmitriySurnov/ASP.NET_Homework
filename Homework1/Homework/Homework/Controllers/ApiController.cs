﻿using Homework.Extentions;
using Homework.RequestDataModel;
using Homework.Server;
using Homework.ServerDatabase;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Controllers
{
	public class ApiController : Controller
    {
        public Guid[] GetTables()
        {
            return Database.Tables.Keys.ToArray();
        }

        public Guid[] GetUsers()
        {
            return Database.Players.Keys.ToArray();
        }

        [HttpPost]
        public string GetUserNameJson([FromBody] GetUserNameRequest value)
        {
            if (Database.Players.ContainsKey(value.userGuid))
                return Database.Players[value.userGuid].Name;
            else
                return "неправильный id user";
        }

        [HttpPost]
        public string GetUserName(string userGuid)
        {
            if (Database.Players.ContainsKey(new Guid(userGuid)))
                return Database.Players[new Guid(userGuid)].Name;
            else
                return "неправильный id user";
        }

        [HttpPost]
        public string GetUserNameGuid(Guid userGuid)
        {
            if (Database.Players.ContainsKey(userGuid))
                return Database.Players[userGuid].Name;
            else
                return "неправильный id user";
        }

        public bool IsTwoPlayersInTheGame()
        {
            Guid key = HttpContext.Session.Get<Guid>("PlayerGuid");
            if (key == Guid.Empty || !Database.Players.ContainsKey(key))
            {
                return false;
            }
            Player player = Database.Players[key];
            Game game = Database.Tables[player.NumberTable];
            lock (game.ChangesLockObject)
            {
                if (game.PlayerOGuid != Guid.Empty &&
                game.PlayerXGuid != Guid.Empty)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
