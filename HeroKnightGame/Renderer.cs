using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace HeroKnightGame
{
    public class Renderer
    {
        private static RenderTarget2D _target;
        private static Rectangle Rectangle;
        private static bool FullScreen = true;

        public Renderer()
        {
            _target = new RenderTarget2D(Globals.Graphics.GraphicsDevice, Globals.WindowSize.X, Globals.WindowSize.Y);
        }

        public static void SetDestinationRectangle()
        {
            var screenSize = Globals.Graphics.GraphicsDevice.PresentationParameters.Bounds;

            float scaleX = (float)screenSize.Width / _target.Width;
            float scaleY = (float)screenSize.Height / _target.Height;
            float scale = Math.Min(scaleX, scaleY);

            int newWidth = (int)(_target.Width * scale);
            int newHeight = (int)(_target.Height * scale);

            Rectangle = new Rectangle(0, 0, newWidth, newHeight);
        }

        public void SetResolution(int height, int width)
        {
            Globals.Graphics.PreferredBackBufferWidth = height;
            Globals.Graphics.PreferredBackBufferHeight = width;
            Globals.Graphics.IsFullScreen = FullScreen;
            Globals.Graphics.ApplyChanges();
            SetDestinationRectangle();
        }

        public static void FullScreenToggle()
        {
            FullScreen = !FullScreen;
            Globals.Graphics.IsFullScreen = FullScreen;
            Globals.Graphics.ApplyChanges();
        }

        public void Activate()
        {
            Globals.Graphics.GraphicsDevice.SetRenderTarget(_target);
        }

        public void Draw()
        {
            Globals.Graphics.GraphicsDevice.SetRenderTarget(null);
            Globals.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            Globals.SpriteBatch.Draw(_target, Rectangle, Color.White);
            Globals.SpriteBatch.End();
        }
    }
}
