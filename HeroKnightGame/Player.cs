using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Numerics;

namespace HeroKnightGame
{
    public class Player : Sprite
    {
        private Vector2 velocity;
        public const int Speed = 800;
        
        public Player(Texture2D texture, Vector2 position) : base(texture, position) 
        { }

        public void Update()
        {
            var KeyState = Keyboard.GetState();

            velocity = Vector2.Zero;

            if (KeyState.IsKeyDown(Keys.W)) velocity.Y--;
            if (KeyState.IsKeyDown(Keys.S)) velocity.Y++;
            if (KeyState.IsKeyDown(Keys.D)) velocity.X++;
            if (KeyState.IsKeyDown(Keys.A)) velocity.X--;

            _position += velocity * Globals.Time;
        }
    }
}
