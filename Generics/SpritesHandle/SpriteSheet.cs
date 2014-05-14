using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RiverRaid.Generics.SpritesHandle
{
    public class SpriteSheet
    {
        #region Attributes
        protected int frameLine;
        public int FrameLine
        {
            get { return frameLine; }
            set { frameLine = value; }
        }

        protected int frameCollumn;
        public int FrameCollumn
        {
            get { return frameCollumn; }
            set { frameCollumn = value; }
        }

        protected bool animated;
        public bool Animated
        {
            get { return animated; }
            set { animated = value; }
        }

        protected Point numberOfFrames; // y = vertical, x = horizontal
        public Point NumberOfFrames
        {
            get { return numberOfFrames; }
            set { numberOfFrames = value; }
        }

        protected Point frameSize;
        public Point FrameSize
        {
            get { return frameSize; }
            set { frameSize = value; }
        }

        protected Vector2 position;
        public Vector2 Postion
        {
            get { return position; }
            set { position = value; }
        }

        protected Texture2D texture;
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        protected Point offset;
        public Point Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        public virtual Rectangle CollisionRect
        {
            get
            {
                return new Rectangle((int)position.X - (offset.X / 2),
                    (int)position.Y - (offset.Y / 2),
                    frameSize.X - (offset.X / 2),
                    frameSize.Y - (offset.Y / 2));
            }
        } 
        #endregion

        #region Constructors
        public SpriteSheet(Texture2D texture)
        {
            this.texture = texture;
            this.animated = false;
            this.frameLine = 0;
            this.frameCollumn = 0;
            this.frameSize = new Point(texture.Width, texture.Height);
            this.numberOfFrames = new Point(1, 1);
            this.position = Vector2.Zero;
            this.offset = Point.Zero;
        }

        public SpriteSheet(Texture2D texture, Vector2 position)
            : this(texture)
        {
            this.texture = texture;
            this.position = position;
        }

        public SpriteSheet(Texture2D texture, Vector2 position, Point offset)
            : this(texture, position)
        {
            this.offset = offset;
        }


        public SpriteSheet(Texture2D texture, Point frameSize, Point numberOfFrames, Vector2 position, Point offset, bool animated)
        {
            this.texture = texture;
            this.frameSize = frameSize;
            this.numberOfFrames = numberOfFrames;
            this.position = position;
            this.offset = offset;
            this.animated = animated;
            this.frameLine = 0;
            this.frameCollumn = 0;
        }

        public SpriteSheet(Texture2D texture, Point frameSize, Point numberOfFrames, Vector2 position, Point offset, bool animated,
            int frameLine, int frameCollumn)
            : this(texture, frameSize, numberOfFrames, position, offset, animated)
        {
            this.frameLine = frameLine;
            this.frameCollumn = frameCollumn;
        } 
        #endregion

        #region Methods

        /// <summary>
        /// Metodo que pode ou não ser reescrito para entrada de logica no sprite,
        /// ele foi criado para manter a compatibilidade e para o reuso do codigo
        /// na classe SpriteManager
        /// </summary>
        /// <param name="gameTime">tempo do jogo</param>
        public virtual void Update(GameTime gameTime)
        {
            if (animated)
            {
                frameCollumn++;
                if (frameCollumn >= numberOfFrames.X)
                    frameCollumn = 0;
            }
        }


        /// <summary>
        /// Desenha o sprite sempre na posição 'position'
        /// </summary>
        /// <param name="gameTime">Tempo do jogo</param>
        /// <param name="spriteBatch">conexão com o objeto de desenho</param>
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position,
                new Rectangle(frameCollumn* frameSize.X, frameLine* frameSize.Y, frameSize.X, frameSize.Y),
                Color.White);
        } 
        #endregion

    }
}
