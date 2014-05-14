using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiverRaid.Generics.XMLParser
{
    public class LevelDescription
    {
        #region Atributos
        private string imageFile;
        public string ImageFile
        {
            get { return imageFile; }
            set { imageFile = value; }
        }

        private string finalSpriteTexture;
        public string FinalSpriteTexture
        {
            get { return finalSpriteTexture; }
            set { finalSpriteTexture = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int screenWidth;
        public int ScreenWidth
        {
            get { return screenWidth; }
            set { screenWidth = value; }
        }

        private int screenHeight;
        public int ScreenHeight
        {
            get { return screenHeight; }
            set { screenHeight = value; }
        }

        private List<Tile> listTiles;
        public Tile this[int index]
        {
            get { return listTiles[index]; }
            set { listTiles[index] = value; }
        }

        public List<Tile> ListTiles
        {
            get { return listTiles; }
        }

        #endregion

        #region Construtores
        public LevelDescription(string imageFile, string name, int screenWidth, int screenHeight, 
            string FinalSpriteTexture)
        {
            this.imageFile = imageFile;
            this.name = name;
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            this.listTiles = new List<Tile>();
            this.finalSpriteTexture = FinalSpriteTexture;
        }

        public LevelDescription(string imageFile, string name, int screenWidth, int screenHeight, List<Tile> listTiles,
            string finalSpriteTexture) :
            this(imageFile, name, screenHeight, screenWidth, finalSpriteTexture)
        {
            this.listTiles = listTiles;
        } 
        #endregion

        #region Metódos
        public void Add(Tile tile)
        {
            listTiles.Add(tile);
        } 
        #endregion
    }
}
