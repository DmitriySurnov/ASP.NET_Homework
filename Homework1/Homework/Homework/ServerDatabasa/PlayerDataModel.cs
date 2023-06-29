namespace Homework.ServerDatabasa
{
    [Serializable]
    public class PlayerDataModel
    {
        public string Name { get; set; }

        public Guid Id { get; set; }

        public int Status { get; set; }

        public int NumberTable { get; set; }

        public PlayerDataModel()
        {
            Name = "Unknown";
            Id = Guid.NewGuid();
            Status = -1;
            NumberTable = -1;
        }

        public PlayerDataModel(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
            Status = 0;
            NumberTable = -1;
        }
    }
}
