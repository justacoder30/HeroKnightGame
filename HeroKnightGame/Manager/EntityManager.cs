﻿using HeroKnightGame.Model;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace HeroKnightGame
{
    public class EntityManager
    {
        private Renderer _render;
        private Player _player;
        private Enemy _enemy;
        private Map _map;
        private Camera _camera;
        private Sprite _backGround;

        public EntityManager()
        {
            _render = new Renderer();
            _map = new Map();
            _player = new Player();
            _enemy = new Enemy(new (110, 1052));
            _render.SetResolution(1920, 1080);
            _camera = new Camera();
            _backGround = new Sprite(Globals.Content.Load<Texture2D>("Background"), new(0, 0));
        }

        public void Update()
        {
            InputManager.Update();
            _player.Update();
            _enemy.Update();    
            _camera.FollowPLayer(_player);
        }

        public void Draw()
        {
            
            _render.Activate();
            Globals.SpriteBatch.Begin();
            _backGround.Draw();
            Globals.SpriteBatch.End();
            //transformMatrix: _camera.Traslation
            Globals.SpriteBatch.Begin(transformMatrix: _camera.Translation);
            _map.Draw();
            _enemy.Draw();
            _player.Draw();
            Globals.SpriteBatch.End();

            _render.Draw();
        }
    }
}
