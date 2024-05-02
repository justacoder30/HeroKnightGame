using HeroKnightGame.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace HeroKnightGame
{
    public class Flag : Model
    {
        Player _player;

        public Flag()
        {
            Position = Map.GetFlagPosition();

            OFFSET_Height = 0;
            OFFSET_Width = 0;

            _animations = new Dictionary<string, Animation>();

            _animations.Add("No Flag", new Animation(Globals.Content.Load<Texture2D>("Item/No Flag"), 1));
            _animations.Add("Flag Out", new Animation(Globals.Content.Load<Texture2D>("Item/Flag Out"), 26, 0.07f, true));

            _animationManager = new AnimationManager(_animations.First().Value);

            _texture_Height = _animationManager.Animation.FrameHeight;
            _texture_Width = _animationManager.Animation.FrameWidth;
        }

        private bool _isTouched(Player player)
        {
            var _playerRect = player.CalculateBounds();
            var _flagRect = CalculateBounds();

            if (_playerRect.Intersects(_flagRect)) return true;

            return false;
        }

        private void SetAnimation()
        {
            _animationManager.Update();

            UpdateAnimation();

            switch (_state)
            {
                case CharacterState.Idle:
                    _animationManager.Play(_animations["No Flag"]);
                    break;
                case CharacterState.Run:
                    _animationManager.Play(_animations["Flag Out"]);
                    break;
            }
        }

        public void UpdateAnimation()
        {
            if (!_animationManager.IsAnimationRunning && _state != CharacterState.Idle)
            {
                
                EntityManager.IsEndGame = true;
                return;
            }

            if (!_isTouched(_player) && _state == CharacterState.Idle) _state = CharacterState.Idle;
            else
            {
                _state = CharacterState.Run;
                SoundManager.PlaySound("GameWin_sound", 1f, true);
            }
        }

        public void Update(Player player)
        {
            _player = player;
            SetAnimation();
        }

        public new void Draw()
        {
            base.Draw();
        }
    }
}
