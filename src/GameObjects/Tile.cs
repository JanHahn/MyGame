using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Newtonsoft.Json;


namespace MyGame;

    public class Tile: IPrintable, ICollidable
    {
        public Vector2 Position {get; set;} 
        public Texture2D TileTexture { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Tile(Vector2 position, Texture2D tileTexture) {
            Position = position;
            TileTexture = tileTexture;
            Width = tileTexture.Width;
            Height = tileTexture.Height;
        }

        public void Draw(SpriteBatch spriteBatch){
            spriteBatch.Draw(TileTexture, Position, Color.White);
        }
    }