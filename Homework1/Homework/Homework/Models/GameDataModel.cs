using Homework.Server;
using Homework.ServerDatabase;

namespace Homework.Models
{
	public class GameDataModel
    {
		public string СurrentРlayerName { get; private set; } // Имя текущего игрока

		public bool IsРlayer { get; private set; } // это игрок или зритель

		public bool IsCurrentPlayerMove { get; private set; } // является текущим ходом игрока

		public FieldModel GameField { get; private set; } // Игровое поле 

		public string InformationAboutWinner { get; private set; } // Информация кто победил

		public GameDataModel(Guid playerGuid)
		{
			Player player = Database.Players[playerGuid];
			СurrentРlayerName = player.Name;
			Game game = Database.Tables[player.NumberTable];
			if (game.PlayerXGuid == playerGuid ||
				game.PlayerOGuid == playerGuid)
			{
				IsРlayer = true;
			}
			else
			{
				IsРlayer = false;
			}
			IsCurrentPlayerMove = game.WhichPlayerWalkingGuid == playerGuid;
			Field field = new Field(game.Field, IsCurrentPlayerMove);
			field.DeterminingWinner();
			GameField = new FieldModel(field.FieldGame, IsCurrentPlayerMove, field.Winner != -1);
			switch (field.Winner)
			{
				case 0:
					{
						InformationAboutWinner = "Ничья";
						break;
					}
				case 1:
					{
						InformationAboutWinner = $"Победил {Database.Players[game.PlayerXGuid].Name}";
						break;
					}
				case 2:
					{
						InformationAboutWinner = $"Победил {Database.Players[game.PlayerOGuid].Name}";
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
