using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Drawing
{
    public static class Textures
    {
        public static Texture2D[] blockTextures = new Texture2D[5];
        public static Texture2D Player;
        public static Texture2D[] dustTextures = new Texture2D[3];
        public static Texture2D debugTexture;

        public static void Load(ContentManager manager)
        {
            Player = manager.Load<Texture2D>("Player");
            for (int i = 0; i < 5; i++)
                blockTextures[i] = manager.Load<Texture2D>("Obstacles/Block" + i);
            for (int i = 0; i < 2; i++)
                dustTextures[i] = manager.Load<Texture2D>("Dusts/Dust" + i);
            debugTexture = manager.Load<Texture2D>("DebugObj");
        }
    }
}
