using Homework.ServerDatabasa;

namespace Homework.Models
{
	public class LobbyModel
	{
		public string СurrentРlayerName { get; private set; }

		public Table[] Tables { get; private set; }

		public LobbyModel(PlayerDataModel player)
		{
			СurrentРlayerName = player.Name;
			Tables = new Table[3];
			for (int currenTable = 0; currenTable < Tables.Length; currenTable++)
			{
				Tables[currenTable] = new Table(player, currenTable);
			}
		}
	}
}
