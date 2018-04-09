using System;

namespace LifeGame
{
	internal class Field
	{
		private int column;
		private int row;
		private int width;
		private int height;
		readonly static int leftMost = 1;
		readonly static int topMost = 3;
		readonly static char characterForFrame = '+';
		readonly static char characterForDeadCell = ' ';
		readonly static char characterForAliveCell = 'O';
		private Cell[,] cells;

		public Field(int row, int column)
		{
			this.column = column;
			this.row = row;
			width = column + 2;		//+2 - поправка с учетом наличия нижней и верхней границ поля
			height = row + 2;       //+2 - поправка с учетом наличия левой и правой границ поля
			cells = new Cell[row, column];
			for (int i = 0; i < row; i++)
			{
				for (int j = 0; j < column; j++)
				{
					cells[i, j] = new Cell();
				}
			}
		}

		public void Paint()
		{
			Console.CursorVisible = false;
			Console.SetCursorPosition(Game.CurrentX - 1, Game.CurrentY - 1);
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					if (i == 0 || i == height - 1 || j == 0 || j == width  - 1)
					{
						Console.Write(characterForFrame);
					}
					else
					{
						Console.Write(characterForDeadCell);
					}
				}
				Console.WriteLine();
			}
		}

		public void Update()
		{
			Console.SetCursorPosition(leftMost, topMost);
			for (int i = 0; i < row; i++)
			{
				for (int j = 0; j < column; j++)
				{
					if (cells[i, j].GetIsAlive())
					{
						Console.ForegroundColor = ConsoleColor.DarkGreen;
						Console.Write(characterForAliveCell);
						Console.ResetColor();
					}
					else
					{
						Console.Write(characterForDeadCell);
					}
				}
				Console.SetCursorPosition(leftMost, topMost + i + 1);		//+1 - перемещение курсора на 1 строку вниз
			}
		}

		public int GetRow()
		{
			return row;
		}

		public int GetColumn()
		{
			return column;
		}

		public int GetWidth()
		{
			return width;
		}

		public int GetHeight()
		{
			return height;
		}

		public int GetLeftMost()
		{
			return leftMost;
		}

		public int GetTopMost()
		{
			return topMost;
		}

		public Cell[,] GetCells()
		{
			return cells;
		}

		internal void SetCell(Cell[,] cells)
		{
			this.cells = cells;
		}
	}
}