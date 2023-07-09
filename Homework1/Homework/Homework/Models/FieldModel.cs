namespace Homework.Models
{
	public class FieldModel
	{
		public CellModel[] FieldGame { get; private set; }

		public string FieldString { get; private set; }

		public FieldModel(string[] fieldGame)
		{
			FieldGame = new CellModel[fieldGame.Length];
			for (int i = 0; i < fieldGame.Length; i++)
			{
				FieldGame[i] = new CellModel(fieldGame[i][0]);
			}
			FieldString = "";
		}

		public FieldModel(string[] fieldGame, bool? IsCurrentPlayerMove, bool isWinner)
		{
			FieldGame = new CellModel[fieldGame.Length];
			for (int i = 0; i < fieldGame.Length; i++)
			{
				if (IsCurrentPlayerMove == null)
				{
					FieldGame[i] = new CellModel(fieldGame[i][0]);
				}
				else
				{
					FieldGame[i] = new CellModel(i, fieldGame[i][0], isWinner, (bool)IsCurrentPlayerMove);
				}

			}
			FieldString = string.Join(",", fieldGame);
		}
	}
}
