﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeroKnightGame.Model
{
    public class Enemy : Sprite
    {
        private Vector2 velocity;
        private Vector2 newVelocity;
        private CharacterState _enemy;
        private SpriteEffects _effect = SpriteEffects.None;
        private const float Speed = 150f;
        private const float Gravity = 1000f;
        private bool _falling = true;
        private int _texture_Width;
        private int _texture_Height;
        private const int OFFSET_Width = 7;
        private const int OFFSET_Height = 8;

        public Enemy(Texture2D texture, Vector2 position) : base(texture, position)
        { }

        public Enemy(Vector2 postion)
        {
            Position = postion;

            _animations = new Dictionary<string, Animation>();

            _animations.Add("Idle", new Animation(Globals.Content.Load<Texture2D>("Enemy/Idle"), 4, 0.2f));
            _animations.Add("Run", new Animation(Globals.Content.Load<Texture2D>("Enemy/Walk"), 4, 0.2f));
            _animations.Add("Attack", new Animation(Globals.Content.Load<Texture2D>("Enemy/Attack"), 8));

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
            
        }

        private void UpdateVelocity()
        {
            
        }

        private void UpdatePosition()
        {

            
        }

        private void UpdateAnimation()
        {
            _enemy = CharacterState.Attack;
            
            if (velocity.X != 0)
            {
                if (velocity.X > 0) _effect = SpriteEffects.None;
                if (velocity.X < 0) _effect = SpriteEffects.FlipHorizontally;
                _enemy = CharacterState.Run;
            }
            else if (velocity.X == 0)
            {
                _enemy = CharacterState.Idle;
            }
        }

        private void SetAnimtion()
        {
            UpdateAnimation();

            switch (_enemy)
            {
                case CharacterState.Idle:
                    _animationManager.Play(_animations["Idle"]);
                    break;
                case CharacterState.Run:
                    _animationManager.Play(_animations["Run"]);
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
