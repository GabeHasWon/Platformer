using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Objects
{
    public class Entity
    {
        public Vector2 position;
        public Texture2D texture;
        public Rectangle bounds;
        public bool active = true;

        public virtual void Update()
        {
        }

        public virtual void Draw(SpriteBatch batch)
        {
        }
    }
}
