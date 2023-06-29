using Homework.ServerDatabasa;

namespace Homework.Models
{
    public class FieldModel
	{
		public CellModel[] FieldGame { get; private set; }

		public FieldModel(char[] fieldGame, int numberTable, PlayerDataModel player) {
			FieldGame = new CellModel[fieldGame.Length];
			for (int i = 0; i < fieldGame.Length; i++)
			{
				FieldGame[i] = new CellModel(i, fieldGame[i], numberTable,player);
			}
		}
	}
}
