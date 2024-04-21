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
        private CharacterState _player;
        private SpriteEffects _effect = SpriteEffects.None;
        private const float Speed = 180f;
        private const float Gravity = 1000f;
        private const float Jump = 400f;
        private bool _falling = true;
        private int _texture_Width;
        private int _texture_Height;
        private const int OFFSET_Width = 52;
        private const int OFFSET_Height = 62;
        private KeyboardState _currentKeySate;
        private KeyboardState _prevKeySate;

        public Player(Texture2D texture, Vector2 position) : base(texture, position) 
        { }

        public Player()
        {
            Position = Map.GetPlayerPosition();

            _animations = new Dictionary<string, Animation>();

            _animations.Add("Idle", new Animation(Globals.Content.Load<Texture2D>("Player1/Idle"), 10));
            _animations.Add("Run", new Animation(Globals.Content.Load<Texture2D>("Player1/Run"), 10, 0.06f));
            _animations.Add("Jump", new Animation(Globals.Content.Load<Texture2D>("Player1/Jump"), 3));
            _animations.Add("Fall", new Animation(Globals.Content.Load<Texture2D>("Player1/Fall"), 3));
            _animations.Add("Attack", new Animation(Globals.Content.Load<Texture2D>("Player1/Attack"), 6, 0.05f, true));

            _animationManager = new AnimationManager(_animations.First().Value);

            _texture_Width = _animationManager.Animation.FrameWidth;
            _texture_Height = _animationManager.Animation.FrameHeight;
        }

        private Rectangle CalculateBounds(Vector2 pos)
        {
            return new((int)pos.X + OFFSET_Width, (int)pos.Y + OFFSET_Height, _texture_Width - OFFSET_Width * 2, _texture_Height - OFFSET_Height);
        }

        private void ApplyGravity()
        {
            newVelocity = new Vector2();
            newVelocity.Y = velocity.Y + Gravity * Globals.Time;
            Vector2 newPos = Position + newVelocity * Globals.Time;

            foreach (var collider in Map.GetMapCollision)
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

            foreach (var collider in Map.GetHolderCollision)
            {
                if (newPos.Y != Position.Y)
                {
                    var newRect = CalculateBounds(new(Position.X, newPos.Y));

                    if (newRect.Intersects(collider))
                    {
                        if (newVelocity.Y > 0)
                        {
                            _falling = false;
                            velocity.Y = 0;
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

            velocity.X *= 0f;

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
        }

        private void UpdatePosition()
        {
            
            Vector2 newPos = Position + velocity * Globals.Time;
            Rectangle newRect;

            foreach (var collider in Map.GetHolderCollision)
            {
                newRect = CalculateBounds(new(Position.X, newPos.Y));

                if (newRect.Intersects(collider))
                {
                    if (velocity.Y >= 0)
                    {
                        newPos.Y = collider.Top - _texture_Height;
                        _falling = false;
                        velocity.Y = 0;
                    }
                }
            }

            foreach (var collider in Map.GetMapCollision)
            {
                if (newPos.X != Position.X)
                {
                    newRect = CalculateBounds(new(newPos.X, Position.Y));
                    if (newRect.Intersects(collider))
                    {
                        if (newPos.X > Position.X) newPos.X = collider.Left - _texture_Width + OFFSET_Width;
                        else newPos.X = collider.Right - OFFSET_Width;
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
                            newPos.Y = collider.Bottom - OFFSET_Height;
                            velocity.Y = 0;
                        }
                    }
                }  
            }

            Position = newPos; 
        }

        private void UpdateAnimation()
        {
            var KeyState = Keyboard.GetState();

            if (velocity.X > 0) _effect = SpriteEffects.None;
            else if (velocity.X < 0) _effect = SpriteEffects.FlipHorizontally;

            if (velocity.Y == 0)
            {
                if (velocity.X != 0)
                { 
                    _player = CharacterState.Run;
                }
                else 
                {
                    if (KeyState.IsKeyDown(Keys.J) || _animationManager.IsAnimationRunning)
                    {
                        _player = CharacterState.Attack;
                        _animationManager.IsAnimationRunning = true;
                    }
                    else _player = CharacterState.Idle;
                }
            }
            else if (velocity.Y < 0) _player = CharacterState.Jump;
            else if (velocity.Y > 0) _player = CharacterState.Fall;
        }

        private void SetAnimtion()
        {
            UpdateAnimation();

            switch(_player)
            {
                case CharacterState.Idle:
                    _animationManager.Play(_animations["Idle"]);
                    break;
                case CharacterState.Run:
                    _animationManager.Play(_animations["Run"]);
                    break;
                case CharacterState.Jump:
                    _animationManager.Play(_animations["Jump"]);
                    break;
                case CharacterState.Fall:
                    _animationManager.Play(_animations["Fall"]);
                    break;
                case CharacterState.Attack:
                    _animationManager.Play(_animations["Attack"]);
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
                                    _animationManager.Rect,
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