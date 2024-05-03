using HeroKnightGame.Manager;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Numerics;

namespace HeroKnightGame
{
    public class EntityManager
    {
        private Player _player;
        private EnemyManager _enemyManager;
        private CoinManger _coinManger;
        private Flag _flag;
        private Map _map;
        private Camera _camera;
        private Sprite _backGround;
        private Heart _heart;
        public static bool IsEndGame;

        public EntityManager()
        {
            _map = new Map();
            _heart = new Heart();
            _player = new Player();
            _flag = new Flag();
            _enemyManager = new EnemyManager();
            _coinManger = new CoinManger();
            _camera = new Camera();
            Score.Intit();
            _backGround = new Sprite(Globals.Content.Load<Texture2D>("Background/BackgroundGame"), new(0, 0));

            SoundManager.PlayMusic();
        }

        public void Update()
        {
            _player.Update();
            _enemyManager.Update(ref _player);
            _coinManger.Update(_player);
            _flag.Update(_player);
            _camera.FollowPLayer(_player);
            _heart.Update(_player.HP);
        }

        public void Draw()
        {
            Globals.SpriteBatch.Begin();
            _backGround.Draw();
            Globals.SpriteBatch.End();

            Globals.SpriteBatch.Begin(transformMatrix: _camera.Translation);
            _map.Draw();
            _flag.Draw();
            _enemyManager.Draw();
            _coinManger.Draw();    
            _player.Draw();
            Globals.SpriteBatch.End();

            Globals.SpriteBatch.Begin();
            _heart.Draw();
            Score.Draw();
            Globals.SpriteBatch.End();
        }
    }
}
