using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace DesignPatternProjekt.ComponentPatterns
{
    internal class Enemy : Component
    {
        private int hp;
        private float speed;
        private Vector2 velocity;
        private SpriteRenderer spriteRenderer;
        private Texture2D texture;
        public Rectangle HitBox
        {
            get
            {
                return new Rectangle
                    (
                        (int)(GameObject.Transform.Position.X - spriteRenderer.Sprite.Width / 2),
                        (int)(GameObject.Transform.Position.Y - spriteRenderer.Sprite.Height / 2),
                        spriteRenderer.Sprite.Width,
                        spriteRenderer.Sprite.Height
                    );
            }
        }

        public Enemy(GameObject gameObject, float speed) : base(gameObject)
        {
            this.speed = speed;

            // Set the velocity to move downwards
            velocity = new Vector2(0, 1);
            GameObject.Transform.Position = RandomPositionOutsideTopOfBounds();
        }

        public override void Update(GameTime gameTime)
        {
            // End the game if the enemy reaches the bottom of the screen
            if (GameObject.Transform.Position.Y >= 440)
            {
                GameWorld.Instance.gameStarted = false;
                GameWorld.Instance.gameEnded = true;
            }
            else
            {
                Move();
            }
        }

        // Get a random position outside the top of the screen
        Vector2 RandomPositionOutsideTopOfBounds()
        {
            Random rnd = new Random();
            return new Vector2(rnd.Next(0, GameWorld.Instance.GraphicsDevice.Viewport.Width), -((SpriteRenderer)GameObject.GetComponent<SpriteRenderer>()).Sprite.Height / 2);
        }

        // Move the enemy
        private void Move()
        {
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

            velocity *= speed;

            GameObject.Transform.Translate(velocity * GameWorld.Instance.DeltaTime);

            if (GameObject.Transform.Position.Y > GameWorld.Instance.GraphicsDevice.Viewport.Height)
            {
                GameObject.Transform.Position = RandomPositionOutsideTopOfBounds();
            }
        }

        // Draw a rectangle around the enemy
        private void DrawRectangle(Rectangle collisionBox, SpriteBatch spriteBatch)
        {
            Rectangle topLine = new Rectangle(collisionBox.X, collisionBox.Y, collisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(collisionBox.X, collisionBox.Y + collisionBox.Height, collisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(collisionBox.X + collisionBox.Width, collisionBox.Y, 1, collisionBox.Height);
            Rectangle leftLine = new Rectangle(collisionBox.X, collisionBox.Y, 1, collisionBox.Height);

            spriteBatch.Draw(texture, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
        }
        public override void Start()
        {
            spriteRenderer = (SpriteRenderer)GameObject.GetComponent<SpriteRenderer>();
            texture = GameWorld.Instance.Content.Load<Texture2D>("Pixel");

        }
    }
}
