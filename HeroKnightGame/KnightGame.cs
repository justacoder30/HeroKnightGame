using HeroKnightGame.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HeroKnightGame
{
    public class KnightGame : Game
    {
        private EntityManager _entityManager;
        private GameState _currentState;
        private GameState _nextState;
        private GameState _prevState;
        private Sprite _backgroundMenu;

        public GameState CurrentState { get; set; }

        public KnightGame()
        {
            Globals.Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public void ChangeNextState(GameState state)
        {
            _nextState = state;
        }

        public void ChangePrevState()
        {
            _nextState = _prevState;
        }

        public void SaveCurrentState()
        {
            _prevState = _currentState;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Globals.Game = this;
            Globals.WindowSize = new Point(480, 270);
            Globals.Content = Content;
            SoundManager.Init();
            Renderer.Init();

            //Renderer.SetResolution(1920, 1080);
            Renderer.SetResolution(1280, 720);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            _currentState = new MenuState(this);
            _backgroundMenu = new Sprite(Globals.Content.Load<Texture2D>("Background/Background1"), new Vector2(0, 0));
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.Update();
            // TODO: Add your update logic here
            Globals.Update(gameTime);
            //_entityManager.Update();

            if (_nextState != null)
            {
                _currentState = _nextState;
                _nextState = null;
            }

            _currentState.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Renderer.Activate();
            // TODO: Add your drawing code here

            Globals.SpriteBatch.Begin();
            Globals.SpriteBatch.Draw(_backgroundMenu.Texture,
                                    new Rectangle(0, 0,
                                    (int)_backgroundMenu.TextureWidth / 4,
                                    (int)_backgroundMenu.TextureHeight / 4), 
                                    Color.White);
            Globals.SpriteBatch.End();

            _currentState.Draw();

            Renderer.Draw();

            base.Draw(gameTime);
        }
    }
}
