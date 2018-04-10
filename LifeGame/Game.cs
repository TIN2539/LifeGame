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
				Console.SetCursorPosition(currentX += 1, currentY);
			}
			else if (key == ConsoleKey.LeftArrow && currentX > field.GetLeftMost())
			{
				Console.SetCursorPosition(currentX -= 1, currentY);
			}
			else if (key == ConsoleKey.DownArrow && currentY < field.GetHeight())
			{
				Console.SetCursorPosition(currentX, currentY += 1);
			}
			else if (key == ConsoleKey.UpArrow && currentY > field.GetTopMost())
			{
				Console.SetCursorPosition(currentX, currentY -= 1);
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
			if (y == 0 && x == 0)
			{
				countOfAliveCell = CheckLeftTop(x, y);
			}
			else if (y == field.GetColumn() - 1 && x == 0)
			{
				countOfAliveCell = CheckRightTop(x, y);
			}
			else if (y == 0 && x == field.GetRow() - 1)
			{
				countOfAliveCell = CheckLeftBottom(x, y);
			}
			else if (y == field.GetColumn() - 1 && x == field.GetRow() - 1)
			{
				countOfAliveCell = CheckRightBottom(x, y);
			}
			else if (x == 0)
			{
				countOfAliveCell = CheckTop(x, y);
			}
			else if (y == 0)
			{
				countOfAliveCell = CheckLeft(x, y);
			}
			else if (x == field.GetRow() - 1)
			{
				countOfAliveCell = CheckBottom(x, y);
			}
			else if (y == field.GetColumn() - 1)
			{
				countOfAliveCell = CheckRight(x, y);
			}
			else
			{
				countOfAliveCell = CheckLeftBottom(x, y) + CheckRightTop(x, y);
				if (field.GetCells()[x - 1, y - 1].GetIsAlive())
				{
					countOfAliveCell++;
				}
				if (field.GetCells()[x + 1, y + 1].GetIsAlive())
				{
					countOfAliveCell++;
				}
			}
			return countOfAliveCell;
		}

		private int CheckBottom(int x, int y)
		{
			int countOfAliveCell = 0;
			if (field.GetCells()[x, y + 1].GetIsAlive())
			{
				countOfAliveCell++;
			}
			if (field.GetCells()[x, y - 1].GetIsAlive())
			{
				countOfAliveCell++;
			}
			if (field.GetCells()[x - 1, y - 1].GetIsAlive())
			{
				countOfAliveCell++;
			}
			if (field.GetCells()[x - 1, y + 1].GetIsAlive())
			{
				countOfAliveCell++;
			}
			if (field.GetCells()[x - 1, y].GetIsAlive())
			{
				countOfAliveCell++;
			}
			return countOfAliveCell;
		}

		private int CheckRight(int x, int y)
		{
			int countOfAliveCell = 0;
			if (field.GetCells()[x + 1, y].GetIsAlive())
			{
				countOfAliveCell++;
			}
			if (field.GetCells()[x - 1, y].GetIsAlive())
			{
				countOfAliveCell++;
			}
			if (field.GetCells()[x - 1, y - 1].GetIsAlive())
			{
				countOfAliveCell++;
			}
			if (field.GetCells()[x + 1, y - 1].GetIsAlive())
			{
				countOfAliveCell++;
			}
			if (field.GetCells()[x, y - 1].GetIsAlive())
			{
				countOfAliveCell++;
			}
			return countOfAliveCell;
		}

		private int CheckTop(int x, int y)
		{
			int countOfAliveCell = 0;
			if (field.GetCells()[x, y + 1].GetIsAlive())
			{
				countOfAliveCell++;
			}
			if (field.GetCells()[x, y - 1].GetIsAlive())
			{
				countOfAliveCell++;
			}
			if (field.GetCells()[x + 1, y + 1].GetIsAlive())
			{
				countOfAliveCell++;
			}
			if (field.GetCells()[x + 1, y - 1].GetIsAlive())
			{
				countOfAliveCell++;
			}
			if (field.GetCells()[x + 1, y].GetIsAlive())
			{
				countOfAliveCell++;
			}
			return countOfAliveCell;
		}

		private int CheckLeft(int x, int y)
		{
			int countOfAliveCell = 0;
			if (field.GetCells()[x + 1, y].GetIsAlive())
			{
				countOfAliveCell++;
			}
			if (field.GetCells()[x - 1, y].GetIsAlive())
			{
				countOfAliveCell++;
			}
			if (field.GetCells()[x, y + 1].GetIsAlive())
			{
				countOfAliveCell++;
			}
			if (field.GetCells()[x + 1, y + 1].GetIsAlive())
			{
				countOfAliveCell++;
			}
			if (field.GetCells()[x - 1, y + 1].GetIsAlive())
			{
				countOfAliveCell++;
			}
			return countOfAliveCell;
		}

		private int CheckRightBottom(int x, int y)
		{
			int countOfAliveCell = 0;
			if (field.GetCells()[x - 1, y].GetIsAlive())
			{
				countOfAliveCell++;
			}
			if (field.GetCells()[x - 1, y - 1].GetIsAlive())
			{
				countOfAliveCell++;
			}
			if (field.GetCells()[x, y - 1].GetIsAlive())
			{
				countOfAliveCell++;
			}
			return countOfAliveCell;
		}

		private int CheckRightTop(int x, int y)
		{
			int countOfAliveCell = 0;
			if (field.GetCells()[x + 1, y].GetIsAlive())
			{
				countOfAliveCell++;
			}
			if (field.GetCells()[x + 1, y - 1].GetIsAlive())
			{
				countOfAliveCell++;
			}
			if (field.GetCells()[x, y - 1].GetIsAlive())
			{
				countOfAliveCell++;
			}
			return countOfAliveCell;
		}

		private int CheckLeftBottom(int x, int y)
		{
			int countOfAliveCell = 0;
			if (field.GetCells()[x, y + 1].GetIsAlive())
			{
				countOfAliveCell++;
			}
			if (field.GetCells()[x - 1, y].GetIsAlive())
			{
				countOfAliveCell++;
			}
			if (field.GetCells()[x - 1, y + 1].GetIsAlive())
			{
				countOfAliveCell++;
			}
			return countOfAliveCell;
		}

		private int CheckLeftTop(int x, int y)
		{
			int countOfAliveCell = 0;
			if (field.GetCells()[x + 1, y].GetIsAlive())
			{
				countOfAliveCell++;
			}
			if (field.GetCells()[x, y + 1].GetIsAlive())
			{
				countOfAliveCell++;
			}
			if (field.GetCells()[x + 1, y + 1].GetIsAlive())
			{
				countOfAliveCell++;
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