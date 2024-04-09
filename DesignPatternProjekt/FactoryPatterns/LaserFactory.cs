using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternProjekt
{
    enum LASERTYPE { PLAYER, ENEMY }
    class LaserFactory : Factory
    {

        private static LaserFactory instance;

        // Singleton pattern
        public static LaserFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LaserFactory();
                }
                return instance;
            }
        }

        private GameObject prototype;

        private LaserFactory()
        {
            prototype = new GameObject();
            SpriteRenderer sr = prototype.AddComponent<SpriteRenderer>();
            sr.SetSprite("Projectile1");
            prototype.AddComponent<Laser>();
        }

        public override GameObject Create()
        {
            return (GameObject)prototype.Clone();
        }
    }
}
