using System;
using System.Threading;

namespace LifeGame
{
	internal class Game
	{
		private Field field;
		private int delay;
		private Generation generation;
		private Memento momento;
		readonly static char characterForCursor = 'X';


		public Game(int column, int row, int delay)
		{
			field = new Field(column, row);
			generation = new Generation();
			momento = new Memento();
			CurrentX = 1;	//В начале игры позиция курсора - левый верхний угол игрового поля. Х = 1, Y = 3
			CurrentY = 3;	//В начале игры позиция курсора - левый верхний угол игрового поля. Х = 1, Y = 3
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
			else if (key == ConsoleKey.LeftArrow && CurrentX > field.GetLeftMost())		
			{
				Console.SetCursorPosition(CurrentX -= 1, CurrentY);
			}
			else if (key == ConsoleKey.DownArrow && CurrentY < field.GetWidth())
			{
				Console.SetCursorPosition(CurrentX, CurrentY += 1);
			}
			else if (key == ConsoleKey.UpArrow && CurrentY > field.GetTopMost())				
			{
				Console.SetCursorPosition(CurrentX, CurrentY -= 1);
			}
			else if (key == ConsoleKey.Enter)
			{
				field.GetCells()[CurrentY - field.GetTopMost(), CurrentX - field.GetLeftMost()].ChangeStatus();
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
					if (field.GetCells()[i, j].GetStatus() && Rules(i, j) != 3 && Rules(i, j) != 2)     //Rules(i, j) != 3 && Rules(i, j) != 2  -- вокруг живой ячейки нету 3 или 2 живых ячеек
					{
						tempCells[i, j].ChangeStatus();
					}
					else if (!field.GetCells()[i, j].GetStatus() && Rules(i, j) == 3)       //Rules(i, j) == 3 -- вокруг мертвой ячейки ровно 3 живие ячейки
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

		private int Rules(int y, int x)
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
			Console.Write(characterForCursor);
			Console.ResetColor();
		}
	}
}