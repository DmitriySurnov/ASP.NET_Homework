using Homework.ServerDatabasa;

namespace Homework.Models
{
	public class CellModel
	{
		public char CellText { get; private set; }

		public bool IsWinner { get; private set; }

		public int CurrenId { get; private set; }

		public bool IsMotionPlayer { private set; get; }

		public CellModel(int currenId, char cellText, int numberTable, PlayerDataModel player) {
			CurrenId = currenId;
			CellText = cellText;
			IsWinner = GamingTables.GameTable[numberTable].Winner != -1;
			IsMotionPlayer = GamingTables.GameTable[numberTable].MotionPlayer.Id == player.Id;
		}
	}
}
