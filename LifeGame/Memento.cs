using System.Collections.Generic;

namespace LifeGame
{
	internal class Memento
	{
		private List<Cell[,]> memento;

		public Memento()
		{
			memento = new List<Cell[,]> { };
		}

		public bool CompareCycle(Cell[,] currentCell)
		{
			bool checkForMatch = true;
			if (memento.Count > 1)
			{
				foreach (Cell[,] cell in memento)
				{
					checkForMatch = true;
					for (int i = 0; i < cell.GetLength(0); i++)
					{
						for (int j = 0; j < cell.GetLength(1); j++)
						{
							if (cell[i, j].GetStatus() != currentCell[i, j].GetStatus())
							{
								checkForMatch = false;
								break;
							}
						}
						if (!checkForMatch)
						{
							break;
						}
					}
					if (checkForMatch)
					{
						return checkForMatch;
					}
				}
				return checkForMatch;
			}
			return false;
		}

		public bool CompareLastVariant(Cell[,] currentCell)
		{
			for (int y = 0; y < currentCell.GetLength(0); y++)
			{
				for (int x = 0; x < currentCell.GetLength(1); x++)
				{
					if (memento[memento.Count - 1][y, x].GetStatus() != currentCell[y, x].GetStatus())
					{
						return false;
					}
				}
			}
			return true;
		}

		internal void Add(Cell[,] cell)
		{
			Cell[,] cellOfCurrentField = new Cell[cell.GetLength(0), cell.GetLength(1)];
			for (int i = 0; i < cellOfCurrentField.GetLength(0); i++)
			{
				for (int j = 0; j < cellOfCurrentField.GetLength(1); j++)
				{
					cellOfCurrentField[i, j] = new Cell(cell[i, j].GetStatus());
				}
			}
			memento.Add(cellOfCurrentField);
		}
	}
}