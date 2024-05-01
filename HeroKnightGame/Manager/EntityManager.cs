﻿using HeroKnightGame.Manager;
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
        private Map _map;
        private Camera _camera;
        private Sprite _backGround;
        public static bool IsEndGame;

        public EntityManager()
        {
            _map = new Map();
            _player = new Player();
            _enemyManager = new EnemyManager();
            _coinManger = new CoinManger();
            _camera = new Camera();
            _backGround = new Sprite(Globals.Content.Load<Texture2D>("Background"), new(0, 0));

            SoundManager.PlayMusic();
        }

        public void Update()
        {
            _player.Update();
            _enemyManager.Update(ref _player);
            _coinManger.Update(_player);
            _camera.FollowPLayer(_player);
        }

        public void Draw()
        {
            Globals.SpriteBatch.Begin();
            _backGround.Draw();
            Globals.SpriteBatch.End();

            Globals.SpriteBatch.Begin(transformMatrix: _camera.Translation);
            _map.Draw();
            _enemyManager.Draw();
            _coinManger.Draw();    
            _player.Draw();
            Globals.SpriteBatch.End();
        }
    }
}
