using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace HeroKnightGame
{
    public class Renderer
    {
        private static RenderTarget2D _target;
        private static Rectangle Rectangle;
        private static bool FullScreen = false;
        private static int _width;
        private static int _height;   
        public static float Scale { get; private set; }

        public Renderer()
        {
            _target = new RenderTarget2D(Globals.Graphics.GraphicsDevice, Globals.WindowSize.X, Globals.WindowSize.Y);
        }

        public static void SetDestinationRectangle()
        {
            var screenSize = Globals.Graphics.GraphicsDevice.PresentationParameters.Bounds;

            float scaleX = (float)screenSize.Width / _target.Width;
            float scaleY = (float)screenSize.Height / _target.Height;
            Scale = Math.Min(scaleX, scaleY);

            int newWidth = (int)(_target.Width * Scale);
            int newHeight = (int)(_target.Height * Scale);

            int posX = (screenSize.Width - newWidth) / 2;
            int posY = (screenSize.Height - newHeight) / 2;

            Rectangle = new Rectangle(posX, posY, newWidth, newHeight);
        }

        public static void SetResolution(int width, int height)
        {
            _width = width;
            _height = height;
            Globals.Graphics.PreferredBackBufferWidth = _width;
            Globals.Graphics.PreferredBackBufferHeight = _height;
            Globals.Graphics.IsFullScreen = FullScreen;
            Globals.Graphics.ApplyChanges();
            SetDestinationRectangle();
        }

        private static void SetFullScreen()
        {
            Globals.Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            Globals.Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            Globals.Graphics.HardwareModeSwitch = false;
            Globals.Game.Window.IsBorderless = FullScreen;
            Globals.Graphics.ApplyChanges();
            SetDestinationRectangle();
        }

        private static void UnsetFullScreen()
        {
            Globals.Graphics.PreferredBackBufferWidth = _width;
            Globals.Graphics.PreferredBackBufferHeight = _height;
            Globals.Game.Window.IsBorderless = FullScreen;
            Globals.Graphics.ApplyChanges();
            SetDestinationRectangle();
        }

        public static void FullScreenToggle()
        {
            FullScreen = !FullScreen;
            if (FullScreen) SetFullScreen();
            else UnsetFullScreen();
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
