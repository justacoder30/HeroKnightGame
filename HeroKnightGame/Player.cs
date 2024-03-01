using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace HeroKnightGame
{
    public class Player : Sprite
    {
        private Vector2 velocity;
        private const float Speed = 400;
        private const float Gravity = 1600;
        private const float Jump = 700;
        private bool _onground;
        
        public Player(Texture2D texture, Vector2 position) : base(texture, position) 
        { }

        private Rectangle CalculateBounds(Vector2 pos)
        {
            return new((int)pos.X, (int)pos.Y, _texture.Width, _texture.Height);
        }

        public void UpdateVelocity()
        {
            var KeyState = Keyboard.GetState();

            velocity.X = 0;

            if (KeyState.IsKeyDown(Keys.D)) velocity.X = Speed;
            if (KeyState.IsKeyDown(Keys.A)) velocity.X = -Speed;

            if (KeyState.IsKeyDown(Keys.Space) && _onground)
            {
                velocity.Y = -Jump;
                _onground = false;
            }

            velocity.Y += Gravity * Globals.Time; 
        }

        public void UpdatePosition()
        {
            
            Vector2 newPos = Position + velocity * Globals.Time;
            Rectangle newRect;

            foreach (var collider in Map.GetCollision())
            {
                if (newPos.X != Position.X)
                {
                    newRect = CalculateBounds(new(newPos.X, Position.Y));
                    if (newRect.Intersects(collider))
                    {
                        if (newPos.X > Position.X) newPos.X = collider.Left - _texture.Width + 0;
                        else newPos.X = collider.Right - 0;
                        continue;
                    }
                }
                newRect = CalculateBounds(new(Position.X, newPos.Y));
                if (newRect.Intersects(collider))
                {
                    if (velocity.Y > 0)
                    {
                        newPos.Y = collider.Top - _texture.Height;
                        _onground = true;
                        velocity.Y = 0;
                    }
                    else  
                    {
                        newPos.Y = collider.Bottom;
                        velocity.Y = 0;
                    }
                }
            }
            Position = newPos; 
        }

        public void Update()
        {
            UpdateVelocity();
            UpdatePosition();
        }
    }
}
