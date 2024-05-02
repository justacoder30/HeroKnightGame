using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace HeroKnightGame
{
    public class Heart : Sprite
    {
        private int _heartcount;

        public Heart()
        {
            _texture = Globals.Content.Load<Texture2D>("Player/hearts");
        }

        public void Update(int healthPoint)
        {
            _heartcount = (healthPoint-1) / 10 + 1;

            if (healthPoint <= 0) _heartcount = 0;
        }

        public new void Draw()
        {
            for(int i=0; i < _heartcount; i++)
            {
                Globals.SpriteBatch.Draw(_texture, new Rectangle(i * _texture.Width/4, 0, _texture.Width/4, _texture.Height/4), Color.White);
            }
        }
    }
}
