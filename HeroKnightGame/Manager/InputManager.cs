using Microsoft.Xna.Framework.Input;

namespace HeroKnightGame
{
    public static class InputManager
    {
        public static KeyboardState CurrentKeySate;
        public static KeyboardState PrevKeySate;

        public static void Update()
        {
            PrevKeySate = CurrentKeySate;
            CurrentKeySate = Keyboard.GetState();

            if (CurrentKeySate.IsKeyDown(Keys.F11) && PrevKeySate.IsKeyUp(Keys.F11)) Renderer.FullScreenToggle();

        }
    }
}
