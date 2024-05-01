using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace HeroKnightGame
{
    public class MenuState : GameState
    {
        private List<Button> _buttons;

        public MenuState(KnightGame game)
        {
            _game = game;

            var buttonTexture = Globals.Content.Load<Texture2D>("Button/Button");
            var buttonFont = Globals.Content.Load<SpriteFont>("Fonts/Font");

            Button StartButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(144, 80),
                Text = "Start",
            };
            StartButton.Click += StartButton_Click;

            Button QuitButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(144, 144),
                Text = "Quit",
            };
            QuitButton.Click += QuitButton_Click;

            _buttons = new List<Button>()
            {
                StartButton,
                QuitButton,
            };
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            _game.ChangeNextState(new RunningState(_game));
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        public override void Update()
        {
            foreach (Button button in _buttons)
            {
                button.Update();
            }
        }

        public override void Draw()
        {
            Globals.SpriteBatch.Begin();

            foreach(Button button in _buttons)
            {
                button.Draw();
            }

            Globals.SpriteBatch.End();
        }
    }
}
