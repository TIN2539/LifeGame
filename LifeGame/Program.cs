using System;
using System.Linq;

namespace LifeGame
{
	internal class Program
	{
		internal static void Main(string[] parametersFromConsole)
		{
			Game game = null;
			Parameters parameters = new Parameters();
			if (parametersFromConsole.Length != 0)
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
				if (!inputParameters.Contains((char)EnumParameters.Width) && inputParameters.Contains((char)EnumParameters.Heigth))
				{
					correctParameters = false;
					Console.Write("Invalid arguments: ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Width of the Universe was not specified.");
					Console.ResetColor();
				}
				else if (inputParameters.Contains((char)EnumParameters.Width) && !inputParameters.Contains((char)EnumParameters.Heigth))
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
						if (int.TryParse(parametersFromConsole[i], out resultOfParse))
						{
							switch (symbolOfInputParametr)
							{
								case (char)EnumParameters.Width:
									parameters.Row = resultOfParse;
									break;
								case (char)EnumParameters.Heigth:
									parameters.Column = resultOfParse;
									break;
								case (char)EnumParameters.Delay:
									parameters.Delay = resultOfParse;
									break;
								default:
									break;
							}
						}

					}
				}
				else
				{
					Environment.Exit(0);
				}
			}
			game = new Game(parameters);
			game.GetField().Paint();
			do
			{
				game.Update();
			} while (!game.IsKeyPressed(Console.ReadKey(true).Key));
			Console.SetCursorPosition(game.GetField().GetLeftMost(), game.GetField().GetHeight() + game.GetField().GetTopMost());
		}
	}
}