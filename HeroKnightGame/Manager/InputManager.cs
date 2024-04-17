using Microsoft.Xna.Framework.Input;

namespace HeroKnightGame
{
    public static class InputManager
    {
        static KeyboardState _currentKeySate;
        static KeyboardState _prevKeySate;

        public static void Update()
        {
            _prevKeySate = _currentKeySate;
            _currentKeySate = Keyboard.GetState();

            if (_currentKeySate.IsKeyDown(Keys.F11) && _prevKeySate.IsKeyUp(Keys.F11)) Renderer.FullScreenToggle();

        }
    }
}
