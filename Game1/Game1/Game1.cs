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
        Texture2D tile;
        SpriteFont Arial12;
        int width;
        int height;
        bool canPress;
        List<Node> menu;
        Node[,] mainMap;
        String typeSetting;
        AStar temp;
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
            width = 28;
            height = 20;
            mainGraph = new Graph(width, height, Content.Load<Texture2D>("tile"));
            canPress = true;
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            mainMap = mainGraph.allTiles;
            menu = new List<Node>();
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
            tile = Content.Load<Texture2D>("tile");
            Arial12 = Content.Load<SpriteFont>("Arial12");
            menu.Add(new Node(750, 45, tile, "Start"));
            menu.Add(new Node(750, 145, tile, "Goal"));
            menu.Add(new Node(770, 245, tile, "Obstacle"));
            
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
            MouseState currentMouse = Mouse.GetState();
            if(canPress && currentMouse.LeftButton == ButtonState.Pressed)
            {
               for(int x = 0; x<width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if(mainMap[x, y].rect.Contains(currentMouse.Position))
                        {
                            if(typeSetting != "Obstacle")
                            {
                                canPress = false;
                            }
                            if (typeSetting == "Start" && mainGraph.Start != mainMap[x, y])
                            {
                                if (mainGraph.Start != null)
                                    mainGraph.Start.Type = "Normal";
                                mainGraph.Start = mainMap[x, y];
                            }
                            else if (typeSetting == "Goal" && mainGraph.Goal != mainMap[x, y])
                            {
                                if (mainGraph.Goal != null)
                                    mainGraph.Goal.Type = "Normal";

                                mainGraph.Goal = mainMap[x, y];
                                temp = new AStar(mainGraph, "Manhattan");
                                temp.Run();
                                for(int f = 0; f< temp.GetPath().Count; f++)
                                {
                                    temp.GetPath()[f].Type = "Path";
                                }
                            }
                            else
                            {
                                mainMap[x, y].Type = typeSetting;
                            }
                        }
                    }
                }
               foreach(Node n in menu)
                {
                    if(n.rect.Contains(currentMouse.Position))
                    {
                        canPress = false;
                        typeSetting = n.Type;
                    }
                }
            }
            if(currentMouse.LeftButton != ButtonState.Pressed)
            {
                canPress = true;
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            MouseState currentMouse = Mouse.GetState();

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            mainGraph.Draw(spriteBatch);
            spriteBatch.Draw(tile, new Rectangle(672, 0, 200, 600), Color.Black);
            spriteBatch.DrawString(Arial12, "Start", new Vector2(690, 50), Color.White);
            //Node temp = new Node(750, 45, tile, "Start");
            spriteBatch.DrawString(Arial12, "Goal", new Vector2(690, 150), Color.White);
           // Node Goal = new Node(750, 145, tile, "Goal");
            spriteBatch.DrawString(Arial12, "Obstacle", new Vector2(690, 250), Color.White);
            //Node ObstacleMenu = new Node(770, 245, tile, "Obstacle");
            
            spriteBatch.Draw(tile, new Rectangle(685, 290, 100, 50), Color.Blue);
            spriteBatch.DrawString(Arial12, "Run", new Vector2(720, 308), Color.Black);
            foreach(Node n in menu)
            {
                n.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
