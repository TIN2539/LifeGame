using System;

namespace LifeGame
{
	internal class Field
	{
		private int column = 10;
		private int row = 40;
		private int width = 12;
		private int lenght = 42;
		private Cell[,] cells;

		public Field(int column, int row)
		{
			this.column = column;
			this.row = row;
			width = column + 2;
			lenght = row + 2;
			cells = new Cell[column, row];
			for (int i = 0; i < column; i++)
			{
				for (int j = 0; j < row; j++)
				{
					cells[i, j] = new Cell();
				}
			}
		}

		public void Paint()
		{
			Console.CursorVisible = false;
			Console.SetCursorPosition(Game.CurrentX - 1, Game.CurrentY - 1);
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < lenght; j++)
				{
					if (i == 0 || i == width - 1 || j == 0 || j == lenght - 1)
					{
						Console.Write('+');
					}
					else
					{
						Console.Write(' ');
					}
				}
				Console.WriteLine();
			}
		}

		public void Update()
		{
			int x = 1;
			int y = 3;
			Console.SetCursorPosition(x, y);
			for (int i = 0; i < column; i++)
			{
				for (int j = 0; j < row; j++)
				{
					if (cells[i, j].GetStatus())
					{
						Console.ForegroundColor = ConsoleColor.DarkGreen;
						Console.Write('O');
						Console.ResetColor();
					}
					else
					{
						Console.Write(' ');
					}
				}
				Console.SetCursorPosition(x, ++y);
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

		public Cell[,] GetCells()
		{
			return cells;
		}

		internal void SetCell(Cell[,] tempCells)
		{
			cells = tempCells;
		}
	}
}