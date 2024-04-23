using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace HeroKnightGame
{
    public class EnemyManager
    {
        public static List<Enemy> enemies;

        public EnemyManager()
        {
            enemies = new List<Enemy>();

            foreach (var _pos in Map.GetEnemyPosition())
            {
                var enemy = new Enemy(_pos);
                enemies.Add(enemy);
            }
        }

        public void Update()
        {
            for(int i=0; i < enemies.Count; i++)
            {
                enemies[i].Update();
                if (enemies[i].IsRemoved)
                {
                    enemies.RemoveAt(i);
                    i--;
                }

            }


        }

        public void Draw()
        {
            foreach (var _enemies in enemies)
            {
                _enemies.Draw();
            }
        }
    }
}
