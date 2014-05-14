using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RiverRaid.Generics.SpritesHandle
{
    public class AutomatedSprite : SpriteSheet
    {
        //suporte a MUV - movimento uniforme variavel
        protected Vector2 velocity;
        
        public AutomatedSprite(Texture2D texture, Vector2 velocity):base(texture)
        {
            this.velocity = velocity;
        }

        public AutomatedSprite(Texture2D texture, Vector2 position, Vector2 velocity)
            : base(texture, position)
        {
            this.velocity = velocity;
        }

        public AutomatedSprite(Texture2D texture, Vector2 position, Point offset, Vector2 velocity)
            :base(texture, position, offset)
        {
            this.velocity = velocity;
        }


        public AutomatedSprite(Texture2D texture, Point frameSize, Point numberOfFrames, Vector2 position, Point offset, bool animated, Vector2 velocity) :
            base(texture, frameSize, offset, position, offset, animated)
        {
            this.velocity = velocity;
        }

        public AutomatedSprite(Texture2D texture, Point frameSize, Point numberOfFrames, Vector2 position, Point offset, bool animated,
            int frameLine, int frameCollumn, Vector2 velocity)
            : base(texture, frameSize, numberOfFrames, position, offset, animated, frameLine, frameCollumn)
        {
            this.velocity = velocity;
        }


        public override void Update(GameTime gameTime)
        {
            position += velocity;
                
            base.Update(gameTime);
        }

        public bool IsOutBounds(Rectangle bounds)
        {
            if (position.X < bounds.X || position.Y < bounds.Y || position.X > bounds.Width || position.Y > bounds.Height)
                return true;
            return false;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

    }
}
