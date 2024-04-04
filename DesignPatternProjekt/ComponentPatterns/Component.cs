using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace DesignPatternProjekt.ComponentPatterns
{
    internal class Component
    {
        public GameObject GameObject { get; private set; }

        public Component(GameObject gameObject)
        {
            GameObject = gameObject;
        }

        public virtual void Awake()
        {

        }

        public virtual void Start()
        {
            
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
