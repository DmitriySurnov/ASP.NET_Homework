namespace Homework.Models
{
	public class ArrayDataModel
	{
		public char[] array {private set; get; }

		public ArrayDataModel()
		{
			Random random = new Random();
			array = new char[9];
			for (int i = 0; i < 9; i++)
			{
				int value = random.Next(0, 3);
				array[i] = value == 0 ? ' ' : value == 1 ? 'X' : 'O';
			}
		}
	}
}
