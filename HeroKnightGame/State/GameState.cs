using Microsoft.Xna.Framework;

namespace HeroKnightGame
{
    public abstract class GameState
    {
        protected KnightGame _game;
        protected EntityManager _entityManager;

        public GameState() { }

        public GameState(KnightGame game)
        {
            _game = game;
        }

        public abstract void Update();

        public abstract void Draw();
    }
}
