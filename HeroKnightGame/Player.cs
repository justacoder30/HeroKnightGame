using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace HeroKnightGame
{
    public class Player : Sprite
    {
        private Vector2 velocity;
        private Vector2 newVelocity;
        private const float Speed = 400f;
        private const float Gravity = 1600f;
        private const float Jump = 700f;
        private bool _falling = true;
        
        public Player(Texture2D texture, Vector2 position) : base(texture, position) 
        { }

        private Rectangle CalculateBounds(Vector2 pos)
        {
            return new((int)pos.X, (int)pos.Y, _texture.Width, _texture.Height);
        }

        private void ApplyGravity()
        {
            newVelocity = new Vector2();
            newVelocity.Y = velocity.Y + Gravity * Globals.Time;
            Vector2 newPos = Position + newVelocity * Globals.Time;

            foreach (var collider in Map.GetCollision())
            {
                if (newPos.Y != Position.Y)
                {
                    var newRect = CalculateBounds(new(Position.X, newPos.Y));
                    if (newRect.Intersects(collider))
                    {
                        if (newVelocity.Y > 0)
                        {
                            velocity.Y = 0;
                            _falling = false;
                            return;
                        }
                    }
                }
            }

            velocity.Y += Gravity * Globals.Time;
            _falling = true;
        }

        private void UpdateVelocity()
        {
            var KeyState = Keyboard.GetState();

            velocity.X = 0;

            if (KeyState.IsKeyDown(Keys.D)) velocity.X = Speed;
            if (KeyState.IsKeyDown(Keys.A)) velocity.X = -Speed;

            if (KeyState.IsKeyDown(Keys.Space) && !_falling)
            {
                velocity.Y = -Jump;
                _falling = true;
            }

            ApplyGravity();
            //velocity.Y += Gravity * Globals.Time; 
        }

        private void UpdatePosition()
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
                if (newPos.Y != Position.Y)
                {
                    newRect = CalculateBounds(new(Position.X, newPos.Y));
                    if (newRect.Intersects(collider))
                    {
                        if (velocity.Y > 0)
                        {
                            newPos.Y = collider.Top - _texture.Height;
                            _falling = false;
                            velocity.Y = 0;
                        }
                        else
                        {
                            newPos.Y = collider.Bottom;
                            velocity.Y = 0;
                        }
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
