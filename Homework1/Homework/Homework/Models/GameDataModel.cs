namespace Homework.Models
{
	public class GameDataModel
	{
		public static PlayerDataModel FirstPlayer { get; set; }
		public static PlayerDataModel SecondPlayer { get; set; }
		public string[] Field { private set; get; }
		public string FildString { set; get; }
		public bool IsX { get; set; } = true;

		public void FillTheField()
		{
			Random random = new Random();
			Field = new string[9];
			for (int i = 0; i < 9; i++)
			{
				int value = random.Next(0, 3);
				Field[i] = value == 0 ? "" : value == 1 ? "X" : "O";
			}
			FildString = string.Join(",", Field);
		}

		public void FillTheField(string fildsString)
		{
			FildString = fildsString;
			Field = FildString.Split(',');
		}

		public void MakeAMove(int id)
		{
			Field[id] = IsX ? "X" : "O";
			IsX = !IsX;
			FildString = string.Join(",", Field);
		}
	}
}
