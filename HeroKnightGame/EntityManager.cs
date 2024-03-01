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

        public EntityManager()
        {
            _render = new Renderer();
            _map = new Map();
            _player = new Player(Globals.Content.Load<Texture2D>("player"), new(100, 1100));
            _render.SetResolution(1920, 1080);
            _camera = new Camera();
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
            //transformMatrix: _camera.Traslation
            Globals.SpriteBatch.Begin(transformMatrix: _camera.Traslation);
            _map.Draw();
            _player.Draw();
            Globals.SpriteBatch.End();

            _render.Draw();
        }
    }
}
