using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace DesignPatternProjekt.ComponentPatterns
{
    internal class Enemy : Component
    {
        private float speed;
        private Vector2 velocity;

        public Enemy(GameObject gameObject) : base(gameObject)
        {
        }
        public Enemy(GameObject gameObject, float speed) : base(gameObject)
        {
            this.speed = speed;

            velocity = new Vector2(0, 1);
            GameObject.Transform.Position = RandomPositionOutsideTopOfBounds();
        }

        public override void Update(GameTime gameTime)
        {
            Move();
        }
        Vector2 RandomPositionOutsideTopOfBounds()
        {
            Random rnd = new Random();
            return new Vector2(rnd.Next(0, GameWorld.Instance.GraphicsDevice.Viewport.Width), -((SpriteRenderer)GameObject.GetComponent<SpriteRenderer>()).Sprite.Height / 2);
        }
        private void Move()
        {
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

            velocity *= speed;

            GameObject.Transform.Translate(velocity * GameWorld.DeltaTime);

            if (GameObject.Transform.Position.Y > GameWorld.Instance.GraphicsDevice.Viewport.Height)
            {
                GameObject.Transform.Position = RandomPositionOutsideTopOfBounds();
            }
        }
    }
}
