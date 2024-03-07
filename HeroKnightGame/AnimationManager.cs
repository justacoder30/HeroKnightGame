using Microsoft.Xna.Framework;
using System;

namespace HeroKnightGame
{
    public class AnimationManager
    {
        private float _timer;
        public Animation Animation { get; set; }

        public AnimationManager(Animation animation)
        {
            Animation = animation;
        }

        public void Play(Animation animation)
        {
            if (Animation == animation) return;
            Animation = animation;
            Animation.CurrentFrame = 0;
            _timer = 0; 
        }

        public void Update()
        {
            _timer += Globals.Time;

            if(_timer > Animation.FrameSpeed)
            {
                _timer = 0;
                Animation.CurrentFrame++;

                if(Animation.CurrentFrame >= Animation.FrameCount)
                {
                    Animation.CurrentFrame = 0;
                }
            }
        }

        public Rectangle Rect()
        {
            return new Rectangle(Animation.FrameWidth * Animation.CurrentFrame,
                                 0,
                                 Animation.FrameWidth,
                                 Animation.FrameHeight);
        }
    }
}
