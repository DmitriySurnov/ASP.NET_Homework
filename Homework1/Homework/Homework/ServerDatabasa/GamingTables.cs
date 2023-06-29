namespace Homework.ServerDatabasa
{
    public static class GamingTables
    {
        public static Game[] GameTable;

        static GamingTables()
        {
            GameTable = new Game[3];
            for (int i = 0; i < 3; i++)
            {
                GameTable[i] = new Game();
            }
        }


    }
}
