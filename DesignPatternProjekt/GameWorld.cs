using DesignPatternProjekt.FactoryPatterns;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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

        public static List<GameObject> gameObjects = new List<GameObject>();
        public static float DeltaTime { get; private set; }
        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            foreach (GameObject go in gameObjects)
            {
                go.Awake();
            }

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
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

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