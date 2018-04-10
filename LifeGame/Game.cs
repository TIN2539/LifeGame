using System;
using System.Threading;

namespace LifeGame
{
	internal class Game
	{
		private Field field;
		private int delay;
		private Generation generation;
		private Memento memento;
		private const char characterForCursor = 'X';
		private int currentX;
		private int currentY;

		public Game(int row, int column, int delay)
		{
			field = new Field(row, column);
			generation = new Generation();
			memento = new Memento();
			currentX = field.GetLeftMost();
			currentY = field.GetTopMost();
			this.delay = delay;
		}

		public Field GetField()
		{
			return field;
		}

		public bool IsKeyPressed(ConsoleKey key)
		{
			bool isSpacebarPressed = false;
			if (key == ConsoleKey.RightArrow && currentX < field.GetColumn())
			{
				Console.SetCursorPosition(IncrementX(), currentY);
			}
			else if (key == ConsoleKey.LeftArrow && currentX > field.GetLeftMost())
			{
				Console.SetCursorPosition(DecrementX(), currentY);
			}
			else if (key == ConsoleKey.DownArrow && currentY <= field.GetRow())
			{
				Console.SetCursorPosition(currentX, IncrementY());
			}
			else if (key == ConsoleKey.UpArrow && currentY > field.GetTopMost())
			{
				Console.SetCursorPosition(currentX, DecrementY());
			}
			else if (key == ConsoleKey.Enter)
			{
				field.GetCells()[currentY - field.GetTopMost(), currentX - field.GetLeftMost()].ChangeStatus();
			}
			else if (key == ConsoleKey.Spacebar)
			{
				do
				{
					Play();
				} while (!IsGameOver());
				isSpacebarPressed = true;
			}
			return isSpacebarPressed;
		}

		private int IncrementX()
		{
			return ++currentX;
		}

		private int DecrementX()
		{
			return --currentX;
		}

		private int IncrementY()
		{
			return ++currentY;
		}

		private int DecrementY()
		{
			return --currentY;
		}

		private bool IsGameOver()
		{
			bool isGameOver = false;
			if (AreAllDie() || memento.IsIdenticalCells(field.GetCells()) || memento.HasSameCells(field.GetCells()))
			{
				isGameOver = true;
			}
			return isGameOver;
		}

		private bool AreAllDie()
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

		private void Play()
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
					if (field.GetCells()[i, j].GetIsAlive() && Rules(i, j) != 3 && Rules(i, j) != 2)     //Rules(i, j) != 3 && Rules(i, j) != 2  -- вокруг живой ячейки нету 3 или 2 живых ячеек
					{
						cellForMakeChanges[i, j].ChangeStatus();
					}
					else if (!field.GetCells()[i, j].GetIsAlive() && Rules(i, j) == 3)       //Rules(i, j) == 3 -- вокруг мертвой ячейки ровно 3 живие ячейки
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

		private int Rules(int x, int y)
		{
			int countOfAliveCell = 0;
			for (int i = -1; i <= 1; i++)		//-1...1 -- для перемещения от текущей ячейки вокруг нее
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