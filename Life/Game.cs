using System;
using System.Collections.Generic;
using System.Linq;

namespace ConwaysGameOfLife
{
	public class Game : IReadonlyField
	{
		private readonly int width;
		private readonly int height;
		private readonly int[,] cellAge;

		public Game(int width, int height, int [,] cellAge=null)
		{
			this.width = width;
			this.height = height;
			this.cellAge = cellAge ?? new int[width, height];
		}

		public int GetAge(int x, int y)
		{
			return cellAge[(x + width) % width, (y + height) % height];
		}

		public Game ReviveCells(params Point[] points)
		{
		    var newCellAge = new int[width, height];        
			foreach (var pos in points)
			{
			    newCellAge[(pos.X + width)%width, (pos.Y + height)%height] = 1;
			}
		    return new Game(width, height, newCellAge);
		}

		public Game Step()
		{
			int[,] newCellAge = new int[width, height];
            for (int y = 0; y < height; y++)
				for (int x = 0; x < width; x++)
				{
					var aliveCount = GetNeighbours(x, y).Count(p => GetAge(p.X, p.Y) > 0);
					newCellAge[x, y] = NewCellAge(cellAge[x, y], aliveCount);
    			}
			return new Game(width, height, newCellAge);
		}

		private static int NewCellAge(int age, int aliveNeighbours)
		{
			var willBeAlive = aliveNeighbours == 3 || aliveNeighbours == 2 && age > 0;
			return willBeAlive ? age + 1 : 0;
		}

		private IEnumerable<Point> GetNeighbours(int x, int y)
		{
			return
				from xx in Enumerable.Range(x - 1, 3)
				from yy in Enumerable.Range(y - 1, 3)
				where xx != x || yy != y
				select new Point(xx, yy);
		}

		public override string ToString()
		{
			var rows = Enumerable.Range(0, height)
				.Select(y => string.Join("", Enumerable.Range(0, width).Select(x => cellAge[x, y] > 0 ? "#" : " ")));
			return string.Join("\n", rows);
		}
	}
}