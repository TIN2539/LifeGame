using System;
using System.Threading;

namespace LifeGame
{
	internal class Game
	{
		private Field field;
		private int delay = 300;
		private Generation generation;
		private Momento momento;

		public Game(int column, int row, int delay)
		{
			field = new Field(column, row);
			generation = new Generation();
			momento = new Momento();
			CurrentX = 1;
			CurrentY = 3;
			this.delay = delay;
		}

		public static int CurrentX { get; set; }
		public static int CurrentY { get; set; }

		public Field GetField()
		{
			return field;
		}
		public bool KeyPressed(ConsoleKey key)
		{
			if (key == ConsoleKey.RightArrow && CurrentX < field.GetRow())
			{
				Console.SetCursorPosition(CurrentX += 1, CurrentY);
			}
			else if (key == ConsoleKey.LeftArrow && CurrentX > 1)
			{
				Console.SetCursorPosition(CurrentX -= 1, CurrentY);
			}
			else if (key == ConsoleKey.DownArrow && CurrentY < field.GetColumn() + 2)
			{
				Console.SetCursorPosition(CurrentX, CurrentY += 1);
			}
			else if (key == ConsoleKey.UpArrow && CurrentY > 3)
			{
				Console.SetCursorPosition(CurrentX, CurrentY -= 1);
			}
			else if (key == ConsoleKey.Enter)
			{
				field.GetCells()[CurrentY - 3, CurrentX - 1].ChangeStatus();
			}
			else if (key == ConsoleKey.Spacebar)
			{
				do
				{
					Play();
				} while (!GameOver());
				return false;
			}
			return true;
		}
		private bool GameOver()
		{
			if(AllDie() || momento.CompareLastVariant(field.GetCells()) || momento.CompareCycle(field.GetCells()))
			{
				return true;
			}
			return false;
		}
		private bool AllDie()
		{
			for (int i = 0; i < field.GetColumn(); i++)
			{
				for (int j = 0; j < field.GetRow(); j++)
				{
					if (field.GetCells()[i, j].GetStatus())
					{
						return false;
					}
				}
			}
			return true;
		}
		private void Play()
		{
			generation.Paint();
			
			momento.Add(field.GetCells());
			Cell[,] tempCells = new Cell[field.GetColumn(), field.GetRow()];
			for (int i = 0; i < field.GetColumn(); i++)
			{
				for (int j = 0; j < field.GetRow(); j++)
				{
					tempCells[i, j] = new Cell(field.GetCells()[i, j].GetStatus());
				}
			}
			for (int i = 0; i < field.GetColumn(); i++)
			{
				for (int j = 0; j < field.GetRow(); j++)
				{
					if (field.GetCells()[i, j].GetStatus() && Rulles(i, j) != 3 && Rulles(i, j) != 2)
					{
						tempCells[i, j].ChangeStatus();
					}
					else if (!field.GetCells()[i, j].GetStatus() && Rulles(i, j) == 3)
					{
						tempCells[i, j].ChangeStatus();
					}
				}
			}
			field.SetCell(tempCells);
			generation.Update();
			generation.Paint();
			field.Update();
			Thread.Sleep(delay);
		}
		private int Rulles(int y, int x)
		{
			int check = 0;
			if (y == 0 && x == 0)
			{
				check = CheckLeftTop(y, x);
			}
			else if (y == 0 && x == field.GetRow() - 1)
			{
				check = CheckRightTop(y, x);
			}
			else if (y == field.GetColumn() - 1 && x == 0)
			{
				check = CheckLeftBottom(y, x);
			}
			else if (y == field.GetColumn() - 1 && x == field.GetRow() - 1)
			{
				check = CheckRightBottom(y, x);
			}
			else if (y == 0)
			{
				check = CheckTop(y, x);
			}
			else if (x == 0)
			{
				check = CheckLeft(y, x);
			}
			else if (y == field.GetColumn() - 1)
			{
				check = CheckBottom(y, x);
			}
			else if (x == field.GetRow() - 1)
			{
				check = CheckRight(y, x);
			}
			else
			{
				check = CheckLeftBottom(y, x) + CheckRightTop(y, x);
				if (field.GetCells()[y - 1, x - 1].GetStatus())
				{
					check++;
				}
				if (field.GetCells()[y + 1, x + 1].GetStatus())
				{
					check++;
				}
			}
			return check;
		}
		private int CheckBottom(int y, int x)
		{
			int check = 0;
			if (field.GetCells()[y, x + 1].GetStatus())
			{
				check++;
			}
			if (field.GetCells()[y, x - 1].GetStatus())
			{
				check++;
			}
			if (field.GetCells()[y - 1, x - 1].GetStatus())
			{
				check++;
			}
			if (field.GetCells()[y - 1, x + 1].GetStatus())
			{
				check++;
			}
			if (field.GetCells()[y - 1, x].GetStatus())
			{
				check++;
			}
			return check;
		}
		private int CheckRight(int y, int x)
		{
			int check = 0;
			if (field.GetCells()[y + 1, x].GetStatus())
			{
				check++;
			}
			if (field.GetCells()[y - 1, x].GetStatus())
			{
				check++;
			}
			if (field.GetCells()[y - 1, x - 1].GetStatus())
			{
				check++;
			}
			if (field.GetCells()[y + 1, x - 1].GetStatus())
			{
				check++;
			}
			if (field.GetCells()[y, x - 1].GetStatus())
			{
				check++;
			}
			return check;
		}
		private int CheckTop(int y, int x)
		{
			int check = 0;
			if (field.GetCells()[y, x + 1].GetStatus())
			{
				check++;
			}
			if (field.GetCells()[y, x - 1].GetStatus())
			{
				check++;
			}
			if (field.GetCells()[y + 1, x + 1].GetStatus())
			{
				check++;
			}
			if (field.GetCells()[y + 1, x - 1].GetStatus())
			{
				check++;
			}
			if (field.GetCells()[y + 1, x].GetStatus())
			{
				check++;
			}
			return check;
		}
		private int CheckLeft(int y, int x)
		{
			int check = 0;
			if (field.GetCells()[y + 1, x].GetStatus())
			{
				check++;
			}
			if (field.GetCells()[y - 1, x].GetStatus())
			{
				check++;
			}
			if (field.GetCells()[y, x + 1].GetStatus())
			{
				check++;
			}
			if (field.GetCells()[y + 1, x + 1].GetStatus())
			{
				check++;
			}
			if (field.GetCells()[y - 1, x + 1].GetStatus())
			{
				check++;
			}
			return check;
		}
		private int CheckRightBottom(int y, int x)
		{
			int check = 0;
			if (field.GetCells()[y - 1, x].GetStatus())
			{
				check++;
			}
			if (field.GetCells()[y - 1, x - 1].GetStatus())
			{
				check++;
			}
			if (field.GetCells()[y, x - 1].GetStatus())
			{
				check++;
			}
			return check;
		}
		private int CheckRightTop(int y, int x)
		{
			int check = 0;
			if (field.GetCells()[y + 1, x].GetStatus())
			{
				check++;
			}
			if (field.GetCells()[y + 1, x - 1].GetStatus())
			{
				check++;
			}
			if (field.GetCells()[y, x - 1].GetStatus())
			{
				check++;
			}
			return check;
		}
		private int CheckLeftBottom(int y, int x)
		{
			int check = 0;
			if (field.GetCells()[y, x + 1].GetStatus())
			{
				check++;
			}
			if (field.GetCells()[y - 1, x].GetStatus())
			{
				check++;
			}
			if (field.GetCells()[y - 1, x + 1].GetStatus())
			{
				check++;
			}
			return check;
		}
		private int CheckLeftTop(int y, int x)
		{
			int check = 0;
			if (field.GetCells()[y + 1, x].GetStatus())
			{
				check++;
			}
			if (field.GetCells()[y, x + 1].GetStatus())
			{
				check++;
			}
			if (field.GetCells()[y + 1, x + 1].GetStatus())
			{
				check++;
			}
			return check;
		}
		public void Update()
		{
			generation.Paint();
			field.Update();
			Console.SetCursorPosition(CurrentX, CurrentY);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write('X');
			Console.ResetColor();
		}
	}
}