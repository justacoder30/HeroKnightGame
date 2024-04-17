using Microsoft.Xna.Framework.Input;

namespace HeroKnightGame
{
    public static class InputManager
    {
        static KeyboardState currentKeySate;
        static KeyboardState prevKeySate;

        public static void Update()
        {
            prevKeySate = currentKeySate;
            currentKeySate = Keyboard.GetState();

            if (currentKeySate.IsKeyDown(Keys.F11) && prevKeySate.IsKeyUp(Keys.F11)) Renderer.FullScreenToggle();

        }
    }
}
