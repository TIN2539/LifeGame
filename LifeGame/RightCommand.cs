using System;

namespace LifeGame
{
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
}