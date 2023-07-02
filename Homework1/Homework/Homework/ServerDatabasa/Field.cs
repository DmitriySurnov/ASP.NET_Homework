namespace Homework.ServerDatabasa
{
	public class Field
	{
		public char[] FieldGame { get; private set; }

		public bool IsX { get; private set; }

		public int NumberFilledCells { get; private set; }

		public Field()
		{
			FieldGame = new char[9];
			IsX = true;
		}
		public void FillTheField()
		{
			NumberFilledCells = 0;
			Random random = new Random();
			for (int i = 0; i < FieldGame.Length; i++)
			{
				int value = random.Next(0, 3);
				FieldGame[i] = value == 0 ? ' ' : value == 1 ? 'X' : 'O';
				if (value == 1 || value == 2)
					NumberFilledCells++;
			}
		}
		public void MakeAMove(int id)
		{
			if (FieldGame[id] != ' ')
				return;
			FieldGame[id] = IsX ? 'X' : 'O';
			NumberFilledCells++;
		}

	}
}
