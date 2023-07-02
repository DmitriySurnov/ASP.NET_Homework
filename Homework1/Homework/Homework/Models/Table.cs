using Homework.ServerDatabasa;

namespace Homework.Models
{
	public class Table
	{
		public int NumberTable { get; private set; }

		public string[] NamesPlayer { get; private set; }

		public int NumberFilledCells { get; private set; }

		public string MotionPlayer { get; private set; }

		public FieldModel GameField { get; private set; }

		public Table(PlayerDataModel player, int numberTable)
		{
			NumberTable = numberTable+1;
			NamesPlayer = new string[2];
			SetNamesPlayer(0, numberTable);
			SetNamesPlayer(1, numberTable);
			if (GamingTables.GameTable[numberTable].Players.Count == 2)
			{
				MotionPlayer = GamingTables.GameTable[numberTable].MotionPlayer.Name;
				NumberFilledCells = GamingTables.GameTable[numberTable].Field.NumberFilledCells;
				GameField = new FieldModel(GamingTables.GameTable[numberTable].Field.FieldGame, numberTable, player);
			}
			else
			{
				MotionPlayer = new PlayerDataModel().Name;
				Field fieldGame = new Field();
				fieldGame.FillTheField();
				NumberFilledCells = fieldGame.NumberFilledCells;
				GameField = new FieldModel(fieldGame.FieldGame, numberTable, player);
			}
		}

		private void SetNamesPlayer(int currenId,int numberTable)
		{
			if (GamingTables.GameTable[numberTable].Players.Count > currenId)
				NamesPlayer[currenId] = GamingTables.GameTable[numberTable].Players[currenId].Name;
			else
				NamesPlayer[currenId] = new PlayerDataModel().Name;

		}
	}
}
