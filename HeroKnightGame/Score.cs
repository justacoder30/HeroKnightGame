using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeroKnightGame
{
    public static class Score
    {
        private static SpriteFont _spriteFont;
        private static int score;

        //Khoi tao Score
        public static void Intit()
        {
            _spriteFont = Globals.Content.Load<SpriteFont>("Fonts/Font");
            score = 0;
        }

        //Tang diem score
        public static void IncreaseScore()
        {
            score++;
        }

        //Ve score len man hinh
        public static void Draw()
        {
            Globals.SpriteBatch.DrawString(_spriteFont, "Score: " + score, new Vector2(Globals.WindowSize.X / 2, 0), Color.Black);
        }
    }
}
