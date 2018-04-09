using System;
using System.Linq;

namespace LifeGame
{
	internal class Program
	{
		internal static void Main(string[] args)
		{
			int column = 10;
			int row = 40;
			int delay = 300;
			Game game = null;
			if (args.Length == 0)
			{
				game = new Game(column, row, delay);
			}
			else
			{
				char[] parameter = new char[3];
				for (int i = 0; i < args.Length; i++)
				{
					args[i].ToLower();
					parameter[i] = args[i].First();
				}
				bool correctParameters = true;
				if (!parameter.Contains('w') && parameter.Contains('h'))
				{
					correctParameters = false;
					Console.Write("Invalid arguments: ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Width of the Universe was not specified.");
					Console.ResetColor();
				}
				else if (parameter.Contains('w') && !parameter.Contains('h'))
				{
					correctParameters = false;
					Console.Write("Invalid arguments: ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Height of the Universe was not specified.");
					Console.ResetColor();
				}
				if (correctParameters)
				{
					for (int i = 0; i < args.Length; i++)
					{
						if (args[i].First() == 'w')
						{
							args[i] = args[i].Substring(1);
							row = int.Parse(args[i]);
						}
						if (args[i].First() == 'h')
						{
							args[i] = args[i].Substring(1);
							column = int.Parse(args[i]);
						}
						if (args[i].First() == 's')
						{
							args[i] = args[i].Substring(1);
							delay = int.Parse(args[i]);
						}
					}
					game = new Game(column, row, delay);
				}
			}

			if (game != null)
			{
				game.GetField().Paint();

				do
				{
					game.Update();
				} while (game.KeyPressed(Console.ReadKey(true).Key));

				Console.SetCursorPosition(1, column + 5);
				//Console.WriteLine("GAME OVER!");
			}
		}
	}
}