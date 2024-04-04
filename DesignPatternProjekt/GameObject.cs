using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatternProjekt.ComponentPatterns;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DesignPatternProjekt
{
    internal class GameObject
    {
        public Transform Transform { get; private set; } = new Transform();

        public List<Component> Components { get; private set; }

        public GameObject()
        {
            Components = new List<Component>();
        }

        public Component AddComponent(Component component)
        {
            Components.Add(component);
            return component;
        }

        public void RemoveComponent(Component component)
        {
            Components.Remove(component);
        }

        public T GetComponent<T>() where T : Component
        {
            foreach (Component component in Components)
            {
                if (component is T)
                { 
                    return (T)component;
                }
            }
            return null;
        }

        public void Update(GameTime gameTime)
        {
            foreach (Component component in Components)
            {
                component.Update(gameTime);
            }
        }

        public void Start()
        {
            foreach (Component component in Components)
            {
                component.Start();
            }
        }

        public void Awake()
        {
            foreach (Component component in Components)
            {
                component.Awake();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Component component in Components)
            {
                component.Draw(spriteBatch);
            }
        }
    }
}
