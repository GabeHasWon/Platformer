using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.Drawing;
using Platformer.Objects;
using Platformer.WorldGen;
using System;

namespace Platformer
{
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        public SpriteBatch batch;
        public static Player player;
        public static Random rand = new Random();
        public static Vector2 worldPosition = new Vector2(0);
        public static int Right = 0;

        public Main()
        {
            Right = Window.ClientBounds.Right;
            player = new Player();
            Obstacle.SetObstacles();
            Dust.SetDusts();
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Generator.MakeWorld();
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Textures.Load(Content);
            batch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            Right = Window.ClientBounds.Right;
            graphics.ApplyChanges();
            player.Update();
            Obstacle.UpdateAll();
            Dust.UpdateAll();
            if (graphics.PreferredBackBufferWidth != Window.ClientBounds.Width)
            {
                graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
                graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
                graphics.ApplyChanges();
            }
            if (Player.KeyDown(Keys.N))
                Dust.AddDust(Mouse.GetState().Position.ToVector2(), 400, new Vector2(0, 2 + rand.NextFloat()));
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            batch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, SamplerState.PointClamp);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            player.Draw(batch);
            Obstacle.DrawAll(batch);
            Dust.DrawAll(batch);
            base.Draw(gameTime);
            batch.End();
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            if (graphics.PreferredBackBufferWidth != Window.ClientBounds.Width)
            {
                graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
                graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
            }
        }
    }
}
