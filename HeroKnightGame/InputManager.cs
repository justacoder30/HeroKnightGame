using Microsoft.Xna.Framework.Input;

namespace HeroKnightGame
{
    public static class InputManager
    {
        public static void Update()
        {
            var KeySate = Keyboard.GetState();

            if(KeySate.IsKeyDown(Keys.F11)) Renderer.FullScreenToggle();
        }
    }
}
