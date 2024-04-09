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
        
        public Texture2D recTexture;

        public Map(GameObject gameObject) : base(gameObject)
        {
            outerRec = new Rectangle(835, 334, 1920, 300);
        }

        public override void Awake()
        {
            
        }

        public override void Start()
        {
            // Set sprite
            SpriteRenderer spriteRenderer = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            spriteRenderer.SetSprite("DesignPatternMap");

            // Set position and scale
            GameObject.Transform.Position = new Vector2(GameWorld.Instance.Graphics.PreferredBackBufferWidth / 2f, GameWorld.Instance.Graphics.PreferredBackBufferHeight - spriteRenderer.Sprite.Height * 2.2f);
            GameObject.Transform.Scale = new Vector2(4f, 4f);

            // Set the rectangle texture
            recTexture = new Texture2D(GameWorld.Instance.GraphicsDevice, 1, 1);
            recTexture.SetData(new[] { Color.White });
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (recTexture != null)
            {
                 spriteBatch.Draw(recTexture, outerRec, Color.White * 0.5f);
            }
        }
    }
}
