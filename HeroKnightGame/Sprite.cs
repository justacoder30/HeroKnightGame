using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeroKnightGame
{
    public class Sprite
    {
        public Texture2D _texture;
        public Vector2 _position;

        public Sprite(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            _position = position;
        }
        
        public void Draw()
        {
            Globals.SpriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}
