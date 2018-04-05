using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Graph mainGraph;
        SpriteFont tileFont;
        Texture2D tileText;
        int width;
        int height;
        bool keyPressable;
        AStar pathFinding;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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

            width = 20;
            height = 17;
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
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
            tileText = Content.Load<Texture2D>("tile");
            tileFont = Content.Load<SpriteFont>("Arial3");
            mainGraph = new Graph(tileFont, width, height, tileText);
            mainGraph.Start = mainGraph.AllTiles[0, 0];
            mainGraph.Start.Type = "Start";
            mainGraph.Goal = mainGraph.AllTiles[15, 10];
            mainGraph.Goal.Type = "Goal";
            keyPressable = false;
            pathFinding = new AStar(mainGraph, "Manhattan");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(keyPressable && Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                keyPressable = false;
                if (pathFinding.OneIteration())
                {
                    Node current = mainGraph.Goal;
                    mainGraph.Goal.Type = "Goal";
                    while(current.Path != null)
                    {
                        current.Path.Type = "Path";
                        current = current.Path;
                    }
                    mainGraph.Start.Type = "Start";
                }
                
            }
            else if (keyPressable && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                keyPressable = false;
                pathFinding.Run();
                Node current = mainGraph.Goal;
                while (current.Path != null)
                {
                    current.Path.Type = "Path";
                    current = current.Path;
                }
                mainGraph.Goal.Type = "Goal";
                mainGraph.Start.Type = "Start";

            }
            else if(Keyboard.GetState().GetPressedKeys().Length == 0)
            {
                 keyPressable = true;
            }
            



            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            mainGraph.Draw(spriteBatch);
            spriteBatch.End();
          
            base.Draw(gameTime);
        }
    }
}
