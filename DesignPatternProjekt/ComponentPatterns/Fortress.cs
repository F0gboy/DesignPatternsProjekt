using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DesignPatternProjekt.ComponentPatterns
{
    class Player : Component
    {
        //private float speed;

        public Player(GameObject gameObject) : base(gameObject)
        {
        }

        //public void Move(Vector2 velocity)
        //{
        //    if (velocity != Vector2.Zero)
        //    {
        //        velocity.Normalize();
        //    }

        //    velocity *= speed;

        //    GameObject.Transform.Translate(velocity * GameWorld.DeltaTime);
        //}

        public override void Awake()
        {
            speed = 100;
        }
        //public void MoveByAddition(Vector2 velocity)
        //{
        //    GameObject.Transform.Position += velocity;
        //}
        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            sr.SetSprite("SoldierSprite");
            GameObject.Transform.Position = new Vector2(GameWorld.Instance.Graphics.PreferredBackBufferWidth / 2, GameWorld.Instance.Graphics.PreferredBackBufferHeight - sr.Sprite.Height / 3);

        }

        public override void Update(GameTime gameTime)
        {

        }
    }

}