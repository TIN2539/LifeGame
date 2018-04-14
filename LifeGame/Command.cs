using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame
{
	internal class Command : ICommand
	{
		private ConsoleKey key;

		public Command(ConsoleKey key)
		{
			this.key = key;
		}

		public override bool CanExecute(ConsoleKey key)
		{
			return this.key == key;
		}

		public override bool Execute()
		{
			return this.key == ConsoleKey.Spacebar;
		}
	}

}