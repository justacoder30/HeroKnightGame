using HeroKnightGame.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HeroKnightGame
{
    public class RunningState : GameState
    {
        //Khoi tao cac entity cho game
        public RunningState(KnightGame game) : base(game) 
        {
            _entityManager = new EntityManager();
            _game.IsMouseVisible = false;
        }

        //Cap nhap xu ly gameplay
        public override void Update()
        {
            //Cap nhap cac entity 
            _entityManager.Update();

            //Khi nhan nut Escape thi` se chuyen sang man hinh Pause Screen
            if (InputManager.CurrentKeySate.IsKeyDown(Keys.Escape) && InputManager.PrevKeySate.IsKeyUp(Keys.Escape))
            {
                _game.SaveCurrentState();
                _game.ChangeNextState(new PauseState(_game));
            }

            //Khi ket thuc game se chuyen vao man hinh EndGameScreen
            if(EntityManager.IsEndGame)
            {
                SoundManager.StopMusic();
                _game.ChangeNextState(new EndState(_game));
            }
        }

        //Vẽ các enity 
        public override void Draw()
        {
            _entityManager.Draw();
        }
    }
}
