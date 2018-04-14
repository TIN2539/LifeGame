using System;
using System.Linq;

namespace LifeGame
{
	internal class Program
	{
		internal static void Main(string[] parametersFromConsole)
		{
			// Default parameters
			int row = 10;
			int column = 40;
			int delay = 300;
			Game game = null;
			if (parametersFromConsole.Length == 0)
			{
				game = new Game(row, column, delay);
			}
			else
			{
				int resultOfParse;
				char[] inputParameters = new char[parametersFromConsole.Length];
				for (int i = 0; i < parametersFromConsole.Length; i++)
				{
					if (int.TryParse(parametersFromConsole[i].Substring(1), out resultOfParse))
					{
						inputParameters[i] = parametersFromConsole[i].First();
					}
				}
				bool correctParameters = true;
				if (!inputParameters.Contains((char)Parameters.Width) && inputParameters.Contains((char)Parameters.Heigth))
				{
					correctParameters = false;
					Console.Write("Invalid arguments: ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Width of the Universe was not specified.");
					Console.ResetColor();
				}
				else if (inputParameters.Contains((char)Parameters.Width) && !inputParameters.Contains((char)Parameters.Heigth))
				{
					correctParameters = false;
					Console.Write("Invalid arguments: ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Height of the Universe was not specified.");
					Console.ResetColor();
				}
				if (correctParameters)
				{
					char symbolOfInputParametr;
					for (int i = 0; i < parametersFromConsole.Length; i++)
					{
						symbolOfInputParametr = parametersFromConsole[i].First();
						parametersFromConsole[i] = parametersFromConsole[i].Substring(1);
						switch (symbolOfInputParametr)
						{
							case (char)Parameters.Width:
								if (int.TryParse(parametersFromConsole[i], out resultOfParse))
								{
									row = resultOfParse;
								}
								break;
							case (char)Parameters.Heigth:
								if (int.TryParse(parametersFromConsole[i], out resultOfParse))
								{
									column = resultOfParse;
								}
								break;
							case (char)Parameters.Delay:
								if (int.TryParse(parametersFromConsole[i], out resultOfParse))
								{
									delay = resultOfParse;
								}
								break;
						}
					}
					game = new Game(row, column, delay);
				}
			}

			if (game != null)
			{
				game.GetField().Paint();

				do
				{
					game.Update();
				} while (!game.IsKeyPressed(Console.ReadKey(true).Key));

				Console.SetCursorPosition(game.GetField().GetLeftMost(), game.GetField().GetHeight() + game.GetField().GetTopMost());
			}
		}
	}
}