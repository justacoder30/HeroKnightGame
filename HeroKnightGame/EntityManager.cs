using System;

namespace HeroKnightGame
{
    public class EntityManager
    {
        private Player _player;

        public EntityManager()
        {
            
        }

        public void Update()
        {
            _player.Update();
        }

        public void Draw()
        {
            Globals.SpriteBatch.Begin();
            _player.Draw();
            Globals.SpriteBatch.End();
        }
    }
}
