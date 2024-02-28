using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using TiledCS;

namespace HeroKnightGame
{
    public class Map
    {
        public static float MapWidth;
        public static float MapHeight;
        public static int TILES_SIZE = 128;
        private TiledMap map;
        private TiledLayer collision;
        Texture2D _texture;

        public Map()
        {
            map = new TiledMap("Content/map.tmx");
            collision = map.Layers.First(l => l.name == "Object Layer 1");
            _texture = Globals.Content.Load<Texture2D>("map");
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(_texture, new Vector2(0, 0), Color.White);
        }
    }
}
