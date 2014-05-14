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

using RiverRaid.Generics.SpritesHandle;
using RiverRaid.Generics.GameScene;
using RiverRaid.GameScenes;


namespace RiverRaid
{
    enum PlayerAnimate{STOP = 0, FORWARD = 1, LEFT = 2, RIGHT = 3};
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        GameScene introduction, level01, gameOver, winner;
        LevelScene gameLevel;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;

            //graphics.IsFullScreen = true;
            this.TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 90);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            GameSounds.Sounds.getSingleton().Play("menu1");


            GameSounds.Sounds.getSingleton().Play("explosionPlay");
            GameSounds.Sounds.getSingleton().Pause("explosionPlay");


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), spriteBatch);

            introduction = new WaintingScene(this, EnumGameScene.INTRODUCTION, EnumGameScene.APRESENTATION_LEVEL01, 
                Content.Load<Texture2D>(@"Images\Apresetation\apresetation"));
            level01 = new WaintingScene(this, EnumGameScene.APRESENTATION_LEVEL01, EnumGameScene.LEVEL01, 
                Content.Load<Texture2D>(@"Images\Apresetation\Level01"));

            gameLevel = new LevelScene(this, EnumGameScene.LEVEL01, EnumGameScene.WINNER,
                @"\XML\declaration\LevelDeclaration.xml", @"\XML\description\LeveDescription.xml", 800, 600);

            gameOver = new GameOverScene(this, EnumGameScene.GAMEOVER, EnumGameScene.GAMEOVER, 
                Content.Load<Texture2D>("gameOver"));

            winner = new GameOverScene(this, EnumGameScene.WINNER, EnumGameScene.WINNER,
                Content.Load<Texture2D>("congratulations"));

            winner.Visible = false;
            winner.Enabled = false;
            
            gameOver.Visible = false;
            gameOver.Enabled = false;

            gameLevel.Visible = false;
            gameLevel.Enabled = false;

            level01.Visible = false;
            level01.Enabled = false;

            this.Components.Add(introduction);
            this.Components.Add(level01);
            this.Components.Add(gameLevel);
            this.Components.Add(gameOver);
            this.Components.Add(winner);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
