﻿

using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;

namespace HeroKnightGame
{
    public class EndState : GameState
    {
        private List<Button> _buttons;

        public EndState(KnightGame game)
        {
            _game = game;

            Button PlayAgainButton = new Button(Globals.Content.Load<Texture2D>("Button/Continue Button"), new Vector2(176, 80));
            PlayAgainButton.Click += PlayAgainButton_Click;

            Button QuitButton = new Button(Globals.Content.Load<Texture2D>("Button/Quit Button"), new Vector2(176, 144));
            QuitButton.Click += QuitButton_Click;

            _buttons = new List<Button>()
            {
                PlayAgainButton,
                QuitButton,
            };
        }

        private void PlayAgainButton_Click(object sender, EventArgs e)
        {
            EntityManager.IsEndGame = false;
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

            foreach (Button button in _buttons)
            {
                button.Draw();
            }

            Globals.SpriteBatch.End();
        }
    }
}
