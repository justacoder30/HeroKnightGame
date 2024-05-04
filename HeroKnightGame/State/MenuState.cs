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


        //Khoi tao Menu
        public MenuState(KnightGame game)
        {
            _game = game;

            Button StartButton = new Button(Globals.Content.Load<Texture2D>("Button/Play Button"), new Vector2(176, 96));
            StartButton.Click += StartButton_Click;

            Button QuitButton = new Button(Globals.Content.Load<Texture2D>("Button/Quit Button"), new Vector2(176, 160));
            QuitButton.Click += QuitButton_Click;

            _buttons = new List<Button>()
            {
                StartButton,
                QuitButton,
            };
        }

        //Neu Click vao Play Button thi` vao gameplay
        private void StartButton_Click(object sender, EventArgs e)
        {
            _game.ChangeNextState(new RunningState(_game));
        }

        //Neu Click vao Quit Button thi` thoat gameplay
        private void QuitButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        //Cap nhap phim Menu
        public override void Update()
        {
            foreach (Button button in _buttons)
            {
                button.Update();
            }
        }

        //Ve phim Menu len man hinh`
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
