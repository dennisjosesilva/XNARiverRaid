using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RiverRaid.Generics.XMLParser;

namespace RiverRaid.Generics.GameScene
{
    public class GameLevel : GameScene
    {
        protected string declarationXML;
        protected string descriptionXML;
        protected int heightScreen;
        protected int widthScreen;

        protected int linesNum;
        protected int collumnsNum;
        protected int linesIterator;
        protected int collunmsIterator;
        protected int beginRange;
        protected int endRange;

        protected float speed;
        protected float movement;

        protected bool isScrooling;

        protected LevelDescription levelDescription;
        protected Texture2D texture;
        protected FinalBridge finalBridge;


        private int topHUDHeight = 30;

        public GameLevel(Game game,EnumGameScene currentGameScene, EnumGameScene nextGameScene,
            string declarationXML, string descriptionXML, int widthScreen, int heightScreen)
            :base(game, currentGameScene, nextGameScene)
        {
            this.declarationXML = declarationXML;
            this.descriptionXML = descriptionXML;
            this.widthScreen = widthScreen;
            this.heightScreen = heightScreen;
            this.isScrooling = true;
               

            levelDescription = RiverRaid.Generics.XMLParser.XMLParser.DescriptionParser(Game.Content.RootDirectory + declarationXML,
            Game.Content.RootDirectory + descriptionXML);
            collumnsNum = levelDescription.ScreenWidth / levelDescription[0].TileSize.X;
            linesNum = (levelDescription.ScreenHeight / levelDescription[0].TileSize.Y) + 3;

            endRange = linesNum * collumnsNum;
            beginRange = 0;

            linesIterator = 0;
            collunmsIterator = 0;

            texture = Game.Content.Load<Texture2D>(levelDescription.ImageFile);
            finalBridge = new FinalBridge(Game.Content.Load<Texture2D>(levelDescription.FinalSpriteTexture),
                new Point(800, 160), Vector2.Zero);
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
           if(isScrooling)
                movement += speed;
            if (movement >= 160)
            {
                if (isScrooling)
                {
                    beginRange = beginRange + 5;
                    endRange = endRange + 5;
                    movement = 0;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = (SpriteBatch)this.Game.Services.GetService(typeof(SpriteBatch));

            if (isScrooling)
            {
                    spriteBatch.Begin();
                    linesIterator = 0; collunmsIterator = 0;
                    for (int i = beginRange; i < endRange; i++)
                    {
                        if (linesIterator > linesNum)
                        {
                            linesIterator = 0;
                            collunmsIterator = 0;
                        }
                        if (collunmsIterator >= collumnsNum)
                        {
                            collunmsIterator = 0;
                            linesIterator++;
                        }

                        if (i < levelDescription.ListTiles.Count)
                        {
                            Vector2 position = new Vector2(collunmsIterator * levelDescription[i].TileSize.X, heightScreen - (linesIterator * levelDescription[i].TileSize.Y) + movement);
                            collunmsIterator++;
                            spriteBatch.Draw(texture, position, levelDescription[i].ImageRect, Color.White);
                        }
                        else
                        {
                            float ypos = heightScreen - (160* linesIterator) + movement;
                            finalBridge.Postion = new Vector2(0, ypos);
                            finalBridge.Draw(gameTime, spriteBatch);

                            if (ypos >= 0)
                            {
                                isScrooling = false;
                                break;
                            }
                        }
                        
                    }
                    spriteBatch.End();
            }
            else
            {
                spriteBatch.Begin();
                linesIterator = 0; collunmsIterator = 0;
                for (int i = beginRange; i < levelDescription.ListTiles.Count; i++)
                {
                    if (linesIterator > linesNum)
                    {
                        linesIterator = 0;
                        collunmsIterator = 0;
                    }
                    if (collunmsIterator >= collumnsNum)
                    {
                        collunmsIterator = 0;
                        linesIterator++;
                    }
                    if (i < levelDescription.ListTiles.Count)
                    {
                        Vector2 position = new Vector2(collunmsIterator * levelDescription[i].TileSize.X, heightScreen - (linesIterator * levelDescription[i].TileSize.Y) + movement + topHUDHeight);
                        collunmsIterator++;
                        spriteBatch.Draw(texture, position, levelDescription[i].ImageRect, Color.White);
                    }
                }

                finalBridge.Postion = new Vector2(0, heightScreen - ((linesIterator + 1) * 160) + movement + topHUDHeight);
                finalBridge.Draw(gameTime, spriteBatch);
                spriteBatch.End();
            }
            

            base.Draw(gameTime);
        }

    }
}
