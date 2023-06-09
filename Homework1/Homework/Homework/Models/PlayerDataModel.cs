namespace Homework.Models
{
	public class PlayerDataModel
	{
		public string Name { get; set; }

		public PlayerDataModel() { 
			Name = "Unknown";
		}

		public PlayerDataModel(string name) { 
			Name = name;
		}
	}
}
