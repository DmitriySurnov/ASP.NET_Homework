using Homework.ServerDatabasa;

namespace Homework.Models
{
	public class Table
	{
		public int NumberTable { get; private set; }

		public string[] NamesPlayer { private set; get; }

		public int NumberFilledCells { get; private set; }

		public string MotionPlayer { set; get; }

		public FieldModel FieldGame { private set; get; }

		public Table(PlayerDataModel player, int numberTable)
		{
			NumberTable = numberTable;
			NamesPlayer = new string[2];
			SetNamesPlayer(0, numberTable);
			SetNamesPlayer(1, numberTable);
			if (GamingTables.GameTable[numberTable].Players.Count == 2)
			{
				MotionPlayer = GamingTables.GameTable[numberTable].MotionPlayer.Name;
				NumberFilledCells = GamingTables.GameTable[numberTable].Field.NumberFilledCells;
				FieldGame = new FieldModel(GamingTables.GameTable[numberTable].Field.FieldGame, numberTable, player);
			}
			else
			{
				MotionPlayer = new PlayerDataModel().Name;
				Field fieldGame = new Field();
				fieldGame.FillTheField();
				NumberFilledCells = fieldGame.NumberFilledCells;
				FieldGame = new FieldModel(fieldGame.FieldGame, numberTable, player);
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
