using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace HeroKnightGame
{
    public class Player : Model
    {
        private KeyboardState _currentKeySate;
        private KeyboardState _prevKeySate;

        public Player(Texture2D texture, Vector2 position) : base(texture, position) 
        { }

        public Player()
        {
            _falling = true;
            Speed = 180f;
            Gravity = 1000f;
            Jump = 410f;
            OFFSET_Width = 52;
            OFFSET_Height = 42;
            damage = 10;
            HP = 200;

            Position = Map.GetPlayerPosition();

            _animations = new Dictionary<string, Animation>();

            _animations.Add("Idle", new Animation(Globals.Content.Load<Texture2D>("Player/Idle"), 10));
            _animations.Add("Run", new Animation(Globals.Content.Load<Texture2D>("Player/Run"), 10, 0.051f));
            _animations.Add("Jump", new Animation(Globals.Content.Load<Texture2D>("Player/Jump"), 3));
            _animations.Add("Fall", new Animation(Globals.Content.Load<Texture2D>("Player/Fall"), 3));
            _animations.Add("Attack", new Animation(Globals.Content.Load<Texture2D>("Player/Attack"), 6, 0.05f, true));

            _animationManager = new AnimationManager(_animations.First().Value);

            _texture_Width = _animationManager.Animation.FrameWidth;
            _texture_Height = _animationManager.Animation.FrameHeight;
        }

        private void ApplyGravity()
        {
            Vector2 newVelocity = new Vector2();
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
        }

        private void UpdateVelocity()
        {
            _prevKeySate = _currentKeySate;
            _currentKeySate = Keyboard.GetState();

            velocity.X *= 0f;

            if (_currentKeySate.IsKeyDown(Keys.D)) 
            {
                velocity.X = Speed;
            }
            if (_currentKeySate.IsKeyDown(Keys.A))
            {
                velocity.X = -Speed;
            }

            if (_currentKeySate.IsKeyDown(Keys.Space) && !_falling)
            {
                velocity.Y = -Jump;
                _falling = true;
            }

            ApplyGravity();
        }

        protected void UpdatePosition()
        {
            
            Vector2 newPos = Position + velocity * Globals.Time;
            Rectangle newRect;

            foreach (var collider in Map.GetHolderCollision)
            {
                if (newPos.Y > Position.Y)
                {
                    newRect = CalculateBounds(new(Position.X, newPos.Y));

                    if (newRect.Intersects(collider))
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

        public void IsAttacking()
        {
            var rect = GetAttackBound();

            for (int i = 0; i < EnemyManager.enemies.Count; i++)
            {
                if (rect.Intersects(EnemyManager.enemies[i].CalculateBounds()))
                {
                    EnemyManager.enemies[i].IsBeingHit(damage);
                    return;
                }
            }
        }

        private void UpdateAnimation()
        {
            if (velocity.X > 0) _effect = SpriteEffects.None;
            else if (velocity.X < 0) _effect = SpriteEffects.FlipHorizontally;

            if (velocity.Y == 0)
            {
                if (velocity.X != 0)
                { 
                    _state = CharacterState.Run;
                }
                else 
                {
                    if (_currentKeySate.IsKeyDown(Keys.J) && _prevKeySate.IsKeyDown(Keys.J) && !_animationManager.IsAnimationRunning)
                    {
                        _state = CharacterState.Attack;
                        _animationManager.IsAnimationRunning = true;
                        IsAttacking();
                    }
                    if (_state == CharacterState.Attack && _animationManager.IsAnimationRunning)
                    {
                        _state = CharacterState.Attack;
                    }
                    else _state = CharacterState.Idle;
                }
            }
            else if (velocity.Y < 0) _state = CharacterState.Jump;
            else if (velocity.Y > 0) _state = CharacterState.Fall;
        }

        private void SetAnimtion()
        {
            _animationManager.Update();

            UpdateAnimation();

            switch (_state)
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
        }

        public new void Draw()
        {
            base.Draw();
        }
    }
}