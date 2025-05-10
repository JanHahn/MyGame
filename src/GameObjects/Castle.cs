using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Globalization;



namespace MyGame;

    public class Castle
    {
        public Vector2 Position { get; set; }

        private Texture2D tileStruct;
        public Texture2D TileStruct { 
            set { tileStruct = value; }
        }
        private List<Layer> layers_;


        //referencje do dodania in game world
        private CollisionManager collisionManager_;
        private List<IPrintable> printableAll_;


        public Castle(CollisionManager collisionManager, List<IPrintable> printableAll, GraphicsDevice graphicsDevice){
            this.collisionManager_ = collisionManager;
            this.printableAll_ = printableAll;

            TileStruct = Texture2D.FromFile(graphicsDevice, "C:\\Users\\janek\\my_shit\\firstGame\\MyGame\\Content\\GameWorld\\CastleTiles.png");

            string json = File.ReadAllText("C:\\Users\\janek\\my_shit\\firstGame\\MyGame\\Content\\GameWorld\\Castle2.json");
            Console.WriteLine(json);
            JObject root = JObject.Parse(json); 

            JArray layersArray = (JArray)root["layers"];

            // Deserializujemy warstwy kafelków
            layers_ = layersArray.ToObject<List<Layer>>();
            foreach (Layer layer in layers_) {
                int localTail = 0;
                foreach (int tailNumber2 in layer.Data) {
                    if (tailNumber2 == 0) {
                        localTail++;
                        continue;
                    }
                    int tailNumber = tailNumber2 - 1;
                    Rectangle srcRect = new Rectangle(tailNumber % 4 * 48 , tailNumber / 4 * 48, 48, 48); 
                    Tile newTile = new Tile(new Vector2(localTail % layer.Columns * 48, localTail / layer.Columns * 48 + 500), ExtractSprite(tileStruct, srcRect, graphicsDevice));
                    if (layer.IsCollidable == true){   
                        collisionManager.Add(newTile);
                    }
                    // dodaj do printable all
                    printableAll_.Add(newTile);
                    Console.WriteLine(tailNumber); 
                    localTail++;
                }
            }
        }


        Texture2D ExtractSprite(Texture2D source, Rectangle sourceRect, GraphicsDevice graphicsDevice)
        {
            Color[] data = new Color[sourceRect.Width * sourceRect.Height];
            source.GetData(0, sourceRect, data, 0, data.Length);

            Texture2D newTexture = new Texture2D(graphicsDevice, sourceRect.Width, sourceRect.Height);
            newTexture.SetData(data);

            return newTexture;
        }
        
    }