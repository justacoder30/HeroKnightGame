using Microsoft.Xna.Framework;
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
            /*_animations.Add("Idle", new Animation(Globals.Content.Load<Texture2D>("Idle"), 5));
            _animations.Add("Run", new Animation(Globals.Content.Load<Texture2D>("Run"), 6));
            _animations.Add("Jump", new Animation(Globals.Content.Load<Texture2D>("Jump"), 2));
            _animations.Add("Shoot", new Animation(Globals.Content.Load<Texture2D>("Shoot"), 3));
            _animations.Add("Death", new Animation(Globals.Content.Load<Texture2D>("Death"), 8));*/

            _animations.Add("Idle", new Animation(Globals.Content.Load<Texture2D>("Enemy/Idle"), 4, 0.2f));
            _animations.Add("Run", new Animation(Globals.Content.Load<Texture2D>("Enemy/Walk"), 4));
            _animations.Add("Shoot", new Animation(Globals.Content.Load<Texture2D>("Enemy/Attack"), 8));

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
            _enemy = CharacterState.Idle;

            if (velocity.X > 0) _effect = SpriteEffects.None;
            if (velocity.X < 0) _effect = SpriteEffects.FlipHorizontally;

            if (velocity.Y == 0)
            {
                if (velocity.X == Speed)
                {
                    _enemy = CharacterState.Run;
                }
                else if (velocity.X == -Speed)
                {
                    _enemy = CharacterState.Run;
                }
                if (velocity.X < Speed && velocity.X > 0 || velocity.X > -Speed && velocity.X < 0)
                {
                    _enemy = CharacterState.Idle;
                }
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
