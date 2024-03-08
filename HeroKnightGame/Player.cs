using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeroKnightGame
{
    public class Player : Sprite
    {
        private Vector2 velocity;
        private Vector2 newVelocity;
        private PlayerState _player;
        private SpriteEffects _effect = SpriteEffects.None;
        private const float Speed = 250f;
        private const float Gravity = 1000f;
        private const float Jump = 550f;
        private bool _falling = true;
        private int _texture_Width;
        private int _texture_Height;

        public Player(Texture2D texture, Vector2 position) : base(texture, position) 
        { }

        public Player(Vector2 postion)
        {
            Position = postion;

            _animations = new Dictionary<string, Animation>();
            _animations.Add("Idle", new Animation(Globals.Content.Load<Texture2D>("Idle"), 11));
            _animations.Add("Run", new Animation(Globals.Content.Load<Texture2D>("Run"), 12));
            _animations.Add("Jump", new Animation(Globals.Content.Load<Texture2D>("Jump"), 1));
            _animations.Add("Fall", new Animation(Globals.Content.Load<Texture2D>("Fall"), 1));

            _animationManager = new AnimationManager(_animations.First().Value);

            _texture_Width = _animationManager.Animation.FrameWidth;
            _texture_Height = _animationManager.Animation.FrameHeight;
        }

        private Rectangle CalculateBounds(Vector2 pos)
        {
            return new((int)pos.X, (int)pos.Y, _texture_Width, _texture_Height);
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
            //_falling = true;
        }

        private void UpdateVelocity()
        {
            var KeyState = Keyboard.GetState();

            velocity.X *= 0.95f;

            if (KeyState.IsKeyDown(Keys.D)) 
            {
                velocity.X = Speed;
            }
            if (KeyState.IsKeyDown(Keys.A))
            {
                velocity.X = -Speed;
            }

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
                        if (newPos.X > Position.X) newPos.X = collider.Left - _texture_Width + 0;
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
                            newPos.Y = collider.Top - _texture_Height;
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

        private void UpdateAnimation()
        { 
            _player = PlayerState.Idle;
            if(velocity.Y == 0 )
            {
                if (velocity.X == Speed)
                {
                    _player = PlayerState.Run;
                    _effect = SpriteEffects.None;
                }
                else if (velocity.X == -Speed)
                {
                    _player = PlayerState.Run;
                    _effect = SpriteEffects.FlipHorizontally;
                }
                if (velocity.X < Speed && velocity.X > 0 || velocity.X > -Speed && velocity.X < 0) 
                {
                    _player = PlayerState.Idle;
                }
            }
            else
            {
                if (velocity.X > 0) _effect = SpriteEffects.None;
                if (velocity.X < 0) _effect = SpriteEffects.FlipHorizontally;
                if (velocity.Y > 0) _player = PlayerState.Fall;
                if (velocity.Y < 0) _player = PlayerState.Jump;
            }
        }

        private void SetAnimtion()
        {
            UpdateAnimation();

            switch(_player)
            {
                case PlayerState.Idle:
                    _animationManager.Play(_animations["Idle"]);
                    break;
                case PlayerState.Run:
                    _animationManager.Play(_animations["Run"]);
                    break;
                case PlayerState.Jump:
                    _animationManager.Play(_animations["Jump"]);
                    break;
                case PlayerState.Fall:
                    _animationManager.Play(_animations["Fall"]);
                    break;
            }
            
        }

        public void Update()
        {
            UpdateVelocity();
            UpdatePosition();
            SetAnimtion();
            _animationManager.Update();
        }

        public new void Draw()
        {
            Globals.SpriteBatch.Draw(_animationManager.Animation.Texture,
                                    Position,
                                    _animationManager.Rect(),
                                    Color.White,
                                    0f,
                                    Vector2.One,
                                    1f,
                                    _effect,
                                    0f
                                    );
        }
    }
}