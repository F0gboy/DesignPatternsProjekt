using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DesignPatternProjekt.ComponentPatterns
{
    internal class Map : Component
    {
        public Rectangle outerRec;
        public Rectangle innerRec;
        public Texture2D recTexture;
        private GraphicsDeviceManager graphicsDeviceManager;

        public Map(GameObject gameObject) : base(gameObject)
        {
            //this.graphicsDeviceManager = graphics;
        }

        public override void Awake()
        {
            
        }

        public override void Start()
        {
            SpriteRenderer spriteRenderer = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            spriteRenderer.SetSprite("DesignPatternMap");

            //recTexture = new Texture2D(graphicsDeviceManager.GraphicsDevice, 80, 30);

            GameObject.Transform.Position = new Vector2(GameWorld.Instance.Graphics.PreferredBackBufferWidth / 2f, GameWorld.Instance.Graphics.PreferredBackBufferHeight - spriteRenderer.Sprite.Height * 2.2f);
            GameObject.Transform.Scale = new Vector2(4f, 4f);
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(recTexture, new Rectangle(100, 100, 100, 100), Color.White);
        }
    }
}
