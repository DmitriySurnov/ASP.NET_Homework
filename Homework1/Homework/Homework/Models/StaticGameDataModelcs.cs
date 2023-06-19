namespace Homework.Models
{
	public static class StaticGameDataModelcs
	{
		public static GameDataModel GameDataModel { get; set; } = new GameDataModel();

		public static object ListPlayerLockObject = new object();
	}
}
