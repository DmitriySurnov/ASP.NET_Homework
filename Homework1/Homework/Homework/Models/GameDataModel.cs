namespace Homework.Models
{
	public class GameDataModel
	{
		public PlayerDataModel FirstPlayer { get; set; }
		public PlayerDataModel SecondPlayer { get; set; }
		public string[] Field { private set; get; }
		public string FieldString { set; get; }
		public bool IsX { get; set; }

		public GameDataModel(string firstPlayer, string secondPlayer, string fieldString, bool isX)
		{
			FirstPlayer = new PlayerDataModel(firstPlayer);
			SecondPlayer = new PlayerDataModel(secondPlayer);
			FieldString = fieldString;
			IsX = isX;
			Field = FieldString.Split(',');
		}

		public GameDataModel(string firstPlayer, string secondPlayer)
		{
			FirstPlayer = new PlayerDataModel(firstPlayer);
			SecondPlayer = new PlayerDataModel(secondPlayer);
			IsX = true;
			FieldString = string.Empty;
			Field = new string[9];
			FillTheField();
		}

		public  GameDataModel() { 
			FirstPlayer = new PlayerDataModel();
			SecondPlayer = new PlayerDataModel();
			IsX = true;
			FieldString = string.Empty;
			Field = new string[9];
		}

		public void FillTheField()
		{
			Random random = new Random();
			for (int i = 0; i < 9; i++)
			{
				int value = random.Next(0, 3);
				Field[i] = value == 0 ? "" : value == 1 ? "X" : "O";
			}
			FieldString = string.Join(",", Field);
		}

		public void MakeAMove(int id)
		{
			Field[id] = IsX ? "X" : "O";
			IsX = !IsX;
			FieldString = string.Join(",", Field);
		}
	}
}
