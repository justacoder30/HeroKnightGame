using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace HeroKnightGame
{
    public class Enemy : Sprite
    {
        private Vector2 velocity;
        private CharacterState _enemy;
        private SpriteEffects _effect = SpriteEffects.None;
        private const float Speed = 40f;
        private int _texture_Width;
        private int _texture_Height;
        private const int OFFSET_Width = 39;
        private const int OFFSET_Height = 18;
        private float _timeChange;
        private float _timer;
        public bool BeingHit;
        public bool IsRemoved;
        public int HP = 30;

        public Enemy(Texture2D texture, Vector2 position) : base(texture, position)
        { }

        public Enemy(Vector2 position)
        {
            Position = position;

            _animations = new Dictionary<string, Animation>();

            _animations.Add("Idle", new Animation(Globals.Content.Load<Texture2D>("Enemy2/Idle"), 8));
            _animations.Add("Walk", new Animation(Globals.Content.Load<Texture2D>("Enemy2/Walk"), 10));
            _animations.Add("Attack", new Animation(Globals.Content.Load<Texture2D>("Enemy2/Attack"), 10));
            _animations.Add("Death", new Animation(Globals.Content.Load<Texture2D>("Enemy2/Death"), 13, 0.08f, true));
            _animations.Add("Hit", new Animation(Globals.Content.Load<Texture2D>("Enemy2/Hit"), 5, 0.08f, true));

            _animationManager = new AnimationManager(_animations.First().Value);

            _texture_Width = _animationManager.Animation.FrameWidth;
            _texture_Height = _animationManager.Animation.FrameHeight;

            Random random = new Random();
            _timeChange = random.Next(3, 6);

        }

        public Rectangle CalculateBounds(Vector2 pos)
        {
            return new((int)pos.X + OFFSET_Width, (int)pos.Y + OFFSET_Height, _texture_Width - OFFSET_Width * 2, _texture_Height - OFFSET_Height);
        }

        public Rectangle CalculateBounds()
        {
            return new((int)Position.X + OFFSET_Width, (int)Position.Y + OFFSET_Height, _texture_Width - OFFSET_Width * 2, _texture_Height - OFFSET_Height);
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
       

        private void UpdateAnimation()
        {
            if (!_animationManager.IsAnimationRunning && _enemy == CharacterState.Death)
            {
                IsRemoved = true;
                return;
            }
            if (_enemy == CharacterState.Death || HP<=0)
            {
                _enemy = CharacterState.Death;
                velocity.X = 0;
                return;
            }
            if (_animationManager.IsAnimationRunning || BeingHit)
            {
                _enemy = CharacterState.Hit;
                velocity.X = 0;
                BeingHit = false;
                return;
            }
            /*if (HP > 0 && BeingHit)
            {
                _enemy = CharacterState.Hit;
                velocity.X = 0;
                BeingHit = false;
                return;
            }*/

            if (velocity.X != 0)
            {
                if (velocity.X > 0) _effect = SpriteEffects.None;
                if (velocity.X < 0) _effect = SpriteEffects.FlipHorizontally;
                _enemy = CharacterState.Walk;
            }
            else if (velocity.X == 0)
            {
                _enemy = CharacterState.Idle;
            }
            
        }

        private void SetAnimtion()
        {
            _animationManager.Update();

            UpdateAnimation();

            switch (_enemy)
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

        public void Update()
        {
            UpdateVelocity();
            UpdatePosition();
            SetAnimtion();
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
