namespace LifeGame
{
	internal class Cell
	{
		private bool lifeStatus = false;

		public Cell()
		{
		}

		public Cell(bool status)
		{
			lifeStatus = status;
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