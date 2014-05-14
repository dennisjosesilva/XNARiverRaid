using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RiverRaid.Generics.XMLParser
{
    public class Tile
    {
        #region Attributes
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string coordinate;
        public string Coordinate
        {
            get { return coordinate; }
            set { coordinate = value; }
        }

        private Rectangle imageRect;
        public Rectangle ImageRect
        {
            get { return imageRect; }
            set { imageRect = value; }
        }

        private List<Rectangle> collisionRectList;
        public List<Rectangle> CollisionRectList
        {
            get { return collisionRectList; }
            set { collisionRectList = value; }
        }

        private Point tileSize;
        public Point TileSize
        {
            get { return tileSize; }
            set { tileSize = value; }
        } 
        #endregion

        public Tile(string name, Point tileSize, string coordinate, List<Rectangle> collisionRectList)
        {
            #region set attributes
            this.name = name;
            this.tileSize = tileSize;
            this.collisionRectList = collisionRectList;
            this.coordinate = coordinate;
            #endregion

            imageRect = convertImageCoordinate();
        }


        public Tile(Tile tile)
        {
            this.name = tile.name;
            this.tileSize = tile.tileSize;
            this.collisionRectList = tile.collisionRectList;

            this.imageRect = tile.imageRect;
        }

        private Rectangle convertImageCoordinate()
        {
            int line;
            int collumn;
            char charLine;

            charLine = coordinate[0];
            line = charLine - 'A';
            collumn = int.Parse(coordinate.Substring(1, 2));

            return new Rectangle(collumn * tileSize.X, line * tileSize.Y,
                tileSize.X, tileSize.Y);
        }
    }
}
