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

        
        //adds a component to the gameobject
        public T AddComponent<T>(params object[] additionalParameters) where T : Component
        {
            Type componentType = typeof(T);

            // Find the constructor that matches the parameters
            var constructor = componentType.GetConstructors()
                .FirstOrDefault(c =>
                {
                    var parametersInfo = c.GetParameters();
                    if (parametersInfo.Length < 1 + additionalParameters.Length)
                        return false;

                    if (parametersInfo[0].ParameterType != typeof(GameObject))
                        return false;

                    // Check if the rest of the parameters match
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

            // If a constructor was found, create the component
            if (constructor != null)
            {

                object[] allParameters = new object[1 + additionalParameters.Length];
                allParameters[0] = this;
                Array.Copy(additionalParameters, 0, allParameters, 1, additionalParameters.Length);

                // Create the component
                T component = (T)Activator.CreateInstance(componentType, allParameters);
                Components.Add(component);
                return component;
            }
            else
            {
                // If no constructor was found, throw an exception
                throw new InvalidOperationException($"Klassen {componentType.Name} har ikke en konstruktør, der matcher de leverede parametre.");
            }
        }

        //removes a component from the gameobject
        public void RemoveComponent(Component component)
        {
            Components.Remove(component);
        }

        //returns a component of type T
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
        //adds a component with existing values
        public Component AddComponentWithExistingValues(Component component)
        {
                Components.Add(component);
                return component;
        }

        public void Awake()
        { 
            for (int i = 0; i < Components.Count; i++)
            {
                    Components[i].Awake();
            }
        }

        public void Start()
        {
            for (int i = 0; i < Components.Count; i++)
            {
                    Components[i].Start();
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < Components.Count; i++)
            {
                    Components[i].Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Components.Count; i++)
            {
                    Components[i].Draw(spriteBatch);
            }
        }
        //clones the gameobject
        public object Clone()
        {
            GameObject go = new GameObject();
            foreach (Component component in Components)
            { 
                Component newComponent = go.AddComponentWithExistingValues(component.Clone() as Component);
                newComponent.SetNewGameObject(go);
            }
            return go;

            }
        }
}
