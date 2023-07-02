using Homework.ServerDatabasa;

namespace Homework.Models
{
	public class Table
	{
		public int NumberTable { get; private set; } // Номер стола

		public string[] NamesPlayers { get; private set; } // Имена играков

		public int NumberFilledCells { get; private set; } // количество ходов от старта 

		public string MotionPlayerName { get; private set; } // Имя игрока который сейчас ходит

		public FieldModel GameField { get; private set; } //  игровое полее

		public Table(PlayerDataModel player, int numberTable)
		{
			NumberTable = numberTable;
			NamesPlayers = new string[2];
			SetNamesPlayer(0, numberTable);
			SetNamesPlayer(1, numberTable);
			if (GamingTables.GameTable[numberTable].Players.Count == 2)
			{
				MotionPlayerName = GamingTables.GameTable[numberTable].MotionPlayer.Name;
				NumberFilledCells = GamingTables.GameTable[numberTable].Field.NumberFilledCells;
				GameField = new FieldModel(GamingTables.GameTable[numberTable].Field.FieldGame, numberTable, player);
			}
			else
			{
				MotionPlayerName = new PlayerDataModel().Name;
				Field fieldGame = new Field();
				fieldGame.FillTheField();
				NumberFilledCells = fieldGame.NumberFilledCells;
				GameField = new FieldModel(fieldGame.FieldGame, numberTable, player);
			}
		}

		private void SetNamesPlayer(int currenId,int numberTable)
		{
			if (GamingTables.GameTable[numberTable].Players.Count > currenId)
				NamesPlayers[currenId] = GamingTables.GameTable[numberTable].Players[currenId].Name;
			else
				NamesPlayers[currenId] = new PlayerDataModel().Name;

		}
	}
}
