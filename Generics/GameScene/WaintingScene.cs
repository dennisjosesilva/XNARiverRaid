using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace RiverRaid.Generics.GameScene
{
    
    public class WaintingScene : GameScene
    {
        private Texture2D texture;

        public WaintingScene(Game game, EnumGameScene currentGameScene, EnumGameScene nextGameScene, Texture2D texture)
        : base(game, currentGameScene, nextGameScene)
        {
            this.texture = texture;
        }

        public override void Update(GameTime gameTime)
        {
            if (shouldUpdate == ShouldUpdate.YES)
            {
                KeyboardState keyBoardState = Keyboard.GetState();

                if (keyBoardState.IsKeyDown(Keys.Enter))
                    changeGameScene();
            }
            else
                shouldUpdate = ShouldUpdate.YES;
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
