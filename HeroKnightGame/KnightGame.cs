using HeroKnightGame.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HeroKnightGame
{
    public class KnightGame : Game
    {
        private Renderer _render;
        private EntityManager _entityManager;
        private GameState _currentState;
        private GameState _nextState;
        private GameState _prevState;

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

            _render = new Renderer();
            Renderer.SetResolution(1920, 1080);
            //Renderer.SetResolution(1280, 720);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            _currentState = new MenuState(this);
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

            _render.Activate();
            // TODO: Add your drawing code here

            _currentState.Draw();

            _render.Draw();

            base.Draw(gameTime);
        }
    }
}
