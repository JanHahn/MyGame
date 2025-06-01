using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Runtime.CompilerServices;
using System.Diagnostics.Contracts;
using FontStashSharp;


namespace MyGame;

public class Inventory
{
    DynamicSpriteFont font;


    public Texture2D InventorySlots { get; set; }
    public Vector2 InventoryPosition { get; set; }
    public Inventory()
    {
        InventoryPosition = new Vector2(600, 985);
        InventorySlots = TextureManager.Get("InventoryBar");

        // CODE BELOW IS TEMPORERLY FOR TESTS //
        var system = new FontSystem();
        system.AddFont(File.ReadAllBytes("Content/arial.ttf"));
        font = system.GetFont(24); // Rozmiar czcionki
    }


    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(InventorySlots, InventoryPosition, null, Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
    }


}