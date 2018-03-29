using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        bool canPress;
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
            mainGraph = new Graph(28, 20, Content.Load<Texture2D>("tile"));
            canPress = true;
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
            tile = Content.Load<Texture2D>("tile");
            Arial12 = Content.Load<SpriteFont>("Arial12");

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
                int x = currentMouse.X;
                int y = currentMouse.Y;
                canPress = false;
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
            Node temp = new Node(750, 45, tile, "Start");
            spriteBatch.DrawString(Arial12, "Goal", new Vector2(690, 150), Color.White);
            Node Goal = new Node(750, 145, tile, "Goal");
            spriteBatch.DrawString(Arial12, "Obstacle", new Vector2(690, 250), Color.White);
            Node ObstacleMenu = new Node(770, 245, tile, "Obstacle");
            
            spriteBatch.Draw(tile, new Rectangle(685, 290, 100, 50), Color.Blue);
            spriteBatch.DrawString(Arial12, "Run", new Vector2(720, 308), Color.Black);
            temp.Draw(spriteBatch);
            Goal.Draw(spriteBatch);
            ObstacleMenu.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
