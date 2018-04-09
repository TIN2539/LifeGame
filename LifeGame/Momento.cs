using System.Collections.Generic;

namespace LifeGame
{
	internal class Momento
	{
		private List<Cell[,]> momento;

		public Momento()
		{
			momento = new List<Cell[,]> { };
		}

		public bool CompareCycle(Cell[,] currentCell)
		{
			bool check = true;
			if (momento.Count > 1)
			{
				foreach (Cell[,] cell in momento)
				{
					check = true;
					for (int i = 0; i < cell.GetLength(0); i++)
					{
						for (int j = 0; j < cell.GetLength(1); j++)
						{
							if (cell[i, j].GetStatus() != currentCell[i, j].GetStatus())
							{
								check = false;
								break;
							}
						}
						if (!check)
						{
							break;
						}
					}
					if (check)
					{
						return check;
					}
				}
				return check;
			}
			return false;
		}

		public bool CompareLastVariant(Cell[,] currentCell)
		{
			for (int y = 0; y < currentCell.GetLength(0); y++)
			{
				for (int x = 0; x < currentCell.GetLength(1); x++)
				{
					if (momento[momento.Count - 1][y, x].GetStatus() != currentCell[y, x].GetStatus())
					{
						return false;
					}
				}
			}
			return true;
		}

		internal void Add(Cell[,] cell)
		{
			Cell[,] tempCell = new Cell[cell.GetLength(0), cell.GetLength(1)];
			for (int i = 0; i < tempCell.GetLength(0); i++)
			{
				for (int j = 0; j < tempCell.GetLength(1); j++)
				{
					tempCell[i, j] = new Cell(cell[i, j].GetStatus());
				}
			}
			momento.Add(tempCell);
		}
	}
}