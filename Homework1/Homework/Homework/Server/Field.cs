namespace Homework.Server
{
	public class Field
    {
        public string[] FieldGame { get; private set; }

        public bool IsX { get; set; }

        public int NumberFilledCells { get; private set; }

		public int Winner { get; private set; }

		public Field(string field)
		{
			FieldGame = field.Split(',');
            NumberFilledCells = field.Replace(" ", "").Replace(",","").Length;
			IsX = true;
		}

		public Field(string field, bool IsCurrentPlayerMove)
		{
			FieldGame = field.Split(',');
			NumberFilledCells = field.Replace(" ", "").Replace(",", "").Length;
			IsX = IsCurrentPlayerMove;
		}

		public Field()
        {
            FieldGame = new string[9];
            IsX = true;
        }
        public void FillTheField()
        {
            NumberFilledCells = 0;
            Random random = new Random();
            for (int i = 0; i < FieldGame.Length; i++)
            {
                int value = random.Next(0, 3);
                FieldGame[i] = value == 0 ? " " : value == 1 ? "X" : "O";

				if (value == 1 || value == 2)
                    NumberFilledCells++;
            }
        }
        public void MakeAMove(int id)
        {
            if (FieldGame[id] != " ")
                return;
            FieldGame[id] = IsX ? "X" : "O";
            NumberFilledCells++;
			DeterminingWinner();

		}

		public void DeterminingWinner()
		{
			Winner = -1;
			int[,] combinations = {
				{ 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 },
				{ 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 },
				{ 0, 4, 8 }, { 2, 4, 6 }};
			for (int i = 0; i < 8; i++)
			{
				if (FieldGame[combinations[i, 0]] == FieldGame[combinations[i, 1]] &&
					FieldGame[combinations[i, 0]] == FieldGame[combinations[i, 2]] && 
					FieldGame[combinations[i, 0]] != " ")
				{
					Winner = FieldGame[combinations[i, 0]] == "X" ? 1 : 2;
					break;
				}
			}
			if (NumberFilledCells == 9 && Winner == -1)
				Winner = 0;
		}

	}
}
