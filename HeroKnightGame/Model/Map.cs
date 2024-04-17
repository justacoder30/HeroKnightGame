using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using TiledCS;

namespace HeroKnightGame
{
    public class Map
    {
        public static float MapWidth;
        public static float MapHeight;
        public static int TILES_SIZE;
        private static TiledLayer collision;
        private TiledMap map;
        private Texture2D _texture;

        public Map()
        {
            _texture = Globals.Content.Load<Texture2D>("map");
            MapWidth = _texture.Width;
            MapHeight = _texture.Height;
            map = new TiledMap("Content/map.tmx");
            collision = map.Layers.First(l => l.name == "Object Layer 1");    
        }

        public static List<Rectangle> GetCollision()
        {
            List<Rectangle> result = new List<Rectangle>();

            foreach(var obj in collision.objects)
            {
                result.Add(new Rectangle((int)obj.x,(int)obj.y, (int)obj.width, (int)obj.height));
            }

            return result;
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(_texture, new Vector2(0, 0), Color.White);
        }
    }
}
