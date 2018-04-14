using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame
{
	internal class Parameters
	{
		public int Column { get; set; }
		public int Delay { get; set; }
		public int Row { get; set; }

		public Parameters() : this(10, 40, 300)
		{
		}

		public Parameters(int row, int column, int delay)
		{
			this.Column = column;
			this.Row = row;
			this.Delay = delay;
		}
	}
}