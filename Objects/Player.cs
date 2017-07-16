using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Drawing;

namespace Platformer.Objects
{
    public class Player : Entity
    {
        public Vector2 velocity;
        public float gravity = 0;

        public override void Update()
        {
            ObstacleCollide();
            bounds = new Rectangle((position + Main.worldPosition).ToPoint(), new Point(21, 16));
            position += velocity;
            if (KeyDown(Keys.Up))
                Jump();
            if (KeyDown(Keys.Down))
                velocity.Y += 0.1f;
            if (KeyDown(Keys.Left))
                velocity.X -= 0.1f;
            if (KeyDown(Keys.Right))
                velocity.X += 0.1f;
            gravity += 0.005f;
            if (gravity > 0)
                gravity *= 1.002f;
            velocity.Y += gravity;
            if (position.X < 30)
            {
                Main.worldPosition.X += Math.Abs(velocity.X);
                position.X += Math.Abs(velocity.X);
            }
            if (position.X > Main.Right - 30)
            {
                Main.worldPosition.X -= Math.Abs(velocity.X);
                position.X -= Math.Abs(velocity.X);
            }
        }

        public void Jump()
        {
            velocity.Y -= 0.1f;
            position.Y -= 2;
        }

        public void ObstacleCollide()
        {
            for (int i = Obstacle.allObstacles.Count - 1; i >= 0; i--)
            {
                if (bounds.Intersects(Obstacle.allObstacles[i].bounds) && Obstacle.allObstacles[i].active && TileData.typeSolid[Obstacle.allObstacles[i].type])
                {
                    TileData.GetHit(Obstacle.allObstacles[i].position, Obstacle.allObstacles[i].type);
                    velocity.Y = 0;
                    gravity = -0;
                    position.Y = Obstacle.allObstacles[i].position.Y - 17;
                }
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(Textures.Player, position);
        }

        public static bool KeyDown(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key);
        }
    }
}
