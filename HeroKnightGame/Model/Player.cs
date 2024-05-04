using HeroKnightGame.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace HeroKnightGame
{
    public class Player : Model
    {
        private KeyboardState _currentKeySate;
        private KeyboardState _prevKeySate;
        private CharacterState _prevState;
        private CharacterState _currentState;

        public Player(Texture2D texture, Vector2 position) : base(texture, position) 
        { }

        public Player()
        {
            Speed = 180f;
            Gravity = 1000f;
            Jump = 410f;
            OFFSET_Width = 52;
            OFFSET_Height = 42;
            damage = 10;
            HP = 50;

            Position = Map.GetPlayerPosition();

            _animations = new Dictionary<string, Animation>();

            _animations.Add("Idle", new Animation(Globals.Content.Load<Texture2D>("Player/Idle"), 10));
            _animations.Add("Run", new Animation(Globals.Content.Load<Texture2D>("Player/Run"), 10, 0.049f));
            _animations.Add("Jump", new Animation(Globals.Content.Load<Texture2D>("Player/Jump"), 3));
            _animations.Add("Fall", new Animation(Globals.Content.Load<Texture2D>("Player/Fall"), 3));
            _animations.Add("Attack", new Animation(Globals.Content.Load<Texture2D>("Player/Attack"), 6, 0.05f, true));
            _animations.Add("Death", new Animation(Globals.Content.Load<Texture2D>("Player/Death"), 10, 0.05f, true));

            _animationManager = new AnimationManager(_animations.First().Value);

            _texture_Width = _animationManager.Animation.FrameWidth;
            _texture_Height = _animationManager.Animation.FrameHeight;
        }

        private Rectangle GravityBounds(Vector2 pos)
        {
            return new((int)pos.X + OFFSET_Width, (int)pos.Y + _texture_Height, _texture_Width - OFFSET_Width * 2, 1);
        }

        //Ham ap dung trong luc len nhan vat
        private void ApplyGravity()
        {
            //Ta se tinh toan cai GravityBounds ngay duoi chan nguoi choi de kiem tra nguoi choi co dang dung tren ground hay khong  
            var newRect = GravityBounds(new(Position.X, Position.Y));

            foreach (var collider in Map.GetMapCollision)
            {
                //Neu co thi khong ap dung trong luc len nhan vat, thoat ra
                if (newRect.Intersects(collider)) return;
            }

            foreach (var collider in Map.GetHolderCollision)
            {
                //Neu co thi khong ap dung trong luc len nhan vat, thoat ra
                if (newRect.Intersects(collider)) return;
            }

            //Ap dung gia toc trong truong len nhan vat
            velocity.Y += Gravity * Globals.Time;
        }

        //Cap nhap vector chuyen dong cho nhan vat
        private void UpdateVelocity()
        {
            _prevKeySate = _currentKeySate;
            _currentKeySate = Keyboard.GetState();

            velocity.X = 0;

            //Ham ap dung trong luc cho nhan vat
            ApplyGravity();

            //Neu Nhan vat chet thi thoat ra
            if (_state == CharacterState.Death) return;

            //vector chuyen dong sang phai
            if (_currentKeySate.IsKeyDown(Keys.D)) 
            {
                velocity.X = Speed;
            }

            //vector chuyen dong sang trai
            if (_currentKeySate.IsKeyDown(Keys.A))
            {
                velocity.X = -Speed;
            }

            //Khi nhay Vecor hu0ng len tren
            if (_currentKeySate.IsKeyDown(Keys.Space) && !_falling)
            {
                velocity.Y = -Jump;
                _falling = true;
            }
        }

        //Cap nhap vi tri cua nhan vat
        protected void UpdatePosition()
        {
            
            Vector2 newPos = Position + velocity * Globals.Time;
            Rectangle newRect;


            foreach (var collider in Map.GetTrapCollision)
            {
                newRect = CalculateBounds(new(Position.X, newPos.Y));

                if (newRect.Intersects(collider))
                {
                    HP = 0;
                    velocity.X = 0;
                }
                
            }

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

            //Tinh toan su ly va cham voi Map Collision
            foreach (var collider in Map.GetMapCollision)
            {
                //Khi nhan vat di chuyen theo chieu ngang
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

                //Khi nhan vat di chuyen theo chieu doc
                if (newPos.Y != Position.Y)
                {
                    newRect = CalculateBounds(new(Position.X, newPos.Y));

                    if (newRect.Intersects(collider))
                    {
                        if (newPos.Y > Position.Y)
                        {
                            newPos.Y = collider.Top - _texture_Height;
                            _falling = false;
                            velocity.Y = 0;
                        }
                        else if (newPos.Y < Position.Y) 
                        {
                            newPos.Y = collider.Bottom - OFFSET_Height;
                            velocity.Y = 0;
                        }
                    }
                }  
            }

            //Gan vi tri cua nhan vat cho vi tri moi da duoc tinh toan
            Position = newPos; 
        }

        public void Attacking()
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
        private void Updatesound()
        {
            _prevState = _currentState;
            _currentState = _state;

            if (_currentState == CharacterState.Attack && _prevState != CharacterState.Attack) SoundManager.PlaySound("Attack_sound", 0.45f);
            if (velocity.Y == 0 && _prevState == CharacterState.Fall) SoundManager.PlaySound("Landing_sound", 0.3f);
            if (_currentState == CharacterState.Run) SoundManager.PlaySound("Footstep_sound", 1f, true);
        }

        private void UpdateAnimation()
        {
            if (!_animationManager.IsAnimationRunning && _state == CharacterState.Death)
            {
                SoundManager.PlaySound("GameLose_sound", 0.2f);
                EntityManager.IsEndGame = true;
                return;
            }
            if (_state == CharacterState.Death || HP <= 0)
            {
                _state = CharacterState.Death;
                velocity.X = 0;
                return;
            }

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
                    if (_currentKeySate.IsKeyDown(Keys.J) && _prevKeySate.IsKeyUp(Keys.J) && !_animationManager.IsAnimationRunning)
                    {
                        _state = CharacterState.Attack;
                        _animationManager.IsAnimationRunning = true;
                        Attacking();
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
        
        //Ham chay animation dua tren cac trang thai cua nhan vat
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
            Updatesound();
        }

        public new void Draw()
        {
            base.Draw();
        }
    }
}