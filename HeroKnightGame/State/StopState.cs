using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using HeroKnightGame.Manager;

namespace HeroKnightGame
{
    public class StopState : GameState
    {
        private List<Button> _buttons;

        public StopState(KnightGame game)
        {
            _game = game;

            var buttonTexture = Globals.Content.Load<Texture2D>("Button/Button");
            var buttonFont = Globals.Content.Load<SpriteFont>("Fonts/Font");

            Button ContinueButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(144, 64),
                Text = "Continue",
            };
            ContinueButton.Click += ContinueButton_Click;

            Button ToMenu = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(144, 128),
                Text = "Back to Menu",
            };
            ToMenu.Click += ToMenuButton_Click;

            Button QuitButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(144, 192),
                Text = "Quit",
            };
            QuitButton.Click += QuitButton_Click;

            _buttons = new List<Button>()
            {
                ContinueButton,
                ToMenu,
                QuitButton,
            };
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            _game.ChangePrevState();
        }
        private void ToMenuButton_Click(object sender, EventArgs e)
        {
            SoundManager.StopMusic();
            _game.ChangeNextState(new MenuState(_game));
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

            foreach (Button button in _buttons)
            {
                button.Draw();
            }

            Globals.SpriteBatch.End();
        }
    }
}
