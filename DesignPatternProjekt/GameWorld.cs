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
        public Dictionary<string, Texture2D> sprites { get; private set; }
        public static SpriteFont font;
        private List<UIComponent> uiComponents = new List<UIComponent>();

        public static GameWorld Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameWorld();
                }
                return instance;
            }
        }

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public GraphicsDeviceManager Graphics { get => _graphics; set => _graphics = value; }

        private List<GameObject> gameObjects = new List<GameObject>();

        private GameWorld()
        {
            _graphics = new(this) { PreferredBackBufferWidth = 1920, PreferredBackBufferHeight = 1080 };
            Content.RootDirectory = "Content";
            _graphics.IsFullScreen = true;
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
            GameState.OnChangeState += ChangeState;

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
            
            font = Content.Load<SpriteFont>("Font");

            sprites = new Dictionary<string, Texture2D>()
            {
                { "NineSlice", Content.Load<Texture2D>("Sprites/NineSlice") }
            };

            GameState.CurrentState = GameStates.StartMenu;
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
            foreach (var uiComponent in uiComponents) {
                uiComponent.Update(gameTime);
            }

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
            
            foreach (var uiComponent in uiComponents) {
                uiComponent.Draw(gameTime, _spriteBatch);
            }
            // TODO: Add your drawing code here

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void ChangeState() 
        {
            uiComponents.Clear();

            switch (GameState.CurrentState)
            {
                 case GameStates.StartMenu:
                    uiComponents.Add(
                        new Button()
                        {
                            rect = new Rectangle(500, 500, 200, 100),
                            text = "Hello"
                        }
                    );
                    break;
                case GameStates.KeybindMenu:
                    break;
                case GameStates.OptionMenu:
                    break;
                case GameStates.Playing:
                    break;
                case GameStates.Paused:
                    break;
            }
        }
    }
}
