using System;

namespace BananaRobot
{
	public class PredictBot: RobotAI
	{

		private const double coolingRatio = 0.8;

		public PredictBot (int mapWidth, int mapHeight):base(mapWidth, mapHeight)
		{
		}

		public override AIWorld.Direction GetMove() 
		{
			double currentTemp = world.GetAt (xPos, yPos);
			int iMin = 0;
			int jMin = 0;
			double bestValue = currentTemp;

			for (int i = -1; i <= 1; i++) {
				for (int j = -1; j <= 1; j++) {
					int x1 = xPos + i;
					int y1 = yPos + j;
					double t = world.GetAt (x1, y1);

					if (t == AIWorld.NO_TEMP) {
						t = EstimateAt (x1, y1);
						//Console.WriteLine ("t for i=" + i + ",j=" + j + " knowledge, estimate=" + t);
					} else {
						//Console.WriteLine("t for i=" + i + ",j=" + j + ", value=" + t);
					}

					if (t < bestValue) {
						iMin = i;
						jMin = j;
						bestValue = t;
					}
				}
			}

			if (Math.Abs (iMin) == 1 && Math.Abs (jMin) == 1) {
				// corner move, can't do that
				//Console.WriteLine ("Evaluating corner case");
				double ti = world.GetAt (xPos + iMin, yPos);
				double tj = world.GetAt (xPos, yPos + jMin);
				if (ti < tj) {
					jMin = 0;
				} else {
					iMin = 0;
				}
			}

			//Console.WriteLine ("iMin="+iMin+", jMin="+jMin+"=>"+DirectionForMove (xPos, yPos, xPos + iMin, yPos + jMin));

			return DirectionForMove (xPos, yPos, xPos + iMin, yPos + jMin);
		}

		private double EstimateAt(int x, int y)
		{
			double neighTot = 0;
			int neighCount = 0;

			for (int i = -1; i <= 1; i++) {
				for (int j = -1; j <= 1; j++) {
					double t = world.GetAt (x + i, y + j);

					if (t != AIWorld.NO_TEMP) {
						neighTot += t;
						neighCount++;
					}
				}
			}

			double avg = neighTot / neighCount;
			return avg * coolingRatio;
		}
	}
}

