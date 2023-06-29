using Homework.ServerDatabasa;

namespace Homework.Models
{
    public class GameDataModel
    {
		public string NameСurrentРlayer { get; private set; }

		public bool IsРlayer { get; private set; }

		public bool MotionPlayer { set; get; }

		public FieldModel FieldGame { private set; get; }

		public bool IsX { get; set; }

		public string WinnerРlayer { get; private set; }

		public GameDataModel(PlayerDataModel player)
        {
			NameСurrentРlayer = player.Name;
			IsРlayer = player.Status == 1;
			MotionPlayer = GamingTables.GameTable[player.NumberTable].MotionPlayer.Id == player.Id;
			FieldGame = new FieldModel(GamingTables.GameTable[player.NumberTable].Field.FieldGame, player.NumberTable, player);
			IsX = GamingTables.GameTable[player.NumberTable].Field.IsX;
			switch (GamingTables.GameTable[player.NumberTable].Winner)
			{
				case 0:
					{
						WinnerРlayer = "Ничья";
						break;
					}
				case 1:
					{
						WinnerРlayer = $"Победил {GamingTables.GameTable[player.NumberTable].Players[0].Name}";
						break;
					}
				case 2:
					{
						WinnerРlayer = $"Победил {GamingTables.GameTable[player.NumberTable].Players[1].Name}";
						break;
					}
				default:
					{
						WinnerРlayer = "";
						break;
					}
			}
		}
    }
}
