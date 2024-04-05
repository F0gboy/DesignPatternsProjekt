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
    public class GameObject
    {
        public Transform Transform { get; private set; } = new Transform();

        public List<Component> Components { get; private set; }

        public GameObject()
        {
            Components = new List<Component>();
        }

        //public void AddComponent(Component component)
        //{
        //    Components.Add(component);
        //}
        public T AddComponent<T>(params object[] additionalParameters) where T : Component
        {
            Type componentType = typeof(T);

            // Find constructors with the correct parameter types
            var constructor = componentType.GetConstructors()
                .FirstOrDefault(c =>
                {
                    var parametersInfo = c.GetParameters();
                    if (parametersInfo.Length < 1 + additionalParameters.Length)
                        return false;

                    // Check if the first parameter is of type GameObject
                    if (parametersInfo[0].ParameterType != typeof(GameObject))
                        return false;

                    for (int i = 1; i < parametersInfo.Length; i++)
                    {
                        if (i - 1 < additionalParameters.Length &&
                            parametersInfo[i].ParameterType != additionalParameters[i - 1].GetType())
                        {
                            return false;
                        }
                    }

                    return true;
                });

            if (constructor != null)
            {
                // Opret en instans ved hjælp af den fundne konstruktør og leverede parametre
                object[] allParameters = new object[1 + additionalParameters.Length];
                allParameters[0] = this;
                Array.Copy(additionalParameters, 0, allParameters, 1, additionalParameters.Length);

                T component = (T)Activator.CreateInstance(componentType, allParameters);
                Components.Add(component);
                return component;
            }
            else
            {
                // Håndter tilfælde, hvor der ikke er en passende konstruktør
                throw new InvalidOperationException($"Klassen {componentType.Name} har ikke en konstruktør, der matcher de leverede parametre.");
            }
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
