using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace DesignPatternProjekt
{
    public abstract class Component
    {
        public bool IsEnabled { get; set; }
        public GameObject GameObject { get; private set; }

        public Component(GameObject gameObject)
        {
            this.GameObject = gameObject;
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
        public virtual object Clone()
        {
            Component component = (Component)MemberwiseClone();
            component.GameObject = GameObject;
            return component;
        }
        public virtual void SetNewGameObject(GameObject gameObject)
        {
            this.GameObject = gameObject;
        }
    }
}
