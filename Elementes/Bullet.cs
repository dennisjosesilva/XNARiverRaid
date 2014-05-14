using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RiverRaid.Generics.SpritesHandle;

namespace RiverRaid.Elementes
{


    public class Bullet : AutomatedSprite
    {
        private bool isExplosing;
        public bool IsExplosing
        {
            get { return isExplosing; }
            set { isExplosing = value; }
        }

        private float explosionTime = 20.0f;
        private Texture2D explosionTexture;
        private float explosionTimeCount;
        private Rectangle screenBounds;
        private const int range = 70;

        private bool canRemove;
        public bool CanRemove
        {
            get { return canRemove; }
        }

        public Bullet(Texture2D texture, Texture2D explosionTexture , Vector2 position, Vector2 direction, 
            Rectangle screenDimesion)
            :base(texture, new Point(30, 30), new Point(7, 2), new Vector2(position.X - 15, position.Y),
            Point.Zero, false, direction) 
        {
            this.explosionTexture = explosionTexture;
            this.isExplosing = false;
            this.canRemove = false;
            this.screenBounds = screenDimesion;
        }

        public bool IsOutScreenBounds
        {
            get
            {
                if (position.X > screenBounds.Width || position.X < screenBounds.X
                    || position.Y > screenBounds.Height || position.Y < screenBounds.Y + range)
                    return true;
                return false;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (isExplosing)
            {
                #region Explodindo
                explosionTimeCount += gameTime.ElapsedGameTime.Milliseconds;
                if (frameCollumn >= (numberOfFrames.X - 1))
                {
                    canRemove = true;
                }
                if (explosionTimeCount >= explosionTime)
                {
                    base.Update(gameTime);
                    explosionTimeCount = 0;
                }
                #endregion
            }
            else
            {
                base.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public void Expode()
        {
            isExplosing = true;
            this.animated = true;
            this.frameSize.X = 65;
            this.frameSize.Y = 65;
            this.frameLine = 0;
            this.frameCollumn = 0;
            this.numberOfFrames.X = 7;
            this.velocity = Vector2.Zero;
            this.texture = explosionTexture;

            GameSounds.Sounds.getSingleton().Play("explosionMissile");
        }
        
    }
}
