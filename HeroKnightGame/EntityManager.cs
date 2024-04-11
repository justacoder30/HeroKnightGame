using Microsoft.Xna.Framework.Graphics;
using System;

namespace HeroKnightGame
{
    public class EntityManager
    {
        private Renderer _render;
        private Player _player;
        private Map _map;
        private Camera _camera;
        private Sprite _backGround;

        public EntityManager()
        {
            _render = new Renderer();
            _map = new Map();
            _player = new Player(new(100, 1100));
            _render.SetResolution(1600, 900);
            _camera = new Camera();
            _backGround = new Sprite(Globals.Content.Load<Texture2D>("Background"), new(0, 0));
        }

        public void Update()
        {
            InputManager.Update();
            _player.Update();
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
            _player.Draw();
            Globals.SpriteBatch.End();

            _render.Draw();
        }
    }
}
