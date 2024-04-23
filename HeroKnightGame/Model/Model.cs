﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace HeroKnightGame
{
    public abstract class Model : Sprite
    {
        protected AnimationManager _animationManager;
        protected Dictionary<string, Animation> _animations;
        protected Vector2 velocity;
        protected CharacterState _state;
        protected SpriteEffects _effect = SpriteEffects.None;
        protected float Speed;
        protected float Gravity;
        protected float Jump;
        protected bool _falling;
        protected int _texture_Width;
        protected int _texture_Height;
        protected int OFFSET_Width;
        protected int OFFSET_Height;
        protected int damage;
        protected bool BeingHit;
        protected int HP;
        public bool IsRemoved;

        public Vector2 Orgin
        {
            get => new Vector2(_texture_Width / 2, _texture_Height / 2);
        }

        protected Model(Texture2D texture, Vector2 position) : base(texture, position)
        { }

        protected Model()
        {

        }

        protected Rectangle CalculateBounds(Vector2 pos)
        {
            return new((int)pos.X + OFFSET_Width, (int)pos.Y + OFFSET_Height, _texture_Width - OFFSET_Width * 2, _texture_Height - OFFSET_Height);
        }

        public Rectangle CalculateBounds()
        {
            return new((int)Position.X + OFFSET_Width, (int)Position.Y + OFFSET_Height, _texture_Width - OFFSET_Width * 2, _texture_Height - OFFSET_Height);
        }

        protected virtual void ApplyGravity()
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
        }

        protected Rectangle GetAttackBound()
        {
            if (_effect == SpriteEffects.None) return new Rectangle((int)Position.X + _texture_Width - OFFSET_Width, (int)Position.Y, OFFSET_Width, OFFSET_Height);
            return new Rectangle((int)Position.X, (int)Position.Y, OFFSET_Width, OFFSET_Height);
        }

        public void IsBeingHit(int damage)
        {
            BeingHit = true;
            HP -= damage;
        }

        protected abstract void UpdateVelocity();
        protected abstract void UpdatePosition();
        protected abstract void SetAnimtion();
        public abstract void Update();
        public abstract void IsAttacking();

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
