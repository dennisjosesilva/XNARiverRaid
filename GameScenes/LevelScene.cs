using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RiverRaid.Elementes;
using RiverRaid.Generics.GameScene;
using RiverRaid.Generics.SpritesHandle;

namespace RiverRaid.GameScenes
{
    public class LevelScene : GameLevel
    {
        private Player player;
        private AutomatedSprite enemy;
        private bool isPaused;
        private HUD levelHUD; 

         public LevelScene(Game game, EnumGameScene currentGameScene, EnumGameScene nextGameScene,
            string declarationXML, string descriptionXML, int widthScreen, int heightScreen):
             base(game, currentGameScene, nextGameScene, declarationXML, descriptionXML, widthScreen, heightScreen)
         {
             player = new Player(
                 Game.Content.Load<Texture2D>("PlayerSheet-Explosion"),
                 Game.Content.Load<Texture2D>(@"Images/bullet/bullet"), 
                 game.Content.Load<Texture2D>(@"Images/bullet/explosao_player"), new Point(138, 150), 
                 new Point(3, 5),new Vector2((widthScreen/2) - 138, heightScreen - 250), 
                 new Point(40, 20), new Point(widthScreen, heightScreen));

             enemy = new AutomatedSprite(Game.Content.Load<Texture2D>("A-7E_b080"), new Vector2(15, 15));

             levelHUD = new HUD(levelDescription.Name, game.Content.Load<Texture2D>("HUD"), 
                    game.Content.Load<SpriteFont>(@"font\gameFont"), player);

             EnemyFactory.Initialize(Game.Content, new Rectangle(0,0,widthScreen,heightScreen), ref player.speed);

             isPaused = false;
         }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            speed = player.Speed;

            EnemyFactory.Singleton.Uptade(gameTime);

            #region GAMEOVER
            if (player.Lose)
            {
                this.nextGameScene = EnumGameScene.GAMEOVER;
                changeGameScene();
            } 
            #endregion

            #region Ganhando
            if (player.Win)
                changeGameScene(); 
            #endregion

            #region Colisão do player detectada reseta o cenario
            if (player.NeedLevelReset)
            {
                setStartLevelConfig();
                EnemyFactory.Singleton.ResetFactory();
            } 
            #endregion
            
            if(!isPaused)
            {
                #region Não pausado
                #region Detecta colisão com o enimigo
                for (int i = 0; i < EnemyFactory.Singleton.ListEnemy.Count; i++ )
                {
                    EnemyFactory.Singleton.ListEnemy[i].Update(gameTime);

                    if (EnemyFactory.Singleton.ListEnemy[i].IsOutBounds())
                        EnemyFactory.Singleton.ListEnemy[i].Collides();

                    foreach (Bullet bullet in player.Bullets)
                    {
                        if (bullet.CollisionRect.Intersects(EnemyFactory.Singleton.ListEnemy[i].CollisionRect))
                        {
                            bullet.Expode();
                            player.GainPoints(EnemyFactory.Singleton.ListEnemy[i].Points);
                            EnemyFactory.Singleton.ListEnemy[i].Collides();
                            break;
                        }
                    }

                    if (player.CollisionRect.Intersects(EnemyFactory.Singleton.ListEnemy[i].CollisionRect))
                    {
                        player.Collides();
                        isPaused = true;
                        break;
                    }

                    if (EnemyFactory.Singleton.ListEnemy[i].CanRemove)
                        EnemyFactory.Singleton.ListEnemy.RemoveAt(i);

                }
                #endregion

                #region Cenario
                linesIterator = 0; collunmsIterator = 0;
                if (isScrooling)
                {
                    #region Com Scrooling
                    for (int i = beginRange; i <= endRange; i++)
                    {
                        if (i < levelDescription.ListTiles.Count)
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
                            Vector2 position = new Vector2(collunmsIterator * levelDescription[i].TileSize.X, heightScreen - (linesIterator * levelDescription[i].TileSize.Y) + movement);
                            collunmsIterator++;

                            foreach (Rectangle collisionRect in levelDescription.ListTiles[i].CollisionRectList)
                            {
                                Rectangle buffer = collisionRect;
                                Point leftCorner = new Point((int)position.X + buffer.Location.X, (int)position.Y + buffer.Location.Y);
                                buffer.Location = leftCorner;

                                if (buffer.Intersects(player.CollisionRect))
                                {
                                    player.Collides();
                                    isPaused = true;
                                    break;
                                }
                            }
                        }
                        if (isPaused)
                            break;
                    } 
                    #endregion
                }
                else
                {
                    #region Sem Scrooling
                    player.MoveForward();
                    for (int i = beginRange; i < levelDescription.ListTiles.Count; i++)
                    {
                        if (i < levelDescription.ListTiles.Count)
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
                            Vector2 position = new Vector2(collunmsIterator * levelDescription[i].TileSize.X, heightScreen - (linesIterator * levelDescription[i].TileSize.Y) + movement);
                            collunmsIterator++;

                            foreach (Rectangle collisionRect in levelDescription.ListTiles[i].CollisionRectList)
                            {
                                Rectangle buffer = collisionRect;
                                Point leftCorner = new Point((int)position.X + buffer.Location.X, (int)position.Y + buffer.Location.Y);
                                buffer.Location = leftCorner;

                                if (buffer.Intersects(player.CollisionRect))
                                {
                                    player.Collides();
                                    isPaused = true;
                                    break;
                                }
                            }


                            if (player.CollisionRect.Intersects(finalBridge.CollisionRect))
                            {
                                player.Collides();
                                isPaused = true;
                                break;
                            }

                            foreach (Bullet bullet in player.Bullets)
                            {
                                if (bullet.CollisionRect.Intersects(finalBridge.CollisionRect))
                                {
                                    bullet.Expode();
                                    finalBridge.Collides();
                                    break;
                                }
                            }
                        }
                        if (isPaused)
                            break;

                    }
                    #endregion
                }
                base.Update(gameTime);
                #endregion 
                #endregion
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteBatch spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            

            foreach (Enemy aEnemy in EnemyFactory.Singleton.ListEnemy)
                aEnemy.Draw(gameTime, spriteBatch);


            player.Draw(gameTime, spriteBatch);
            levelHUD.Draw(gameTime, spriteBatch);
            
            spriteBatch.End();
        }

        protected void  setStartLevelConfig()
        {
            endRange = linesNum * collumnsNum;
            beginRange = 0;

            finalBridge.Reset();
    
            linesIterator = 0;
            collunmsIterator = 0;

            isScrooling = true;
            isPaused = false;


            GameSounds.Sounds.getSingleton().Stop("menu1");
            //GameSounds.Sounds.getSingleton().Resume("player");
            GameSounds.Sounds.getSingleton().Play("player");
            //GameSounds.Sounds.getSingleton().Resume("fase");
            GameSounds.Sounds.getSingleton().Play("fase");

            player.StartGame();
        }   
        
                
  
    }
}
