namespace LifeGame
{
	internal class Cell
	{
		private bool lifeStatus = false;

		public Cell()
		{
		}

		public Cell(bool lifeStatus)
		{
			this.lifeStatus = lifeStatus;
		}

		public void ChangeStatus()
		{
			lifeStatus = !lifeStatus;
		}

		public bool GetStatus()
		{
			return lifeStatus;
		}
	}
}