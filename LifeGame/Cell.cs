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
			if (lifeStatus)
			{
				lifeStatus = false;
			}
			else
			{
				lifeStatus = true;
			}
		}

		public bool GetStatus()
		{
			return lifeStatus;
		}
	}
}