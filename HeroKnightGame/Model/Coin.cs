using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace HeroKnightGame
{
    public class Coin : Model
    {
        public Coin(Vector2 pos)
        {
            Position = pos;

            OFFSET_Height = 0;
            OFFSET_Width = 1;

            _animations = new Dictionary<string, Animation>();

            _animations.Add("Idle", new Animation(Globals.Content.Load<Texture2D>("Item/Coin"), 5));

            _animationManager = new AnimationManager(_animations.First().Value);

            _texture_Height = _animationManager.Animation.FrameHeight;
            _texture_Width = _animationManager.Animation.FrameWidth;
        }

        //Ham kiem tra Coin co bi nhan vat dung vao khong
        private void _isCollected(Player player)
        {
            var _playerRect = player.CalculateBounds();
            var _coinRect = CalculateBounds();

            //Neu co thi loai bo Coin ra khoi phan display
            if (_playerRect.Intersects(_coinRect))
            {
                IsRemoved = true;
            }
        }

        //Ham setup animation cho dong xu
        private void SetAnimation()
        {
            _animationManager.Update();
            _animationManager.Play(_animations["Idle"]);
        }

        //Ham cap nhap cac thanh phan logic cua Coin, chay moi Frame
        public void Update(Player player)
        {
            _isCollected(player);
            SetAnimation();
        }

        public new void Draw()
        {
            base.Draw();
        }
    }
}
