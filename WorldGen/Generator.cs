using BenchmarkDotNet.Attributes;
using Microsoft.Xna.Framework;
using Platformer.Objects;

namespace Platformer.WorldGen
{
    class Generator
    {
        [Benchmark]
        static void MakeWorld()
        {
            int startY = 0;
            for (int i = 0; i < 800; ++i)
            {
                Obstacle.AddObstacle(new Vector2(i * 20, 400 - startY), 0);
                BuildUnderground(i, startY);
                if (Main.rand.Next(20) == 0)
                {
                    int size = Main.rand.Next(5, 9);
                    for (int k = 0; k > -size; --k)
                    {
                        Vector2 position = new Vector2(i * 20, 380 - startY + (k * 20));
                        Obstacle.AddObstacle(position, 3);
                    }
                    Obstacle.AddObstacle(new Vector2((i * 20) - 20, 420 - ((size + 4) * 20)), 4);
                }
                int rand = Main.rand.Next(4);
                if (rand == 0)
                    startY--;
                if (rand == 1)
                    startY++;
            }
        }

        static void BuildUnderground(int i, int startY)
        {
            for (int k = 0; k < 100; ++k)
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
                Obstacle.AddObstacle(new Vector2(i * 20, 420 - startY + (k * 20)), randStone == 0 ? 2 : 1);
            }
        }
    }
}
