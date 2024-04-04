using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
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
            GameState.OnChangeState += ChangeState;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
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

            // TODO: Add your update logic here
            foreach (var uiComponent in uiComponents) {
                uiComponent.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here5

            _spriteBatch.Begin();

            foreach (var uiComponent in uiComponents) {
                uiComponent.Draw(gameTime, _spriteBatch);
            }

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
