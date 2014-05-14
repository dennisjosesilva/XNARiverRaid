using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Xml;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RiverRaid.Generics.XMLParser
{
    public class XMLParser
    {
        public static LevelDeclaration DeclarationsParser(string xmlFile)
        {
            int tileWidth = 0, tileHeight = 0;
            LevelDeclaration level = null;
            List<Rectangle> collisionRectList;

            #region inicializa stream
            FileStream fs = new FileStream(xmlFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(fs);
            #endregion

            #region Atributos do level
            XmlNodeList nodes = xmldoc.GetElementsByTagName("TilesDeclation");
            tileWidth = int.Parse(nodes[0].Attributes["TileWidth"].Value);
            tileHeight = int.Parse(nodes[0].Attributes["TileHeight"].Value); 
            #endregion

            level = new LevelDeclaration(tileWidth, tileHeight);

            foreach (XmlNode node in nodes[0].ChildNodes)
            {
                collisionRectList = new List<Rectangle>();
                #region Preenche lista com retangulos de colisão
                foreach (XmlNode collisionNode in node.ChildNodes)
                    collisionRectList.Add(new Rectangle(
                        int.Parse(collisionNode.Attributes["XPos"].Value),
                        int.Parse(collisionNode.Attributes["YPos"].Value),
                        int.Parse(collisionNode.Attributes["Width"].Value),
                        int.Parse(collisionNode.Attributes["Height"].Value)));
	            #endregion

                #region Insere Tile na lista
                level.DicTiles.Add(node.Attributes["name"].Value,
                        new Tile(node.Attributes["name"].Value,
                                 new Point(level.TileWidth, level.TileHeight),
                                 node.Attributes["Coordinate"].Value,
                                 collisionRectList)); 
                #endregion
                
            }
            return level;
        }

        public static LevelDescription DescriptionParser(string declarationLevelXML, string descriptionLevelXML)
        {
            #region Inicializações
            LevelDeclaration declarations = null;
            LevelDescription description = null;
            string imageFile = string.Empty;
            string name = string.Empty;
            string finalSpriteTexture = string.Empty;
            int screenHeight = 0, screenWidth = 0; 
            #endregion

            #region Inicializa parser
            declarations = XMLParser.DeclarationsParser(declarationLevelXML);
            FileStream fs = new FileStream(descriptionLevelXML, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(fs); 
            #endregion

            #region Construi LevelDescription
            XmlNodeList nodes = xmldoc.GetElementsByTagName("LevelDescription");
            name = nodes[0].Attributes["Name"].Value;
            imageFile = nodes[0].Attributes["ImageFile"].Value;
            screenWidth = int.Parse(nodes[0].Attributes["ScreenWidth"].Value);
            screenHeight = int.Parse(nodes[0].Attributes["ScreenHeight"].Value);
            finalSpriteTexture = nodes[0].Attributes["FinalSprite"].Value;

            description = new LevelDescription(imageFile, name, screenWidth, screenHeight, finalSpriteTexture); 
            #endregion

            #region Popula lista de tiles do levelDescription
            foreach (XmlNode node in nodes[0].ChildNodes)
            {
                if (node.NodeType == XmlNodeType.Comment) continue;
                description.Add(new Tile(declarations.getTile(node.Attributes["TileType"].Value)));
            }
            #endregion

            return description;
        }
    }
}
