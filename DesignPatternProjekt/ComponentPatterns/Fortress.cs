using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternProjekt
{
    class Fortress : Component
    {
        private float speed;
        private MouseState currentMouse;
        private float rotation;
        public static List<GameObject> LaserList = new List<GameObject>();

        public Fortress(GameObject gameObject) : base(gameObject)
        {
        }

        public override void Awake()
        {
            speed = 100;
        }
        public void MoveByAddition(Vector2 velocity)
        {
            GameObject.Transform.Position += velocity;
        }

        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            sr.SetSprite("SoldierSprite");
            GameObject.Transform.Position = new Vector2(GameWorld.Instance.Graphics.PreferredBackBufferWidth / 2, GameWorld.Instance.Graphics.PreferredBackBufferHeight / 2);

        }

        // Rotate the fortress towards the mouse
        public void Rotation()
        {
            currentMouse = Mouse.GetState();

            Vector2 mousePosistion = new Vector2(currentMouse.X, currentMouse.Y);
            Vector2 dPos = GameObject.Transform.Position - mousePosistion;

            // Calculate the rotation
            rotation = (float)Math.Atan2(dPos.Y, dPos.X);
            GameObject.Transform.Rotation = rotation;
            Laser laserComponent = GameObject.GetComponent<Laser>() as Laser;

            if (laserComponent != null)
            {
                laserComponent.UpdateVelocity();
            }
        }

        bool canShoot = true;

        // Shoot a laser
        public void Shoot()
        {
            if (canShoot)
            {
                canShoot = false;
                lastShot = 0;
                GameObject laser = LaserFactory.Instance.Create();
                GameWorld.LaserList.Add(laser);
                laser.Transform.Position = GameObject.Transform.Position;
                laser.Transform.Rotation = GameObject.Transform.Rotation;
                GameWorld.Instance.Instantiate(laser);
            }
        }
        float shootTimer = 1;
        float lastShot = 0;
        public override void Update(GameTime gameTime)
        {
            // Move the fortress
            lastShot += GameWorld.Instance.DeltaTime;
            if (lastShot > shootTimer)
            {
                canShoot = true;
            }
        }
    }

}