namespace Homework.Models
{
	public class CellModel
	{
		public char CellText { get; private set; }

		public bool IsWinner { get; private set; }

		public int CurrenId { get; private set; }

		public bool IsMotionPlayer { get; private set; }

		public CellModel(char cellText)
		{
			CurrenId = 0;
			CellText = cellText;
			IsWinner = true;
			IsMotionPlayer = false;
		}

		public CellModel(int currenId, char cellText, bool isWinner, bool IsCurrentPlayerMove)
		{
			CurrenId = currenId;
			CellText = cellText;
			IsWinner = isWinner;
			IsMotionPlayer = IsCurrentPlayerMove;
		}
	}
}
