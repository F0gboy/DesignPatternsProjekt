using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternProjekt
{
    public class GameObject
    {
        private List<Component> components = new List<Component>();

        public Transform Transform { get; private set; } = new Transform();

        public string Tag { get; set; }


        public T AddComponent<T>() where T : Component
        {
            Type componentType = typeof(T);
            // Get All constructors
            var constructors = componentType.GetConstructors();
            // Find COnstructor with exacly 1 param that is a GameObject
            var constructor = constructors.FirstOrDefault(c =>
            {
                var parameters = c.GetParameters();
                return parameters.Length == 1 && parameters[0].ParameterType == typeof(GameObject);
            });
            if (constructor != null)
            {
                //Create instance of component using the Activator Class with This GameObject as A parameter.
                T component = (T)Activator.CreateInstance(componentType, this);
                components.Add(component);
                return component;
            }
            else
            {
                //Error handling...
                throw new InvalidOperationException($"Klassen {componentType.Name} skal have en konstruktør med ét parameter af typen GameObject.");
            }
        }
        public Component AddComponentWithExistingValues(Component component)
        {
            components.Add(component);
            return component;
        }

        public Component GetComponent<T>() where T : Component
        {
            return components.Find(x => x.GetType() == typeof(T));
        }

        public void Awake()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Awake();
            }
        }

        public void Start()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Start();
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Draw(spriteBatch);
            }
        }
        public object Clone()
        {
            GameObject go = new GameObject();
            foreach (Component component in components)
            {
                Component newComponent = go.AddComponentWithExistingValues(component.Clone() as Component);
                newComponent.SetNewGameObject(go);
            }
            return go;

        }
    }
}