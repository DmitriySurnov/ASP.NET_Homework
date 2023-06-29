using Homework.ServerDatabasa;

namespace Homework.Models
{
	public class LobbyModel
	{
		public string NameСurrentРlayer { get; private set; }

		public Table[] Tables { get; private set; }

		public LobbyModel(PlayerDataModel player)
		{
			NameСurrentРlayer = player.Name;
			Tables = new Table[3];
			for (int currenTable = 0; currenTable < Tables.Length; currenTable++)
			{
				Tables[currenTable] = new Table(player, currenTable);
			}
		}
	}
}
