using Homework.ServerDatabasa;

namespace Homework.Models
{
    public class GameDataModel
    {
		public string СurrentРlayerName { get; private set; } // Имя текущего игрока

		public bool IsРlayer { get; private set; } // это игрок или зритель

		public bool IsCurrentPlayerMove { get; private set; } // является текущим ходом игрока

		public FieldModel GameField { get; private set; } // Игровое поле 

		public string InformationAboutWinner { get; private set; } // Информация кто победил

		public GameDataModel(PlayerDataModel player)
        {
			СurrentРlayerName = player.Name;
			IsРlayer = player.Status == 1;
			IsCurrentPlayerMove = GamingTables.GameTable[player.NumberTable].MotionPlayer.Id == player.Id;
			GameField = new FieldModel(GamingTables.GameTable[player.NumberTable].Field.FieldGame, player.NumberTable, player);
			switch (GamingTables.GameTable[player.NumberTable].Winner)
			{
				case 0:
					{
						InformationAboutWinner = "Ничья";
						break;
					}
				case 1:
					{
						InformationAboutWinner = $"Победил {GamingTables.GameTable[player.NumberTable].Players[0].Name}";
						break;
					}
				case 2:
					{
						InformationAboutWinner = $"Победил {GamingTables.GameTable[player.NumberTable].Players[1].Name}";
						break;
					}
				default:
					{
						InformationAboutWinner = "";
						break;
					}
			}
		}
    }
}
