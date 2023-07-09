namespace Homework.Server
{
    public class Game
    {
        public Guid PlayerXGuid { get; set; }

        public Guid PlayerOGuid { get; set; }

        public string Field { get; set; }

        public Guid WhichPlayerWalkingGuid { get; set; } //какой игрок ходит

        public object ChangesLockObject { get; private set; }


		public Game()
        {
            PlayerXGuid = Guid.Empty;
            PlayerOGuid = Guid.Empty;
            Field = string.Empty;
            WhichPlayerWalkingGuid = Guid.Empty;
			ChangesLockObject = new object();
		}
    }
}
