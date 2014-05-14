using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RiverRaid.Generics.SpritesHandle;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace RiverRaid.Elementes
{
    class Enemy : AutomatedSprite
    {
        private bool canRemove;
        public bool CanRemove
        {
            get { return canRemove; }
        }

        private int points = 15;
        public int Points
        {
            get { return points; }
        }

        private Single playerSpeed;
        private Rectangle enemyBounds;

        public Enemy(Texture2D texture, Vector2 position, Vector2 velocity, Rectangle enemyBounds, 
            Point offset, ref Single playerSpeed)
            : base(texture, position, velocity)
        {
            this.offset = offset;  
            this.canRemove = false;
            this.enemyBounds = enemyBounds;
            this.playerSpeed = playerSpeed;
        }

        public bool IsOutBounds()
        {
            if (position.X < enemyBounds.X || position.X > enemyBounds.Width ||
                position.Y < enemyBounds.Y || position.Y > enemyBounds.Height)
                return true;
            return false;
        }

        public override void Update(GameTime gameTime)
        {
            velocity.Y = playerSpeed + 10 ;
            base.Update(gameTime);
        }

        public void Collides()
        {
            canRemove = true;
        }
    }
}
