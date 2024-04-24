using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace HeroKnightGame
{
    public class Enemy : Model
    {
        private float _timeChange;
        private float _timer;

        public Enemy(Texture2D texture, Vector2 position) : base(texture, position)
        { }

        public Enemy(Vector2 position)
        {
            Speed = 40f;
            OFFSET_Width = 39;
            OFFSET_Height = 18;
            HP = 30;

            Position = position;

            _animations = new Dictionary<string, Animation>();

            _animations.Add("Idle", new Animation(Globals.Content.Load<Texture2D>("Enemy/Idle"), 8));
            _animations.Add("Walk", new Animation(Globals.Content.Load<Texture2D>("Enemy/Walk"), 10));
            _animations.Add("Attack", new Animation(Globals.Content.Load<Texture2D>("Enemy/Attack"), 10));
            _animations.Add("Death", new Animation(Globals.Content.Load<Texture2D>("Enemy/Death"), 13, 0.08f, true));
            _animations.Add("Hit", new Animation(Globals.Content.Load<Texture2D>("Enemy/Hit"), 5, 0.08f, true));

            _animationManager = new AnimationManager(_animations.First().Value);

            _texture_Width = _animationManager.Animation.FrameWidth;
            _texture_Height = _animationManager.Animation.FrameHeight;

            Random random = new Random();
            _timeChange = random.Next(3, 6);

        }

        public float GetPlayerDistance(Player player)
        {
            return (float)Math.Sqrt((float)Math.Pow(player.Position.X - Position.X, 2) + (float)Math.Pow(player.Position.Y - Position.Y, 2));
        }

        public bool IsAttackRange(Player player)
        {
            if (GetPlayerDistance(player) <= _texture_Width/2)
            {
                if(player.Position.X - Position.X >= 0) _effect = SpriteEffects.None;
                else _effect = SpriteEffects.FlipHorizontally;
                return true;
            }
            return false;
        }

        public void IsAttacking()
        {
            
        }

        private void UpdateVelocity()
        {
            _timer += Globals.Time;

            if(velocity.X != 0 && _timer > _timeChange)
            {
                velocity.X = 0;
                _timer = 0;
            }
            else if (velocity.X == 0 && _timer > _timeChange)
            {
                if(_effect == SpriteEffects.None) velocity.X = Speed;
                else velocity.X = -Speed;
                _timer = 0;
            }
        }

        private void UpdatePosition()
        {
            Vector2 newPos = Position + velocity * Globals.Time;
            Rectangle newRect;

            foreach (var collider in Map.GetEnemyCollision)
            {
                if (newPos.X != Position.X)
                {
                    newRect = CalculateBounds(new(newPos.X, Position.Y));
                    if (newRect.Intersects(collider))
                    {
                        velocity.X *= -1;
                        continue;
                    }
                }
            }

            Position += velocity * Globals.Time;
        }
       

        private void UpdateAnimation(Player player)
        {
            if (!_animationManager.IsAnimationRunning && _state == CharacterState.Death)
            {
                IsRemoved = true;
                return;
            }
            if (_state == CharacterState.Death || HP<=0)
            {
                _state = CharacterState.Death;
                velocity.X = 0;
                return;
            }
            if (_animationManager.IsAnimationRunning || BeingHit)
            {
                _state = CharacterState.Hit;
                velocity.X = 0;
                BeingHit = false;
                return;
            }
            if(IsAttackRange(player))
            {
                _state = CharacterState.Attack;
                velocity.X = 0;
                return;
            }
            if (velocity.X != 0)
            {
                if (velocity.X > 0) _effect = SpriteEffects.None;
                if (velocity.X < 0) _effect = SpriteEffects.FlipHorizontally;
                _state = CharacterState.Walk;
            }
            else if (velocity.X == 0)
            {
                _state = CharacterState.Idle;
            }
            
        }

        private void SetAnimtion(Player player)
        {
            _animationManager.Update();

            UpdateAnimation(player);

            switch (_state)
            {
                case CharacterState.Idle:
                    _animationManager.Play(_animations["Idle"]);
                    break;
                case CharacterState.Walk:
                    _animationManager.Play(_animations["Walk"]);
                    break;
                case CharacterState.Attack:
                    _animationManager.Play(_animations["Attack"]);
                    break;
                case CharacterState.Hit:
                    _animationManager.Play(_animations["Hit"]);
                    break;
                case CharacterState.Death:
                    _animationManager.Play(_animations["Death"]);
                    break;
            }

        }

        public void Update(Player player)
        {
            UpdateVelocity();
            UpdatePosition();
            SetAnimtion(player);
        }

        public new void Draw()
        {
            base.Draw();
        }
    }
}
