using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeroKnightGame
{
    public class Sprite
    {
        protected Texture2D _texture;
        public Vector2 Position { get; set; }

        protected Sprite(Texture2D texture, Vector2 position)
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
