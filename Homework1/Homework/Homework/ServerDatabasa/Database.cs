using Homework.Server;

namespace Homework.ServerDatabasa
{
	public class Database
	{
		public Dictionary<Guid, Player> Players { get; }

		public Dictionary<Guid, Game> Tables { get; }

		public Database()
		{
			Players = new Dictionary<Guid, Player>();
			Tables = new Dictionary<Guid, Game>();
			for (int couter = 0; couter < 3; couter++)
				Tables.Add(Guid.NewGuid(), new Game());
		}
	}
}
