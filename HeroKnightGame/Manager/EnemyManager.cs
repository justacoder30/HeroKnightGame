using HeroKnightGame.Model;
using System;
using System.Collections.Generic;

namespace HeroKnightGame
{
    public class EnemyManager
    {
        List<Enemy> _enemies;

        public EnemyManager()
        {
            _enemies = new List<Enemy>();

            foreach (var _pos in Map.GetEnemyPosition())
            {
                var enemy = new Enemy(_pos);
                _enemies.Add(enemy);
            }
        }

        public void Update()
        {
            foreach(var _enemies in _enemies)
            {
                _enemies.Update();
            }
        }

        public void Draw()
        {
            foreach (var _enemies in _enemies)
            {
                _enemies.Draw();
            }
        }
    }
}
