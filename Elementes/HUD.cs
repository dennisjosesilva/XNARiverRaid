using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RiverRaid.Elementes
{
    class HUD
    {
        private Texture2D texture;
        private SpriteFont spriteFont;
        private Player playerInfo;
        private string levelName;

        public HUD(string levelName, Texture2D texture, SpriteFont spriteFont, Player playerInfo)
        {
            this.texture = texture;
            this.spriteFont = spriteFont;
            this.playerInfo = playerInfo;
            this.levelName = levelName;
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Vector2.Zero, Color.White);
            spriteBatch.DrawString(spriteFont, playerInfo.Lifes.ToString() + "x", new Vector2(670, 560), Color.White);
            spriteBatch.DrawString(spriteFont, "score: " + playerInfo.Points.ToString(), new Vector2(85, 22), Color.White);
            spriteBatch.DrawString(spriteFont, levelName , new Vector2(620, 22), Color.White);
        }

    }
}
