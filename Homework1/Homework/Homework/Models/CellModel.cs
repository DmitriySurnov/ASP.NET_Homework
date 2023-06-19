namespace Homework.Models
{
	public class CellModel
	{
		public string CellText { get; private set; }

		public bool IsWinner { get; private set; }

		public int CurrenId { get; private set; }

		public bool IsMotionPlayer { private set; get; }

		public CellModel(int currenId, PlayerDataModel playerDataModel)
		{
			CurrenId = currenId;
			IsWinner = StaticGameDataModelcs.GameDataModel.Winner == -1;
			CellText = StaticGameDataModelcs.GameDataModel.Field[currenId];
			IsMotionPlayer = StaticGameDataModelcs.GameDataModel.MotionPlayer.Id == playerDataModel.Id;
		}
	}
}
