using Homework.Server;
using Homework.ServerDatabasa;

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
			Game game = database.Tables[user.NumberTable];
			foreach (var player in database.Players)
			{
				if (player.Key == playerGuid || player.Key == game.PlayerOGuid ||
					player.Key == game.PlayerXGuid)
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
