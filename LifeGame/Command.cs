using System;

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
			return key == ConsoleKey.Spacebar;
		}
	}
}