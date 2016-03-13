using System;

namespace BananaRobot
{

	public abstract class RobotAI
	{

		protected int xPos;
		protected int yPos;
		protected AIWorld world;

		public RobotAI (int mapWidth, int mapHeight)
		{
			world = new AIWorld(mapWidth, mapHeight);
			xPos = mapWidth / 2;
			yPos = mapHeight / 2;
		}

		public AIWorld.Direction MakeMove(double currentTemp)
		{
			world.AddMesure (xPos, yPos, currentTemp);
			AIWorld.Direction dir = GetMove ();
			MoveInDirection (dir);
			return dir;
		}

		public abstract AIWorld.Direction GetMove();

		protected AIWorld.Direction DirectionForMove(int xOld, int yOld, int xNew, int yNew) 
		{
			int dx = xNew - xOld;
			AIWorld.Direction xDir;

			if (dx == 0) {
				xDir = AIWorld.Direction.NONE;
			} else if (dx < 0) {
				xDir = AIWorld.Direction.UP;
			} else {
				xDir = AIWorld.Direction.DOWN;
			}

			int dy = yNew - yOld;
			AIWorld.Direction yDir;

			if (dy == 0) {
				yDir = AIWorld.Direction.NONE;
			} else if (dy < 0) {
				yDir = AIWorld.Direction.LEFT;
			} else {
				yDir = AIWorld.Direction.RIGHT;
			}

			if (dx == 0 && dy == 0)
				return AIWorld.Direction.NONE;
			else if (Math.Abs (dx) > Math.Abs (dy))
				return xDir;
			else
				return yDir;
		}

		private void MoveInDirection(AIWorld.Direction d)
		{
			switch (d)
			{
			case AIWorld.Direction.UP:
				xPos -= 1;
				break;
			case AIWorld.Direction.DOWN:
				xPos += 1;
				break;
			case AIWorld.Direction.LEFT:
				yPos -= 1;
				break;
			case AIWorld.Direction.RIGHT:
				yPos += 1;
				break;
			}
		}

		public int GetX()
		{
			return xPos;
		}

		public int GetY()
		{
			return yPos;
		}
	}
}

