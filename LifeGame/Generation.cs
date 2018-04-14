using System;

namespace LifeGame
{
	internal class Generation
	{
		private int generationNumber = 0;

		public void Paint()
		{
			Console.SetCursorPosition(0, 0);
			Console.Write("Generation: ");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine(generationNumber);
			Console.ResetColor();
		}

		public void Update()
		{
			generationNumber++;
		}
	}
}