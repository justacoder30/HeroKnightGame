﻿
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.ComponentModel;
using System;
using HeroKnightGame.Manager;

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
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width/5, _texture.Height/5);
            }
        }

        public Rectangle RectangleScale
        {
            get
            {
                return new Rectangle(
                                    (int)(Rectangle.X * Renderer.Scale) + Renderer.PosX,
                                    (int)(Rectangle.Y * Renderer.Scale) + Renderer.PosY,
                                    (int)(Rectangle.Width * Renderer.Scale),
                                    (int)(Rectangle.Height * Renderer.Scale)
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

        public Button(Texture2D texture, Vector2 pos)
        {
            _texture = texture;

            Position = pos;
        }

        //Cập nhật trạng thái của nút
        public void Update()
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            if (mouseRectangle.Intersects(RectangleScale))
            {
                if(!_isHovering)
                {
                    _isHovering = true;
                    SoundManager.PlaySound("ButtonChose_sound");
                }

                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {   //click chưa 
                    SoundManager.PlaySound("ButtonClick_sound");
                    Click.Invoke(this, new EventArgs());
                }
            }
            else _isHovering = false;
        }

        //Vẽ nút lên màn hình.
        public void Draw()
        {
            var colour = Color.White;

            if (_isHovering) colour = Color.LightSeaGreen;

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
