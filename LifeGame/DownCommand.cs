using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame
{
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
}
