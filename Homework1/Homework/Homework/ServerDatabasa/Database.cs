using Homework.Server;

namespace Homework.ServerDatabase
{
	public static class Database
	{
		public static Dictionary<Guid, Player> Players;

		public static Dictionary<Guid, Game> Tables;

		static Database()
		{
			Players = new Dictionary<Guid, Player>();
			Tables = new Dictionary<Guid, Game>();
			for (int couter = 0; couter < 3; couter++)
				Tables.Add(Guid.NewGuid(), new Game());
		}
	}
}
