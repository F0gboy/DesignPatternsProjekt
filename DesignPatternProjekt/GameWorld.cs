using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using DesignPatternProjekt.ComponentPatterns;

namespace DesignPatternProjekt
{
    public class GameWorld : Game
    {
        private static GameWorld instance;

        public static GameWorld Instance
        {
            get
            {
                if (instance == null)
                {
                    Debug.WriteLine("hello");
                    instance = new GameWorld();
                }
                return instance;
            }
        }

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public GraphicsDeviceManager Graphics { get => _graphics; set => _graphics = value; }

        private List<GameObject> gameObjects = new List<GameObject>();

        public GameWorld()
        {
            Graphics = new GraphicsDeviceManager(this);
            Graphics.PreferredBackBufferHeight = 1080;
            Graphics.PreferredBackBufferWidth = 1920;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            GameObject map = new GameObject();
            map.AddComponent(new Map(map, Graphics));
            map.AddComponent(new SpriteRenderer(map));
            gameObjects.Add(map);

            foreach (GameObject go in gameObjects)
            {
                go.Awake();
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            foreach (GameObject go in gameObjects)
            {
                go.Start();
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (GameObject go in gameObjects)
            {
                go.Update(gameTime);
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            foreach (GameObject go in gameObjects)
            {
                go.Draw(_spriteBatch);
            }
            // TODO: Add your drawing code here

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}