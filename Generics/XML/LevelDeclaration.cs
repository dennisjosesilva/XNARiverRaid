using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RiverRaid.Generics.XMLParser
{
    public class LevelDeclaration
    {
        private int tileHeight;
        public int TileHeight
        {
            get { return tileHeight; }
            set { tileHeight = value; }
        }

        private int tileWidth;
        public int TileWidth
        {
            get { return tileWidth; }
            set { tileWidth = value; }
        }

        private Dictionary<string, Tile> dicTiles;
        public Dictionary<string, Tile> DicTiles
        {
              get { return dicTiles; }
              set { dicTiles = value; }
        }

        public LevelDeclaration(int tileWidth, int tileHeight)
        {
            this.tileHeight = tileHeight;
            this.tileWidth = tileWidth;
            dicTiles = new Dictionary<string, Tile>();
        }

        public void Add(Tile tile)
        {
            dicTiles.Add(tile.Name, tile);
        }

        public Tile getTile(string name)
        {
            return dicTiles[name];
        }
    }
}
