using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace HeroKnightGame
{
    public class Sprite
    {
        protected AnimationManager _animationManager;
        protected Dictionary<string, Animation> _animations;
        protected Texture2D _texture;
        protected Vector2 Position {  get; set; }

        public Vector2 Orgin
        {
            get => new Vector2(_texture.Width/2, _texture.Height/2);
        }

        public Sprite(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            Position = position;
        }

        protected Sprite() { }
        
        public void Draw()
        {
            Globals.SpriteBatch.Draw(_texture, Position, Color.White);
        }
    }
}
