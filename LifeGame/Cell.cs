namespace LifeGame
{
	internal class Cell
	{
		private bool isAlive;

		public Cell()
		{
			isAlive = false;
		}

		public Cell(bool isAlive)
		{
			this.isAlive = isAlive;
		}

		public void ChangeStatus()
		{
			isAlive = !isAlive;
		}

		public bool GetIsAlive()
		{
			return isAlive;
		}
	}
}