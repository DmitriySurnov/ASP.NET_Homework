namespace Homework.ServerDatabasa
{
    public class Game
    {
        public List<PlayerDataModel> Players { get; set; }

        public Field Field { set; get; }

        public bool IsMove { get; private set; }

        public int Winner { get; private set; }

        public PlayerDataModel MotionPlayer { set; get; }

        public object ChangesLockObject { get; private set; }

        public Game()
        {
            ChangesLockObject = new object();
            Players = new List<PlayerDataModel>();
            Winner = -1;
            MotionPlayer = new PlayerDataModel();
        }

        public void СreateTheField()
        {
			Field = new Field();
            FillTheField();
		}

		public void MakeAMove(int id)
        {
			Field.MakeAMove(id);
			IsMove = Field.NumberFilledCells != 9;
			DeterminingWinner();
            if (Winner == -1)
                Field.IsX = !Field.IsX;
		}

		public void FillTheField()
        {
            Field.FillTheField();
            IsMove = Field.NumberFilledCells != 9;
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
                if (Field.FieldGame[combinations[i, 0]] == Field.FieldGame[combinations[i, 1]] &&
                    Field.FieldGame[combinations[i, 0]] == Field.FieldGame[combinations[i, 2]] && Field.FieldGame[combinations[i, 0]] != ' ')
                {
                    IsMove = false;
                    Winner = Field.FieldGame[combinations[i, 0]] == 'X' ? 1 : 2;
                    break;
                }
            }
            if (!IsMove && Winner == -1)
                Winner = 0;
        }
    }
}
