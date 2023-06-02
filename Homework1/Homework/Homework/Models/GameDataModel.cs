namespace Homework.Models
{
	public class GameDataModel
	{
		public string PlayerName { get; private set; }
		public char[] array {set; get; }

		public GameDataModel()
		{
			PlayerName = "";
			Random random = new Random();
			array = new char[9];
			for (int i = 0; i < 9; i++)
			{
				int value = random.Next(0, 3);
				array[i] = value == 0 ? ' ' : value == 1 ? 'X' : 'O';
			}
		}

		public GameDataModel(string playerName) : this()
		{
			PlayerName = playerName;
		}
	}
}
