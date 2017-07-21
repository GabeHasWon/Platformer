using BenchmarkDotNet.Attributes;
using Microsoft.Xna.Framework;
using Platformer.Objects;

namespace Platformer.WorldGen
{
    class Generator
    {
        [Benchmark]
        public static void MakeTitle()
        {
            int startY = 0;
            for (int i = 0; i < 200; ++i)
            {
                Obstacle.AddWall(new Vector2(i * 20, 400 - startY), 0, false);
                Obstacle.AddObstacle(new Vector2(i * 20, 400 - startY), 0);
                BuildUnderground(i, startY);
                if (Main.rand.Next(20) == 0)
                {
                    int size = Main.rand.Next(5, 9);
                    for (int k = 0; k > -size; --k)
                    {
                        Vector2 position = new Vector2(i * 20, 380 - startY + (k * 20));
                        Obstacle.AddObstacle(position, 3, true);
                    }
                    Obstacle.AddObstacle(new Vector2((i * 20) - 20, 420 - startY - ((size + 4) * 20)), 4, false);
                }
                int rand = Main.rand.Next(3);
                if (rand == 0)
                    startY -= 20;
                else if (rand == 1)
                    startY += 20;
            }
        }

        public static void BuildUnderground(int i, int startY)
        {
            int totalReps = 100;
            if (i > 130)
                totalReps = 210 - i;
            for (int k = 0; k < totalReps; ++k)
            {
                int randStone = Main.rand.Next(20);
                if (k > 5 && k <= 10)
                    randStone = Main.rand.Next(16);
                if (k > 10 && k <= 21)
                    randStone = Main.rand.Next(6);
                if (k > 21 && k <= 27)
                    randStone = Main.rand.Next(3);
                if (k > 27)
                    randStone = Main.rand.Next(1);
                Obstacle.AddObstacle(new Vector2(i * 20, 420 - startY + (k * 20)), randStone == 0 ? 2 : 1, true);
            }
        }
        
        public static void LevelOne()
        {
            for (int i = 0; i < 100; ++i)
            {
                int reps = 90;
                reps -= i * 2;
                reps += Main.rand.Next(-1, 2);
                for (int k = 0; k < reps; k++)
                {
                    int type = 0;
                    if (k > 0)
                        type = 1;
                    Obstacle.AddWall(new Vector2(i * 20, 400 + (k * 20)), 0, false);
                    Obstacle.AddObstacle(new Vector2(i * 20, 400 + (k * 20)), type);
                }
            }
        }
        
        public static void ClearWorld()
        {
            for (int i = Obstacle.allObstacles.Count - 1; i >= 0; i--)
            {
                Obstacle.allObstacles[i].active = false;
                Obstacle.allObstacles[i].type = -1;
                Obstacle.allObstacles[i].wall = -1;
            }
            Main.worldPosition = new Vector2(0);
        }
    }
}
