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

        public Animation(Texture2D texture, int framecount)
        {
            Texture = texture;
            FrameCount = framecount;
            FrameSpeed = 0.05f;
        }
    }
}
