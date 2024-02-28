using Microsoft.Xna.Framework;

namespace HeroKnightGame
{
    public class Camera
    {
        public Matrix Traslation {  get; set; }

        public void FollowPLayer(Player player)
        {
            var dx = Globals.WindowSize.X/2 - player._position.X;
            var dy = Globals.WindowSize.Y/2 - player._position.Y;
            Traslation = Matrix.CreateTranslation(dx, dy, 0f);
        }
    }
}
