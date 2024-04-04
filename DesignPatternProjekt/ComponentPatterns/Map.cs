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
        public Map(GameObject gameObject) : base(gameObject)
        {
        }

        public override void Awake()
        {
            
        }

        public override void Start()
        {
            SpriteRenderer spriteRenderer = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            spriteRenderer.SetSprite("DesignPatternMap");

            GameObject.Transform.Position = new Vector2(GameWorld.Instance.Graphics.PreferredBackBufferWidth / 2f, GameWorld.Instance.Graphics.PreferredBackBufferHeight - spriteRenderer.Sprite.Height * 2.2f);
            GameObject.Transform.Scale = new Vector2(4f, 4f);
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
