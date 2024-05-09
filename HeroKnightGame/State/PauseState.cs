using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using HeroKnightGame.Manager;
using Microsoft.Xna.Framework.Input;

namespace HeroKnightGame
{
    public class PauseState : GameState
    {
        private List<Button> _buttons;

        public PauseState(KnightGame game)
        {
            _game = game;

            _game.IsMouseVisible = true;

            Button ContinueButton = new Button(Globals.Content.Load<Texture2D>("Button/Resume Button"), new Vector2(176, 64));
            ContinueButton.Click += ContinueButton_Click;

            Button ToMenu = new Button(Globals.Content.Load<Texture2D>("Button/Menu Button"), new Vector2(176, 128));
            ToMenu.Click += ToMenuButton_Click;

            Button QuitButton = new Button(Globals.Content.Load<Texture2D>("Button/Quit Button"), new Vector2(176, 192));
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
            if (InputManager.CurrentKeySate.IsKeyDown(Keys.Escape) && InputManager.PrevKeySate.IsKeyUp(Keys.Escape))
            {
                _game.ChangePrevState();
            }

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
