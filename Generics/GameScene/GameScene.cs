using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace RiverRaid.Generics.GameScene
{

     
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public abstract class GameScene : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected enum ShouldUpdate { YES, NO };
        protected EnumGameScene currentGameScene;
        protected EnumGameScene nextGameScene;
        protected ShouldUpdate shouldUpdate;

        public GameScene(Game game, EnumGameScene currentGameScene, EnumGameScene nextGameScene)
            : base(game)
        {
            this.currentGameScene = currentGameScene;
            this.nextGameScene = nextGameScene;
            shouldUpdate = ShouldUpdate.NO;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public void changeGameScene()
        {
            GameScene currentScene = (GameScene)this.Game.Components.ElementAt(this.Game.Components.IndexOf(this)) ;
            GameScene nextScene = 
                (GameScene)this.Game.Components.First(gameScene => ((GameScene)gameScene).currentGameScene == this.nextGameScene);

            currentScene.Enabled = false;
            currentScene.Visible = false;
            currentScene.shouldUpdate = ShouldUpdate.NO;
            nextScene.Visible = true;
            nextScene.Enabled = true;
           
            
        }

        public override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
        
    }
}
