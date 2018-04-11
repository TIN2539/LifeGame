using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame
{
	internal abstract class ICommand
	{
		public abstract bool Execute(ref int currentX, ref int currentY);

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

		public override bool Execute(ref int currentX, ref int currentY)
		{
			throw new NotImplementedException();
		}

		public bool Execute()
		{
			return this.key == ConsoleKey.Spacebar;
		}
	}

	internal class RightCommand : Command
	{
		private int column;
		public RightCommand(int column) : base(ConsoleKey.RightArrow)
		{
			this.column = column;
		}

		public override bool Execute(ref int currentX, ref int currentY)
		{
			if (currentX < column)
			{
				++currentX;
				Console.SetCursorPosition(currentX, currentY);
			}
			return base.Execute();
		}
	}

	internal class LeftCommand : Command
	{
		private int leftMost;
		public LeftCommand(int leftMost) : base(ConsoleKey.LeftArrow)
		{
			this.leftMost = leftMost;
		}

		public override bool Execute(ref int currentX, ref int currentY)
		{
			if (currentX > leftMost)
			{
				--currentX;
				Console.SetCursorPosition(currentX, currentY);
			}
			return base.Execute();
		}
	}

	internal class UpCommand : Command
	{
		private int topMost;
		public UpCommand(int topMost) : base(ConsoleKey.UpArrow)
		{
			this.topMost = topMost;
		}

		public override bool Execute(ref int currentX, ref int currentY)
		{
			if (currentY > topMost)
			{
				--currentY;
				Console.SetCursorPosition(currentX, currentY);
			}
			return base.Execute();
		}
	}

	internal class DownCommand : Command
	{
		private int row;
		public DownCommand(int row) : base(ConsoleKey.DownArrow)
		{
			this.row = row;
		}

		public override bool Execute(ref int currentX, ref int currentY)
		{
			if (currentY <= row)
			{
				++currentY;
				Console.SetCursorPosition(currentX, currentY);
			}
			return base.Execute();
		}
	}

	internal class EnterCommand : Command
	{
		private Field field;

		public EnterCommand(Field field) : base(ConsoleKey.Enter)
		{
			this.field = field;
		}

		public override bool Execute(ref int currentX, ref int currentY)
		{
			field.GetCells()[currentY - field.GetTopMost(), currentX - field.GetLeftMost()].ChangeStatus();
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

		public override bool Execute(ref int currentX, ref int currentY)
		{
			do
			{
				game.Play();
			} while (!game.IsGameOver());
			return base.Execute();
		}
	}

}
