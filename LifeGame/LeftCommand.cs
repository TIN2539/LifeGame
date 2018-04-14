using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame
{
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
}