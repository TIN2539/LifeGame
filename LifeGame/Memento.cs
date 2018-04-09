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

		public bool HasSameCells(Cell[,] currentCell)
		{
			bool hasSameCells = false;
			if (memento.Count > 1)
			{
				foreach (Cell[,] cells in memento)
				{
					hasSameCells = true;
					for (int i = 0; i < cells.GetLength(0); i++)
					{
						for (int j = 0; j < cells.GetLength(1); j++)
						{
							if (cells[i, j].GetIsAlive() != currentCell[i, j].GetIsAlive())
							{
								hasSameCells = false;
								break;
							}
						}
						if (!hasSameCells)
						{
							break;
						}
					}
					if (hasSameCells)
					{
						break;
					}
				}
			}
			return hasSameCells;
		}

		public bool IsIdenticalCells(Cell[,] currentCell)
		{
			bool isAdenticalCells = true;
			for (int y = 0; y < currentCell.GetLength(0); y++)
			{
				for (int x = 0; x < currentCell.GetLength(1); x++)
				{
					if (memento[memento.Count - 1][y, x].GetIsAlive() != currentCell[y, x].GetIsAlive())
					{
						isAdenticalCells = false;
						break;
					}
				}
				if(!isAdenticalCells)
				{
					break;
				}
			}
			return isAdenticalCells;
		}

		internal void Add(Cell[,] cell)
		{
			Cell[,] cellOfCurrentField = new Cell[cell.GetLength(0), cell.GetLength(1)];
			for (int i = 0; i < cellOfCurrentField.GetLength(0); i++)
			{
				for (int j = 0; j < cellOfCurrentField.GetLength(1); j++)
				{
					cellOfCurrentField[i, j] = new Cell(cell[i, j].GetIsAlive());
				}
			}
			memento.Add(cellOfCurrentField);
		}
	}
}