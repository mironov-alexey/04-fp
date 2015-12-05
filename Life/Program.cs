using System;

namespace ConwaysGameOfLife
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			var ui = new ConsoleUi(20, 20);
			var game = new Game(20, 20).ReviveCells(glider);
			ui.Update(game);
			while (true)
			{
				Console.ReadKey(intercept:true);
			    game = game.Step();
				ui.Update(game);
			}
		}

		private static readonly Point[] glider =
		{
			new Point(5, 0), new Point(5, 2),
			new Point(6, 1), new Point(6, 2),
			new Point(7, 1)
		};

		private static readonly Point[] pentamino =
		{
			new Point(9, 9), new Point(10, 9),
			new Point(8, 8), new Point(9, 8),
			new Point(9, 7)
		};

		private static readonly Point[] stick5 =
		{
			new Point(8, 10), 
			new Point(9, 10), 
			new Point(10, 10), 
			new Point(11, 10), 
			new Point(12, 10), 
		};
	}
}