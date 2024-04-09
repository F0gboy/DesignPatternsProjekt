using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternProjekt
{
    class Laser : Component
    {
        private float speed;

        private Vector2 velocity;


        public Laser(GameObject gameObject) : base(gameObject)
        {
            this.speed = 500;
            UpdateVelocity();
            

        }
        public override void Update(GameTime gameTime)
        {
            Move(gameTime);
            UpdateVelocity();
            
        }

        // Move the laser
        private void Move(GameTime gameTime)
        {
            // Normalize the velocity
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

            velocity *= speed;

            GameObject.Transform.Translate(velocity * GameWorld.Instance.DeltaTime);

            // Destroy the laser if it goes out of bounds
            if (GameObject.Transform.Position.Y < 0)
            {
                GameWorld.Instance.Destroy(this.GameObject);
            }
        }

        // Update the velocity of the laser
        public void UpdateVelocity()
        {
            float rotation = GameObject.Transform.Rotation;
            velocity = new Vector2((float)-Math.Cos(rotation), (float)-Math.Sin(rotation)) * speed;
        }
    }
}
