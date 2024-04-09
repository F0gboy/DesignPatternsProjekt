using DesignPatternProjekt.ComponentPatterns;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternProjekt.FactoryPatterns
{
    public enum ENEMYTYPE { SLOW, FAST, STRONG }

    internal class EnemyFactory : Factory
    {
        private static EnemyFactory instance;

        private Random rnd = new Random();
        public static float DeltaTime;

        public static EnemyFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EnemyFactory();

                }

                return instance;
            }
        }

        public static GameObject Create(ENEMYTYPE type)
        {
            GameObject go = new GameObject();

            SpriteRenderer sr = go.AddComponent<SpriteRenderer>();

            GameWorld.EnemiesList.Add(go);

            // Set the sprite and add the enemy component and speed
            switch (type)
            {
                case ENEMYTYPE.SLOW:
                    sr.SetSprite("Slowzombie");
                    go.AddComponent<Enemy>(15f);
                    break;
                case ENEMYTYPE.FAST:
                    sr.SetSprite("Fastzombie");
                    go.AddComponent<Enemy>(25f);
                    break;
                case ENEMYTYPE.STRONG:
                    sr.SetSprite("Strongzombie");
                    go.AddComponent<Enemy>(35f);

                    break;
            }

            return go;
        }

        public override GameObject Create()
        {
            GameObject go = new GameObject();

            GameWorld.EnemiesList.Add(go);

            SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
            sr.SetSprite("enemyBlue2");
            go.AddComponent<Enemy>();
            return go;
        }

        // Spawn enemies
        public static void SpawnEnemies(ENEMYTYPE type, int count)
        {
            for (int i = 0; i < count; i++)
            {   

                GameWorld.gameObjects.Add(Create(type));
                
            }
        }

       
    }
}
