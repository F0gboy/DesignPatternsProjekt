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

            switch (type)
            {
                case ENEMYTYPE.SLOW:
                    sr.SetSprite("Slowzombie");
                    go.AddComponent<Enemy>(50f);
                    break;
                case ENEMYTYPE.FAST:
                    sr.SetSprite("Fastzombie");
                    go.AddComponent<Enemy>(100f);
                    break;
                case ENEMYTYPE.STRONG:
                    sr.SetSprite("Strongzombie");
                    go.AddComponent<Enemy>(150f);

                    break;
            }

            return go;
        }
        public override GameObject Create()
        {
            GameObject go = new GameObject();

            SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
            sr.SetSprite("enemyBlue2");
            go.AddComponent<Enemy>();
            return go;
        }



        public static void SpawnEnemies(ENEMYTYPE type, int count)
        {
            for (int i = 0; i < count; i++)
            {
                // Spawn enemy

                GameWorld.gameObjects.Add(Create(type));
                // Place or manage the spawned enemy as needed
            }
        }

        //public static void SpawnEnemiesWithDelay(ENEMYTYPE type, int count, float minDelay, float maxDelay)
        //{
        //    Random random = new Random();
            

        //    //float timeSinceLastSpawn = 0f;

        //    //for (int i = 0; i < count; i++)
        //    //{
        //    //    timeSinceLastSpawn += GameWorld.DeltaTime;

        //    //    if (timeSinceLastSpawn >= minDelay)
        //    //    {
        //    //        // Spawn enemy
        //    //        GameWorld.gameObjects.Add(Create(type));
        //    //        // Place or manage the spawned enemy as needed

        //    //        timeSinceLastSpawn = 0f;
        //    //    }
        //    //}

        //    //float timeSinceLastSpawn = 0f;

        //    //for (int i = 0; i < count; i++)
        //    //{
        //    //    timeSinceLastSpawn += GameWorld.DeltaTime;
        //    //    int minDelayMillis = (int)(minDelay * 1000);
        //    //    int maxDelayMillis = (int)(maxDelay * 1000);
        //    //    if (timeSinceLastSpawn >= random.Next(minDelayMillis, maxDelayMillis) / 1000f)
        //    //    {
        //    //        // Spawn enemy
        //    //        GameWorld.gameObjects.Add(Create(type));
        //    //        // Place or manage the spawned enemy as needed

        //    //        timeSinceLastSpawn = 0f;
        //    //    }
        //    //}
        //    float timeUntilNextSpawn = 0;

        //    for (int i = 0; i < count; i++)
        //    {
        //        timeUntilNextSpawn = minDelay;

        //        while (timeUntilNextSpawn >= 0)
        //        {
                    
        //            timeUntilNextSpawn --;
        //        }

        //        if (timeUntilNextSpawn <= 0)
        //        {
        //            // Spawn enemy
        //            GameWorld.gameObjects.Add(Create(type));
        //            // Place or manage the spawned enemy as needed

        //            // Calculate a random delay within the specified range
        //            //int minDelayMillis = (int)(minDelay * 1000);
        //            //int maxDelayMillis = (int)(maxDelay * 1000);
        //            //timeUntilNextSpawn = random.Next(minDelayMillis, maxDelayMillis) / 1000f;
        //            timeUntilNextSpawn = maxDelay;


        //        }

        //    }


        //}
    }
}
