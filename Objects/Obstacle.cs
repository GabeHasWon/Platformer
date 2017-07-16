using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Objects
{
    public class Obstacle : Entity
    {
        public static List<Obstacle> allObstacles = new List<Obstacle>();
        public int index = 0;
        public int type = 0;

        public static void AddObstacle(Vector2 position, int type, Action<Player> hitPlayer = null, bool roundPos = true)
        {
            for (int i = allObstacles.Count - 1; i >= 0; i--)
            {
                if (!allObstacles[i].active)
                {
                    allObstacles[i].position = position;
                    allObstacles[i].position /= 20;
                    allObstacles[i].position.X = (int)allObstacles[i].position.X;
                    allObstacles[i].position.Y = (int)allObstacles[i].position.Y;
                    allObstacles[i].position *= 20;
                    allObstacles[i].active = true;
                    allObstacles[i].index = i;
                    allObstacles[i].bounds = new Rectangle(position.ToPoint(), TileData.typeSizes[type]);
                    allObstacles[i].type = type;
                    break;
                }
            }
        }

        public override void Update()
        {
            bounds = new Rectangle((position + Main.worldPosition).ToPoint(), TileData.typeSizes[type]);
        }

        public static void SetObstacles()
        {
            for (int i = 0; i < 80000; i++)
            {
                Obstacle o = new Obstacle();
                o.active = false;
                allObstacles.Add(o);
            }
        }

        public static void UpdateAll()
        {
            for (int i = allObstacles.Count - 1; i >= 0; i--)
            {
                if (allObstacles[i] != null && allObstacles[i].active)
                {
                    allObstacles[i].Update();
                }
            }
        }

        public static void DrawAll(SpriteBatch batch)
        {
            for (int i = allObstacles.Count - 1; i >= 0; i--)
            {
                if (allObstacles[i] != null && allObstacles[i].active)
                {
                    if (Vector2.Distance(allObstacles[i].position, Main.player.position - Main.worldPosition) < 500)
                    {
                        batch.Draw(Textures.blockTextures[allObstacles[i].type], allObstacles[i].position + Main.worldPosition);
                    }
                }
            }
        }

        public static Action<Player> DirtDustPlayer(Vector2 position)
        {
            return (Player player) =>
            {
                if (player.velocity.Y < -3)
                {
                    Dust.AddDust(position + Main.worldPosition, 200, new Vector2(0, 2 + Main.rand.NextFloat()));
                }
            };
        }
    }
}
