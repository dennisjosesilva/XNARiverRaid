using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RiverRaid.Generics.SpritesHandle;

namespace RiverRaid.Elementes
{
    public enum EnumPlayerAnimation {STOP = 0, FORWARD = 1, LEFT = 2, RIGHT = 3, EXPLOSION = 4};

    class Player : SpriteSheet
    {
        private Point bounds;
        private bool isExplosioning;
        private const float ExplosionTime = 400.0f;
        private float explosionTimeCount = 0.0f;

        private Vector2 bufferPosition;

        private Texture2D bulletTexture;
        private Texture2D bulletExplosionTexture;

        private int points;
        public int Points
        {
            get { return points; }
        }

        private List<Bullet> bullets;
        public List<Bullet> Bullets
        {
            get { return bullets; }
        }

        private bool lose;
        public bool Lose
        {
            get { return lose; }
        }

        private bool win;
        public bool Win
        {
            get { return win; }
        }

        private int lifes;
        public int Lifes
        {
            get { return lifes; }
        }

        private bool needLevelReset;
        public bool NeedLevelReset
        {
            get { return needLevelReset; }
            set { needLevelReset = value; }
        }

        public  float speed;
        public float Speed
        {
            get { return speed; }
        }

        public Rectangle CollisionBox
        {
            get { return new Rectangle((int)position.X, (int)position.Y, (int)frameSize.X, (int)frameSize.Y); }
        }

        public Player(Texture2D texture, Texture2D bulletTexture, Texture2D bulletExplosionTexture
            , Point frameSize, Point numberOfFrames, Vector2 position, 
            Point offset, Point bounds) :
            base(texture, frameSize, numberOfFrames, position, offset, true)
        {
            #region Inicialização do Player
            this.bounds = bounds;
            this.speed = 8.0f;
            this.lifes = 3;
            this.points = 0;
            #endregion
            
            bufferPosition = position;
            
            #region Inicialização das bullets
            this.bullets = new List<Bullet>();
            this.bulletTexture = bulletTexture;
            this.bulletExplosionTexture = bulletExplosionTexture; 
            #endregion

            StartGame();
            needLevelReset = true;
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (isExplosioning)
            {
                #region Explodindo
                this.frameLine = (int)EnumPlayerAnimation.EXPLOSION;
                explosionTimeCount += gameTime.ElapsedGameTime.Milliseconds;
                if (frameCollumn >= (numberOfFrames.X - 1))
                {
                    if (lifes == 0)
                        lose = true;
                    needLevelReset = true;
                }
                if (explosionTimeCount >= ExplosionTime)
                {
                    base.Update(gameTime);
                    explosionTimeCount = 0;
                } 
                #endregion
            }
            else
            {
                #region Não Explodindo
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    this.frameLine = (int)EnumPlayerAnimation.FORWARD;
                    this.speed = 20.0f;
                }
                else if (keyboardState.IsKeyDown(Keys.Left))
                {
                    this.frameLine = (int)EnumPlayerAnimation.LEFT;
                    if (position.X > 0)
                        position.X -= speed;
                }
                else if (keyboardState.IsKeyDown(Keys.Right))
                {
                    this.frameLine = (int)EnumPlayerAnimation.RIGHT;
                    if (position.X < (bounds.X - frameSize.X))
                        position.X += speed;
                }
                else if(keyboardState.IsKeyDown(Keys.Down))

                {
                    this.frameLine = (int)EnumPlayerAnimation.STOP;
                    this.speed = 5.0f;
                }
                else
                {
                    this.frameLine = (int)EnumPlayerAnimation.STOP;
                    this.speed = 10.0f;
                }

                #region Processa eventos para a tiro

                if (keyboardState.IsKeyDown(Keys.Space))
                    fire();

                for (int i = 0; i < bullets.Count; i++)
                {
                    bullets[i].Update(gameTime);
                    if (bullets[i].IsOutScreenBounds && !bullets[i].IsExplosing)
                    {
                        bullets[i].Expode();
                    }
                    if (bullets[i].CanRemove)
                        bullets.RemoveAt(i);
                }
                
                #endregion

                base.Update(gameTime); 
                #endregion
            }

        }

        public override void  Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
 	        base.Draw(gameTime, spriteBatch);
            foreach (Bullet bullet in bullets)
                bullet.Draw(gameTime, spriteBatch);
        }

        public void Collides()
        {
            GameSounds.Sounds.getSingleton().Stop("player");
            GameSounds.Sounds.getSingleton().Stop("fase");
            GameSounds.Sounds.getSingleton().Resume("explosionPlay");

            
            frameCollumn = 0;
            isExplosioning = true;
            lifes--;

        }

        public void StartGame()
        {
            isExplosioning = false;
            needLevelReset = false;
            position = bufferPosition;
            this.frameLine = (int)EnumPlayerAnimation.STOP;


            if (!GameSounds.Sounds.getSingleton().IsPaused("explosionPlay"))
            {
                GameSounds.Sounds.getSingleton().Pause("explosionPlay");
            }

        }

        public void fire()
        {
            GameSounds.Sounds.getSingleton().Play("missile");


            bullets.Add(new Bullet(bulletTexture, bulletExplosionTexture,
                new Vector2(position.X + (frameSize.X / 2), position.Y), new Vector2(0, -20),
                new Rectangle(0, 0, bounds.X, bounds.Y)));

            GameSounds.Sounds.getSingleton().Stop("missile");
            //GameSounds.Sounds.getSingleton().Play("explosionMissile");
        }

        public void MoveForward()
        {
            position.Y -= speed;
            if (position.Y < 0)
                win = true;
        }

        public void GainPoints(int points)
        {
            this.points += points;
        }
    }
}
