using DesignPatternProjekt.FactoryPatterns;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using DesignPatternProjekt.ComponentPatterns;
using DesignPatternProjekt.CommandPatterns;

namespace DesignPatternProjekt
{
    public class GameWorld : Game
    {
        private static GameWorld instance;
        public Dictionary<string, Texture2D> sprites { get; private set; }
        public static SpriteFont font;
        private List<UIComponent> uiComponents = new List<UIComponent>();
        private Fortress player;
        public bool gameStarted;
        public bool gameEnded;
        public static List<GameObject> LaserList = new List<GameObject>();
        public static List<GameObject> LaserTempList = new List<GameObject>();
        public static List<GameObject> EnemiesList = new List<GameObject>();
        public Rectangle outerRec;

        // Singleton
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

        private List<GameObject> newGameObjects = new List<GameObject>();

        private List<GameObject> destroyedGameObjects = new List<GameObject>();
        public float DeltaTime { get; private set; }
        private GameWorld()
        {
            _graphics = new(this) { PreferredBackBufferWidth = 1920, PreferredBackBufferHeight = 1080 };
            Content.RootDirectory = "Content";
            _graphics.IsFullScreen = true;
            IsMouseVisible = true;
            outerRec = new Rectangle(835, 334, 1920, 300);

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            // Create the map
            GameObject map = new GameObject();
            map.AddComponent<Map>();
            map.AddComponent<SpriteRenderer>();
            gameObjects.Add(map);

            // Create the player
            GameObject playerGo = new GameObject();
            player = playerGo.AddComponent<Fortress>();
            playerGo.AddComponent<SpriteRenderer>();
            gameObjects.Add(playerGo);
            foreach (GameObject go in gameObjects)
            {
                go.Awake();
            }

            // Add commands to the input handler
            InputHandler.Instance.AddButtonDownCommand(Keys.Space, new ShootCommand(player));

            GameState.OnChangeState += ChangeState;

            base.Initialize();
        }
        

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // Create the enemies
            EnemyFactory.SpawnEnemies(ENEMYTYPE.STRONG, 3);
            EnemyFactory.SpawnEnemies(ENEMYTYPE.FAST, 3);
            EnemyFactory.SpawnEnemies(ENEMYTYPE.SLOW, 4);

            // Load the sprites
            foreach (GameObject go in gameObjects)
            {
                go.Start();
            }

            // Load the font
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

            // Update the game objects
            if (gameStarted)
            {
                DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

                foreach (GameObject go in gameObjects)
                {
                    go.Update(gameTime);
                }

                LaserTempList = new List<GameObject>(LaserList);

                // Check for collisions
                foreach (var laser in LaserList)
                {
                    foreach (var enemy in EnemiesList)
                    {
                        float distance = Vector2.Distance(laser.Transform.Position, enemy.Transform.Position);

                        if (distance <= 30)
                        {
                            // Destroy the enemy
                            enemy.Transform.Position = new Vector2(enemy.Transform.Position.X, -60);

                            LaserTempList.Remove(laser);
                            Destroy(laser);
                        }
                    }
                }

                LaserList = new List<GameObject>(LaserTempList);

                player.Rotation();

            }

            // Update the UI components
            foreach (var uiComponent in uiComponents) {
                uiComponent.Update(gameTime);
            }

            InputHandler.Instance.Execute();

            base.Update(gameTime);

            Cleanup();

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            // Draw the game objects
            if (gameStarted)
            {
                foreach (GameObject go in gameObjects)
                {
                    go.Draw(_spriteBatch);
                }

            }
            
            // Draw the end screen
            if (gameEnded)
            {
                gameStarted = false;
                
                _spriteBatch.DrawString(font, "You lost, reopen program to try again.", new Vector2(500, 200), Color.Black);
            }

            // Draw the UI components
            foreach (var uiComponent in uiComponents) {
                uiComponent.Draw(gameTime, _spriteBatch);
            }
            

            _spriteBatch.End();

            base.Draw(gameTime);

        }

        // Clean up the game objects
        private void Cleanup()
        {
            for (int i = 0; i < newGameObjects.Count; i++)
            {
                gameObjects.Add(newGameObjects[i]);
                newGameObjects[i].Awake();
                newGameObjects[i].Start();
            }

            for (int i = 0; i < destroyedGameObjects.Count; i++)
            {
                gameObjects.Remove(destroyedGameObjects[i]);
            }
            destroyedGameObjects.Clear();
            newGameObjects.Clear();
        }

        public void Instantiate(GameObject go)
        {
            newGameObjects.Add(go);
        }

        public void Destroy(GameObject go)
        {
            destroyedGameObjects.Add(go);
        }

        // Change the state of the game
        private void ChangeState() 
        {
            uiComponents.Clear();

            switch (GameState.CurrentState)
            {
                 case GameStates.StartMenu:
                    uiComponents.Add(
                        new Button()
                        {
                            rect = new Rectangle(885, 500, 150, 75),
                            text = "New Game", 
                            Command = new StateChangeCommand(GameStates.Playing)
                        }
                    );
                    break;
                case GameStates.KeybindMenu:
                    break;
                case GameStates.OptionMenu:
                    break;
                case GameStates.Playing:
                    gameStarted = true;
                    break;
                case GameStates.Paused:
                    break;
            }
        }
    }
}
