using System;

namespace LifeGame
{
	internal abstract class ICommand
	{
		public abstract bool Execute();

		public abstract bool CanExecute(ConsoleKey key);
	}

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

	internal class RightCommand : Command
	{
		private Game game;

		public RightCommand(Game game) : base(ConsoleKey.RightArrow)
		{
			this.game = game;
		}

		public override bool Execute()
		{
			if (game.GetCurrentX() < game.GetField().GetColumn())
			{
				Console.SetCursorPosition(game.IncrementX(), game.GetCurrentY());
			}
			return base.Execute();
		}
	}

	internal class LeftCommand : Command
	{
		private Game game;

		public LeftCommand(Game game) : base(ConsoleKey.LeftArrow)
		{
			this.game = game;
		}

		public override bool Execute()
		{
			if (game.GetCurrentX() > game.GetField().GetLeftMost())
			{
				Console.SetCursorPosition(game.DecrementX(), game.GetCurrentY());
			}
			return base.Execute();
		}
	}

	internal class UpCommand : Command
	{
		private Game game;

		public UpCommand(Game game) : base(ConsoleKey.UpArrow)
		{
			this.game = game;
		}

		public override bool Execute()
		{
			if (game.GetCurrentY() > game.GetField().GetTopMost())
			{
				Console.SetCursorPosition(game.GetCurrentX(), game.DecrementY());
			}
			return base.Execute();
		}
	}

	internal class DownCommand : Command
	{
		private Game game;

		public DownCommand(Game game) : base(ConsoleKey.DownArrow)
		{
			this.game = game;
		}

		public override bool Execute()
		{
			if (game.GetCurrentY() <= game.GetField().GetRow())
			{
				Console.SetCursorPosition(game.GetCurrentX(), game.IncrementY());
			}
			return base.Execute();
		}
	}

	internal class EnterCommand : Command
	{
		private Game game;

		public EnterCommand(Game game) : base(ConsoleKey.Enter)
		{
			this.game = game;
		}

		public override bool Execute()
		{
			game.GetField().GetCells()[game.GetCurrentY() - game.GetField().GetTopMost(), game.GetCurrentX() - game.GetField().GetLeftMost()].ChangeStatus();
			return base.Execute();
		}
	}

	internal class SpacebarCommand : Command
	{
		private Game game;

		public SpacebarCommand(Game game) : base(ConsoleKey.Spacebar)
		{
			this.game = game;
		}

		public override bool Execute()
		{
			do
			{
				game.Play();
			} while (!game.IsGameOver());
			return base.Execute();
		}
	}
}