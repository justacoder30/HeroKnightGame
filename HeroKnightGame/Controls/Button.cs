
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.ComponentModel;
using System;

namespace HeroKnightGame
{
    public class Button
    {
        private MouseState _currentMouse;

        private SpriteFont _font;

        private bool _isHovering;

        private MouseState _previousMouse;

        private Texture2D _texture; 

        public event EventHandler Click;


        public bool Clicked { get; private set; }

        public Color PenColour { get; set; }

        public Vector2 Position { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public Rectangle RectangleScale
        {
            get
            {
                return new Rectangle(
                                    (int)(Position.X * Renderer.Scale),
                                    (int)(Position.Y * Renderer.Scale),
                                    (int)(_texture.Width * Renderer.Scale),
                                    (int)(_texture.Height * Renderer.Scale)
                                    );
            }
        }

        public string Text { get; set; }

        public Button(Texture2D texture, SpriteFont font)
        {
            _texture = texture;

            _font = font;

            PenColour = Color.Black;
        }

        //Cập nhật trạng thái của nút
        public void Update()
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            if (mouseRectangle.Intersects(RectangleScale))
            {
                _isHovering = true;

                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {   //click chưa 
                    Click.Invoke(this, new EventArgs());
                }
            }
        }

        //Vẽ nút lên màn hình.
        public void Draw()
        {
            var colour = Color.White;

            if (_isHovering) colour = Color.Gray;

            Globals.SpriteBatch.Draw(_texture, Rectangle, colour);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);

                Globals.SpriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColour);
            }
        }
    }
}
