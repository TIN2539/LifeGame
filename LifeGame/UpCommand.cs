using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame
{
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
}