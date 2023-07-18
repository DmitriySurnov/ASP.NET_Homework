﻿using Homework.Server;
using Homework.ServerDatabase;

namespace Homework.Models
{
	public class Observers
	{
		public List<string> UserNameWatchersGame { get; set; }

		public List<string> UserNameWatchersGameLobby { get; set; }

		public Observers(Guid playerGuid, Database database)
		{
			UserNameWatchersGame = new List<string>();
			UserNameWatchersGameLobby = new List<string>();
			Player user = database.Players[playerGuid];
			foreach(var player in database.Players)
			{
				if (player.Key == playerGuid)
				{
					continue;
				}
				if (player.Value.NumberTable == Guid.Empty)
				{
					UserNameWatchersGameLobby.Add(player.Value.Name);
				}
				else if(player.Value.NumberTable == user.NumberTable)
				{
					UserNameWatchersGame.Add(player.Value.Name);
				}
			}
		}
	}
}
