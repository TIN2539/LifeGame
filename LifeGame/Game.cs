using System;
using System.Collections.Generic;
using System.Threading;

namespace LifeGame
{
	internal class Game
	{
		private const char characterForCursor = 'X';

		private List<ICommand> commands;
		private int currentX;
		private int currentY;
		private int delay;
		private Field field;
		private Generation generation;
		private Memento memento;

		public Game(Parameters parameters)
		{
			field = new Field(parameters.Row, parameters.Column);
			generation = new Generation();
			memento = new Memento();
			commands = new List<ICommand>
			{
				new RightCommand(this),
				new LeftCommand(this),
				new UpCommand(this),
				new DownCommand(this),
				new EnterCommand(this),
				new SpacebarCommand(this)
			};
			currentX = field.GetLeftMost();
			currentY = field.GetTopMost();
			this.delay = parameters.Delay;
		}

		public bool AreAllDie()
		{
			bool areAllDie = true;
			for (int i = 0; i < field.GetRow(); i++)
			{
				for (int j = 0; j < field.GetColumn(); j++)
				{
					if (field.GetCells()[i, j].GetIsAlive())
					{
						areAllDie = false;
						break;
					}
				}
				if (!areAllDie)
				{
					break;
				}
			}
			return areAllDie;
		}

		public int DecrementX()
		{
			return --currentX;
		}

		public int DecrementY()
		{
			return --currentY;
		}

		public int GetCurrentX()
		{
			return currentX;
		}

		public int GetCurrentY()
		{
			return currentY;
		}

		public Field GetField()
		{
			return field;
		}

		public int IncrementX()
		{
			return ++currentX;
		}

		public int IncrementY()
		{
			return ++currentY;
		}

		public bool IsGameOver()
		{
			bool isGameOver = false;
			if (AreAllDie() || memento.IsIdenticalCells(field.GetCells()) || memento.HasSameCells(field.GetCells()))
			{
				isGameOver = true;
			}
			return isGameOver;
		}

		public bool IsKeyPressed(ConsoleKey key)
		{
			bool isSpacebarPressed = false;
			foreach (ICommand command in commands)
			{
				if (command.CanExecute(key))
				{
					isSpacebarPressed = command.Execute();
					break;
				}
			}
			return isSpacebarPressed;
		}

		public void Play()
		{
			generation.Paint();
			memento.Add(field.GetCells());
			Cell[,] cellForMakeChanges = new Cell[field.GetRow(), field.GetColumn()];
			for (int i = 0; i < field.GetRow(); i++)
			{
				for (int j = 0; j < field.GetColumn(); j++)
				{
					cellForMakeChanges[i, j] = new Cell(field.GetCells()[i, j].GetIsAlive());
				}
			}
			for (int i = 0; i < field.GetRow(); i++)
			{
				for (int j = 0; j < field.GetColumn(); j++)
				{
					if (field.GetCells()[i, j].GetIsAlive() && Rules(i, j) != 3 && Rules(i, j) != 2) // Rules(i, j) != 3 && Rules(i, j) != 2  -- вокруг живой ячейки нету 3 или 2 живых ячеек
					{
						cellForMakeChanges[i, j].ChangeStatus();
					}
					else if (!field.GetCells()[i, j].GetIsAlive() && Rules(i, j) == 3) // Rules(i, j) == 3 -- вокруг мертвой ячейки ровно 3 живие ячейки
					{
						cellForMakeChanges[i, j].ChangeStatus();
					}
				}
			}
			field.SetCell(cellForMakeChanges);
			generation.Update();
			generation.Paint();
			field.Update();
			Thread.Sleep(delay);
		}

		public int Rules(int x, int y)
		{
			int countOfAliveCell = 0;
			for (int i = -1; i <= 1; i++) // -1...1 -- для перемещения от текущей ячейки вокруг нее
			{
				for (int j = -1; j <= 1; j++)
				{
					if (x + i >= 0 && x + i < field.GetCells().GetLength(0) && y + j >= 0 && y + j < field.GetCells().GetLength(1) && field.GetCells()[x + i, y + j].GetIsAlive())
					{
						countOfAliveCell++;
					}

				}
			}
			if (field.GetCells()[x, y].GetIsAlive())
			{
				countOfAliveCell--;
			}
			return countOfAliveCell;
		}

		public void Update()
		{
			generation.Paint();
			field.Update();
			Console.SetCursorPosition(currentX, currentY);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write(characterForCursor);
			Console.ResetColor();
		}
	}
}