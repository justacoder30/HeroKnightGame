using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace HeroKnightGame
{
    public class Sprite
    {
        protected Texture2D _texture;
        public Vector2 Position {  get; set; }
        public float TextureWidth { get => _texture.Width; }
        public float TextureHeight {  get => _texture.Height; }
        public Texture2D Texture { get => _texture; }

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
