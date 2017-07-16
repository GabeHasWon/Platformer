using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Objects
{
    public static class TileData
    {
        public static bool[] typeSolid = { true, true, true, false, false};
        public static Point[] typeSizes = { new Point(20), new Point(20), new Point(20), new Point(20), new Point(60) };

        public static Action<Player> GetHit(Vector2 position, int type)
        {
            return Obstacle.DirtDustPlayer(position);
        }
    }
}
