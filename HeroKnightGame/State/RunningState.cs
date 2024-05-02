using HeroKnightGame.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HeroKnightGame
{
    public class RunningState : GameState
    {
        public RunningState(KnightGame game) : base(game) 
        {
            _entityManager = new EntityManager();
        }

        public override void Update()
        {
            _entityManager.Update();

            if (InputManager.CurrentKeySate.IsKeyDown(Keys.Escape) && InputManager.PrevKeySate.IsKeyUp(Keys.Escape))
            {
                _game.SaveCurrentState();
                _game.ChangeNextState(new PauseState(_game));
            }

            if(EntityManager.IsEndGame)
            {
                SoundManager.StopMusic();
                _game.ChangeNextState(new EndState(_game));
            }
        }

        public override void Draw()
        {
            _entityManager.Draw();
        }
    }
}
