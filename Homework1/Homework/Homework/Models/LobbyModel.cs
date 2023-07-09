using Homework.Server;
using Homework.ServerDatabase;

namespace Homework.Models
{
	public class LobbyModel
	{
		public string СurrentРlayerName { get; private set; }

		public Table[] Tables { get; private set; }

		public LobbyModel(Player player)
		{
			СurrentРlayerName = player.Name;
			Tables = new Table[Database.Tables.Count];
			int currenTable = 0;
			foreach (var table in Database.Tables)
			{
				Tables[currenTable] = new Table(table.Key, currenTable, table.Value);
				currenTable++;
			}
		}
	}
}
