using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Drawing;
using BenchmarkDotNet.Attributes;

namespace Platformer.Objects
{
    public class Player : Entity
    {
        public Vector2 velocity;
        public float gravity = 0;
        public bool canMove = true;

        [Benchmark]
        public override void Update()
        {
            ObstacleCollide();
            position += velocity;
            bounds = new Rectangle(position.ToPoint(), new Point(21, 16));
            if (canMove)
                Move();
            else
                velocity *= .70f;
            gravity += 0.005f;
            if (gravity > 0)
                gravity *= 1.002f;
            velocity.Y += gravity;
            if (position.X < 200)
            {
                Main.worldPosition.X += Math.Abs(velocity.X);
                position.X += Math.Abs(velocity.X);
            }
            if (position.X > Main.Right - 200)
            {
                Main.worldPosition.X -= Math.Abs(velocity.X);
                position.X -= Math.Abs(velocity.X);
            }

            if (velocity.X > 10f)
                velocity.X = 10;
            if (velocity.Y > 10f)
                velocity.Y = 10;
        }

        public void Jump()
        {
            velocity.Y -= 0.1f;
            position.Y -= 2;
        }

        public void Move()
        {
            if (KeyDown(Keys.W))
                Jump();
            if (KeyDown(Keys.S))
                velocity.Y += 0.1f;
            if (KeyDown(Keys.A))
                velocity.X -= 0.1f;
            if (KeyDown(Keys.D))
                velocity.X += 0.1f;
        }

        public void ObstacleCollide()
        {
            Main.testObj = new Tuple<bool, Texture2D, string>(false, null, string.Empty);
            for (int i = Obstacle.allObstacles.Count - 1; i >= 0; i--)
            {
                bool canHit = bounds.Intersects(Obstacle.allObstacles[i].bounds) && Obstacle.allObstacles[i].active && TileData.typeSolid[Obstacle.allObstacles[i].type] && Obstacle.allObstacles[i].type >= 0;
                float distY = Math.Abs(position.Y - (Obstacle.allObstacles[i].position.Y + Main.worldPosition.Y));
                float distX = Math.Abs(position.X - (Obstacle.allObstacles[i].position.X + Main.worldPosition.X));
                int dirX = position.X > Obstacle.allObstacles[i].position.X ? 1 : 0;
                int dirY = position.Y > Obstacle.allObstacles[i].position.Y ? 1 : 0;
                if (canHit)
                {
                    if (distX < distY)
                    {
                        TileData.GetHit(Obstacle.allObstacles[i].position, Obstacle.allObstacles[i].type);
                        velocity.Y = 0;
                        gravity = -0;
                        if (dirY == 0)
                            position.Y = Obstacle.allObstacles[i].position.Y - 16.01f;
                        else
                            position.Y = Obstacle.allObstacles[i].position.Y + 16.01f;
                        position.Y += Main.worldPosition.Y;
                    }
                    else
                    {
                        TileData.GetHit(Obstacle.allObstacles[i].position, Obstacle.allObstacles[i].type);
                        velocity.X = 0;
                        if (dirX == 0)
                            position.X = Obstacle.allObstacles[i].position.X - 17f;
                        else
                            position.X = Obstacle.allObstacles[i].position.X + 17f;
                        position.X += Main.worldPosition.X;
                    }
                    Main.testObj = new Tuple<bool, Texture2D, string>(true, Textures.blockTextures[Obstacle.allObstacles[i].type], $"Y > X? {distY > distX} - DirY/DirX? {dirY} {dirX} ");
                }
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(Textures.Player, position, Color.White);
            batch.Draw(Textures.debugTexture, new Vector2(Main.Right / 2, Main.Bottom / 2) - (Vector2.Normalize(position - (position + velocity)) * 100), color: Color.Red, layerDepth: 0f);
            batch.Draw(Textures.debugTexture, new Vector2(Main.Right / 2, Main.Bottom / 2), color: Color.Red, layerDepth: 0f);
        }

        public static bool KeyDown(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key);
        }
    }
}
