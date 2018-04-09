using System;
using System.Linq;

namespace LifeGame
{
	internal class Program
	{
		internal static void Main(string[] args)
		{
			//Default parameters
			int row = 10;		
			int column = 40;
			int delay = 300;
			char[] parameters = { 'w', 'h', 's' };      //w - width, h - height, s - delay
			Game game = null;
			if (args.Length == 0)
			{
				game = new Game(row, column, delay);
			}
			else
			{
				char[] parameter = new char[args.Length];
				for (int i = 0; i < args.Length; i++)
				{
					args[i].ToLower();
					parameter[i] = args[i].First();
				}
				bool correctParameters = true;
				if (!parameter.Contains(parameters[0]) && parameter.Contains(parameters[1]))
				{
					correctParameters = false;
					Console.Write("Invalid arguments: ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Width of the Universe was not specified.");
					Console.ResetColor();
				}
				else if (parameter.Contains(parameters[0]) && !parameter.Contains(parameters[1]))
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
						if (args[i].First() == parameters[0])
						{
							args[i] = args[i].Substring(1);
							row = int.Parse(args[i]);
						}
						if (args[i].First() == parameters[1])
						{
							args[i] = args[i].Substring(1);
							column = int.Parse(args[i]);
						}
						if (args[i].First() == parameters[2])
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
				} while (game.IsKeyPressed(Console.ReadKey(true).Key));

				Console.SetCursorPosition(game.GetField().GetLeftMost(), game.GetField().GetHeight() + game.GetField().GetTopMost());
				//Console.WriteLine("GAME OVER!");
			}
		}
	}
}