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
    enum GameState {SettingUp, Running}
    public class Game1 : Game
    {
        //Fields
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Graph mainGraph;
        SpriteFont tileFont;
        Texture2D tileText;
        SpriteFont errorFont;
        int width;
        int height;
        //Keyboard Pressable
        bool keyPressable;
        //Mouse Pressable
        bool canPress;
        AStar pathFinding;
        GameState currentState;
        String typeSetting;
        bool error;
        
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
            //width and height set here
            width = 20;
            height = 17;
            //fullscreen code
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
            //loads content
            tileText = Content.Load<Texture2D>("tile");
            tileFont = Content.Load<SpriteFont>("Arial3");
            errorFont = Content.Load<SpriteFont>("errorFont");
            //create a new graph
            mainGraph = new Graph(tileFont, width, height, tileText);
            //set start and goal
            mainGraph.Start = mainGraph.AllTiles[0, 0];
            mainGraph.Start.Type = "Start";
            mainGraph.Goal = mainGraph.AllTiles[15, 10];
            mainGraph.Goal.Type = "Goal";
            keyPressable = false;

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
            if (Keyboard.GetState().IsKeyDown(Keys.Tab))
            {
                Reset();
            }
            if (currentState == GameState.SettingUp)
            {
                if(keyPressable)
                {
                    if(Keyboard.GetState().GetPressedKeys().Length >0)
                    {
                        if(Keyboard.GetState().IsKeyDown(Keys.O))
                        {
                            typeSetting = "Obstacle";
                        }
                        //Backspace = "Erase"
                        if (Keyboard.GetState().IsKeyDown(Keys.Back))
                        {
                            typeSetting = "Normal";
                        }
                        //S = Start
                        if (Keyboard.GetState().IsKeyDown(Keys.S))
                        {
                            typeSetting = "Start";
                        }
                        //G = Goal
                        if (Keyboard.GetState().IsKeyDown(Keys.G))
                        {
                            typeSetting = "Goal";
                        }
                        //R = Run
                        if (Keyboard.GetState().IsKeyDown(Keys.R))
                        {
                            currentState = GameState.Running;
                        }
                        else
                        {
                            //set different types per tiles, including normal, start, obstacle and goal
                            MouseState currentMouse = Mouse.GetState();
                            if (canPress && currentMouse.LeftButton == ButtonState.Pressed)
                            {
                                for (int x = 0; x < width; x++)
                                {
                                    for (int y = 0; y < height; y++)
                                    {
                                        if (mainGraph.AllTiles[x, y].Rect.Contains(currentMouse.Position))
                                        {
                                            if (typeSetting != "Obstacle" && typeSetting !="Normal" )
                                            {
                                                canPress = false;
                                            }
                                            if (typeSetting == "Start" && mainGraph.Start != mainGraph.AllTiles[x, y])
                                            {
                                                mainGraph.Start = mainGraph.AllTiles[x, y];
                                            }
                                            else if (typeSetting == "Goal" && mainGraph.Goal != mainGraph.AllTiles[x, y])
                                            {
                                                mainGraph.Goal = mainGraph.AllTiles[x, y];
                                            }
                                            else
                                            {
                                                //Don't replace start or goal
                                                if(mainGraph.AllTiles[x,y].Type == "Start" || mainGraph.AllTiles[x,y].Type=="Goal")
                                                {

                                                }
                                                else
                                                    mainGraph.AllTiles[x, y].Type = typeSetting;
                                            }
                                        }
                                    }
                                }
                               
                            }
                            //if mouse unpressed allow press
                            if (currentMouse.LeftButton != ButtonState.Pressed)
                            {
                                canPress = true;
                            }
                        }


                    }
                }
                //if no key pressed
                else if (Keyboard.GetState().GetPressedKeys().Length == 0)
                {
                    keyPressable = true;
                }
            }
            //if running
            else if(currentState == GameState.Running)
            {
                //create a new pathfinding if needed
                if (pathFinding == null)
                {
                    pathFinding = new AStar(mainGraph, "Manhattan");
                }
                //up arrow key
                if (keyPressable && Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    keyPressable = false;
                    //run once
                    if (pathFinding.OneIteration())
                    {
                        //only enters here if no path found or goal found
                        //check if path found
                        if(mainGraph.Goal.Path == null)
                        {
                            error = true;
                            return;
                        }
                        //set path type to path
                        Node current = mainGraph.Goal;
                        mainGraph.Goal.Type = "Goal";
                        while (current.Path != null)
                        {
                            current.Path.Type = "Path";
                            current = current.Path;
                        }
                        mainGraph.Start.Type = "Start";
                    }

                }
                //space key
                else if (keyPressable && Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    keyPressable = false;
                    //run pathfinding
                    pathFinding.Run();
                    //check if path found
                    if (mainGraph.Goal.Path == null)
                    {
                        error = true;
                        return;
                    }
                    //set path type to path
                    Node current = mainGraph.Goal;
                    while (current.Path != null)
                    {
                        current.Path.Type = "Path";
                        current = current.Path;
                    }
                    //reset goal and start types
                    mainGraph.Goal.Type = "Goal";
                    mainGraph.Start.Type = "Start";

                }
                //if no key pressed
                else if (Keyboard.GetState().GetPressedKeys().Length == 0)
                {
                    keyPressable = true;
                }
            }
           
            



            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //reset the background color to black
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            //draw the graph
            mainGraph.Draw(spriteBatch);
            //if no path found
            if (error)
            {
                spriteBatch.DrawString(errorFont, "ERROR NO PATH FOUND", new Vector2(0, GraphicsDevice.Viewport.Height / 2), Color.DarkOrange);
            }
            spriteBatch.End();
          
            base.Draw(gameTime);
        }

        public void Reset()
        {
            //resets all pieces to the starting valeus
            mainGraph = new Graph(tileFont, width, height, tileText);
            mainGraph.Start = mainGraph.AllTiles[0, 0];
            mainGraph.Goal = mainGraph.AllTiles[15, 10];
            keyPressable = false;
            canPress = false;
            pathFinding = null;
            currentState = GameState.SettingUp;
            typeSetting = "Normal";
            error = false;
        }
    }
}
