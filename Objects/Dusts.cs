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
    public class Dust : Entity
    {
        public Vector2 velocity;
        public static List<Dust> allDusts = new List<Dust>();
        public int index = 0;
        public int timeLeft = 0;

        public static void AddDust(Vector2 pos, int time, Vector2 vel)
        {
            for (int i = allDusts.Count - 1; i >= 0; i--)
            {
                if (!allDusts[i].active)
                {
                    allDusts[i].position = pos;
                    allDusts[i].active = true;
                    allDusts[i].timeLeft = time;
                    allDusts[i].velocity = vel;
                    allDusts[i].index = i;
                    break;
                }
            }
        }

        public static void SetDusts()
        {
            for (int i = 0; i < 300; i++)
            {
                Dust o = new Dust();
                o.active = false;
                allDusts.Add(o);
            }
        }

        public static void DrawAll(SpriteBatch batch)
        {
            for (int i = allDusts.Count - 1; i >= 0; i--)
            {
                if (allDusts[i] != null && allDusts[i].active)
                {
                    batch.Draw(Textures.dustTextures[0], allDusts[i].position);
                }
            }
        }

        public override void Update()
        {
            position += velocity;
            timeLeft--;
            if (timeLeft < 0)
                Kill();
        }

        public void Kill()
        {
            position = new Vector2(0);
            index = 0;
            active = false;
        }

        public static void UpdateAll()
        {
            for (int i = allDusts.Count - 1; i >= 0; i--)
            {
                if (allDusts[i] != null && allDusts[i].active)
                {
                    allDusts[i].Update();
                }
            }
        }
    }
}
