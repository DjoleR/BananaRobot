using System;

namespace BananaRobot
{
	public class AIWorld
	{

		public enum Direction {
			UP, DOWN, LEFT, RIGHT, NONE
		}

		//private const int MAP_SIZE = 100;
		public const double NO_TEMP = 0.0;
		
		private double[,] tempMap;
		private readonly int width;
		private readonly int height;

		public AIWorld (int width, int height)
		{
			tempMap = new double[width,height];
			for (int i = 0; i < width; i++) {
				for (int j = 0; j < height; j++) {
					tempMap [i, j] = 0;
				}
			}

			this.width = width;
			this.height = height;
		}

		/*public int[,] GetNeighboringRegion(int x, int y, int offset)
		{
			int retSize = 2 * offset + 1;
			int[,] neighs = new int[retSize,retSize];

			for (int i = 0; i <= offset; i++) {
				for (int j = 0; j <= offset; j++) {
					int xget = x + i - offset;
					int yget = y + j - offset;
					neighs [i,j] = tempMap [xget,yget];
				}
			}

			return neighs;
		}*/

		public void AddMesure(int x, int y, double temp)
		{
			if (ContainsPos (x, y)) {
				tempMap [x, y] = temp;
			}
		}

		public double GetAt(int x, int y)
		{
			if (ContainsPos(x,y)) {
				double a = tempMap [x, y];
				double b = tempMap [x, y];
				double c = tempMap [x, y];
				return a;
			} else {
				return 0;
			}
		}

		public bool ContainsPos(int x, int y) 
		{
			return (x >= 0 || x < width || y >= 0 || y < height);
		}

	}
}

