using System;

namespace LifeGame
{
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
}