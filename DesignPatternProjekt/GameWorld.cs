using DesignPatternProjekt.FactoryPatterns;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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

        public static List<GameObject> gameObjects = new List<GameObject>();
        public static float DeltaTime { get; private set; }
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
            map.AddComponent<Map>();
            map.AddComponent<SpriteRenderer>();
            gameObjects.Add(map);

            foreach (GameObject go in gameObjects)
            {
                go.Awake();
            }
            GameState.OnChangeState += ChangeState;

            //for (int i = 0; i < 7; i++)
            //{
            //    gameObjects.Add(EnemyFactory.Instance.Create(ENEMYTYPE.STRONG));
            //}
            //for (int i = 0; i < 7; i++)
            //{
            //    gameObjects.Add(EnemyFactory.Instance.Create(ENEMYTYPE.SLOW));

            //}
            //for (int i = 0; i < 7; i++)
            //{
            //    gameObjects.Add(EnemyFactory.Instance.Create(ENEMYTYPE.FAST));

            //}




            //EnemyFactory.SpawnEnemies(ENEMYTYPE.STRONG, 2);

            base.Initialize();
        }
        

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            EnemyFactory.SpawnEnemiesWithDelay(ENEMYTYPE.SLOW, 5, 1.5f, 3f);
            EnemyFactory.SpawnEnemiesWithDelay(ENEMYTYPE.FAST, 3, 2f, 4f);
            EnemyFactory.SpawnEnemiesWithDelay(ENEMYTYPE.STRONG, 2, 3f, 5f);

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
            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
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
