namespace Homework.Server
{
    public class Player
    {
        public string Name { get; set; }

        public Guid NumberTable { get; set; }

        public Player(string name)
        {
            Name = name;
            NumberTable = Guid.Empty;
        }

        public Player()
        {
            Name = "Unknown";
            NumberTable = Guid.Empty;
        }
    }
}
