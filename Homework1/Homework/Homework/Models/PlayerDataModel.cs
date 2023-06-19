namespace Homework.Models
{
	public class PlayerDataModel
	{
		public string Name { get; set; }

		public Guid Id { get; set; }

		public PlayerDataModel() { 
			Name = "Unknown";
			Id = Guid.NewGuid();
		}

		public PlayerDataModel(string name) { 
			Name = name;
			Id = Guid.NewGuid();
		}
	}
}
