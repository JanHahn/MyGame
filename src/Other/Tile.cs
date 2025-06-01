using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Newtonsoft.Json;


namespace MyGame;

public class Tile : IPrintable, ICollidable
{
    public Vector2 Position { get; set; }
    public string TileAtlas { get; private set; }
    public int TileNumber { get; private set; }
    public int Width { get; } = 48;
    public int Height { get; } = 48;

    private Texture2D tileAtlasTexture_;
    private Rectangle sourceRect_;

    public Tile(Vector2 position, string tileAtlas, int tileNumber)
    {
        Position = position;
        TileAtlas = tileAtlas;
        TileNumber = tileNumber;

        // Pobranie tekstury atlasu
        tileAtlasTexture_ = TextureManager.Get(TileAtlas);

        int columns = tileAtlasTexture_.Width / Width;

        sourceRect_ = new Rectangle(
            (tileNumber % columns) * Width,
            (tileNumber / columns) * Height,
            Width,
            Height
        );
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(tileAtlasTexture_, Position, sourceRect_, Color.White);
    }
}