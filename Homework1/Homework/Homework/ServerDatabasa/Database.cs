using Homework.Server;

namespace Homework.ServerDatabase
{
	public class Database
	{
		public Dictionary<Guid, Player> Players;

		public Dictionary<Guid, Game> Tables;

		public Database()
		{
			Players = new Dictionary<Guid, Player>();
			Tables = new Dictionary<Guid, Game>();
			for (int couter = 0; couter < 3; couter++)
				Tables.Add(Guid.NewGuid(), new Game());
		}
	}
}
