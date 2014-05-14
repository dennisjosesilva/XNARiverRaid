using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RiverRaid.Generics.SpritesHandle;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RiverRaid.Generics.GameScene
{
    public class FinalBridge : SpriteSheet
    {
        private bool isDestroyed; 
        private int buildingPoints;

        public FinalBridge(Texture2D texture, Point FrameSize, Vector2 position)
            :base(texture, FrameSize, new Point(1,2), position, Point.Zero, false)
        {
            this.offset.Y = 60;
            Reset();
        }

        public void Reset()
        {
            this.isDestroyed = false;
            buildingPoints = 3;
            frameLine = 0;
        }

        public override Rectangle CollisionRect
        {
            get
            {
                if (isDestroyed)
                    return Rectangle.Empty;
                else
                    return base.CollisionRect;
            }
        }

        public void Collides()
        {
            buildingPoints--;
            if (buildingPoints <= 0)
            {
                frameLine = 1;
                isDestroyed = true;
            }
        }

    }
}
