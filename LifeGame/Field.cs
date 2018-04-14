using System;

namespace LifeGame
{
	internal class Field
	{
		private const char characterForAliveCell = 'O';
		private const char characterForDeadCell = ' ';
		private const char characterForFrame = '+';
		private const int leftMost = 1;
		private const int topMost = 2;

		private Cell[,] cells;
		private int column;
		private int height;
		private int row;
		private int width;

		public Field(int row, int column)
		{
			this.column = column;
			this.row = row;
			width = column + 2; // +2 - поправка с учетом наличия нижней и верхней границ поля
			height = row + 2; // +2 - поправка с учетом наличия левой и правой границ поля
			cells = new Cell[row, column];
			for (int i = 0; i < row; i++)
			{
				for (int j = 0; j < column; j++)
				{
					cells[i, j] = new Cell();
				}
			}
		}

		public Cell[,] GetCells()
		{
			return cells;
		}

		public int GetColumn()
		{
			return column;
		}

		public int GetHeight()
		{
			return height;
		}

		public int GetLeftMost()
		{
			return leftMost;
		}

		public int GetRow()
		{
			return row;
		}

		public int GetTopMost()
		{
			return topMost;
		}

		public int GetWidth()
		{
			return width;
		}

		public void Paint()
		{
			Console.CursorVisible = false;

			Console.SetCursorPosition(leftMost - 1, topMost - 1); // -1 -- сдвинуть курсор за пределы игрового поля для рисовки рамки
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					if (i == 0 || i == height - 1 || j == 0 || j == width - 1)
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
				Console.SetCursorPosition(leftMost, topMost + i + 1); // +1 - перемещение курсора на 1 строку вниз
			}
		}

		internal void SetCell(Cell[,] cells)
		{
			this.cells = cells;
		}
	}
}