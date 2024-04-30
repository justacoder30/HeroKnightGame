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
        public static List<Rectangle> GetHolderCollision;
        public static List<Rectangle> GetMapCollision;
        public static List<Rectangle> GetEnemyCollision;
        private static TiledMap map;
        private Texture2D _texture;

        public Map()
        {
            _texture = Globals.Content.Load<Texture2D>("Map/Dungeon Map");

            MapWidth = _texture.Width;
            MapHeight = _texture.Height;

            map = new TiledMap("Content/Map/Map.tmx");

            TiledLayer _mapCollision = map.Layers.First(m => m.name == "MapCollider");
            TiledLayer _holderCollision = map.Layers.First(h => h.name == "HolderCollider");
            TiledLayer _enemyCollision = map.Layers.First(e => e.name == "EnemyCollider");


            GetMapCollision = new List<Rectangle>();
            GetHolderCollision = new List<Rectangle>();
            GetEnemyCollision = new List<Rectangle>();

            foreach (var obj in _mapCollision.objects)
            {
                GetMapCollision.Add(new Rectangle((int)obj.x, (int)obj.y, (int)obj.width, (int)obj.height));
            }

            foreach (var obj in _holderCollision.objects)
            {
                GetHolderCollision.Add(new Rectangle((int)obj.x, (int)obj.y, (int)obj.width, (int)obj.height));
            }

            foreach(var obj in _enemyCollision.objects)
            {
                GetEnemyCollision.Add(new Rectangle((int)obj.x, (int)obj.y, (int)obj.width, (int)obj.height));
            }
        }

        public static Vector2 GetPlayerPosition()
        {
            TiledLayer _playerPos = map.Layers.First(p => p.name == "PlayerPosition");

            return new Vector2(_playerPos.objects[0].x, _playerPos.objects[0].y);
        }

        public static List<Vector2> GetEnemyPosition()
        {
            TiledLayer _enemyPos = map.Layers.First(e => e.name == "EnemyPosition");
            List<Vector2> _pos = new List<Vector2>();

            foreach (var obj in _enemyPos.objects)
            {
                _pos.Add(new Vector2(obj.x, obj.y));
            }

            return _pos;
        }

        public static List<Vector2> GetCoinPosition()
        {
            TiledLayer _coinPos = map.Layers.First(c => c.name == "CoinPosition");
            List<Vector2> _pos = new List<Vector2>();

            foreach (var obj in _coinPos.objects)
            {
                _pos.Add(new Vector2(obj.x, obj.y));
            }

            return _pos;
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(_texture, new Vector2(0, 0), Color.White);
        }
    }
}
