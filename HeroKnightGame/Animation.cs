using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace HeroKnightGame
{
    public class Animation
    {
        public Texture2D Texture { get; set; }
        public int CurrentFrame {  get; set; }
        public int FrameCount { get; set; }
        public float FrameSpeed { get; set; }
        public int FrameWidth { get => Texture.Width / FrameCount; }
        public int FrameHeight { get => Texture.Height; }

        public Animation(Texture2D texture, int framecount, float frameSpeed = 0.1f)
        {
            Texture = texture;
            FrameCount = framecount;
            FrameSpeed = frameSpeed;
        }
    }
}
