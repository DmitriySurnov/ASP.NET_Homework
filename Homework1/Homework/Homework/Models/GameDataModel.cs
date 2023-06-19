namespace Homework.Models
{
	public class GameDataModel
	{

		public string[] Field { private set; get; }

		public string FieldString { set; get; }

		public bool IsX { get; set; }

		public bool IsMove { get; set; }

		public int Winner { get; private set; }

		public List<PlayerDataModel> ListPlayer { set; get; }

		public PlayerDataModel MotionPlayer { set; get; }

		public GameDataModel()
		{
			IsX = true;
			FieldString = string.Empty;
			Field = new string[9];
			IsMove = true;
			FillTheField();
			Winner = -1;
			DeterminingWinner();
			MotionPlayer = new PlayerDataModel();
			ListPlayer = new List<PlayerDataModel>();
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
			IsMove = !(FieldString.Length == 17);
			DeterminingWinner();
		}

		private void DeterminingWinner()
		{
			Winner = -1;
			int[,] combinations = {
				{ 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 },
				{ 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 },
				{ 0, 4, 8 }, { 2, 4, 6 }};
			for (int i = 0; i < 8; i++)
			{
				if (Field[combinations[i, 0]] == Field[combinations[i, 1]] &&
					Field[combinations[i, 0]] == Field[combinations[i, 2]] && Field[combinations[i, 0]] != "")
				{
					IsMove = false;
					Winner = Field[combinations[i, 0]] == "X" ? 1 : 2;
					break;
				}
			}
			if (!IsMove && Winner == -1)
				Winner = 0;
		}

		public void MakeAMove(int id)
		{
			Field[id] = IsX ? "X" : "O";
			FieldString = string.Join(",", Field);
			IsMove = !(FieldString.Length == 17);
			DeterminingWinner();
			if (Winner == -1)
				IsX = !IsX;
		}
	}
}
