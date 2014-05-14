using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RiverRaid.Generics.GameScene;

namespace RiverRaid.GameScenes
{
    class GameOverScene : GameScene
    {
        private Texture2D texture;

        bool enable = true;
        
        public GameOverScene(Game game, EnumGameScene currentGameScene, EnumGameScene nextGameScene, Texture2D texture)
        : base(game, currentGameScene, nextGameScene)
        {
            this.texture = texture;
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            GameSounds.Sounds.getSingleton().Pause("player");
            GameSounds.Sounds.getSingleton().Pause("fase");
            if (enable == true)
            {
           //     GameSounds.Sounds.getSingleton().Play("end");
                enable = false;
            }

            if (keyboardState.IsKeyDown(Keys.Enter))
                this.Game.Exit();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            spriteBatch.Begin();

            spriteBatch.Draw(texture, Vector2.Zero, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
