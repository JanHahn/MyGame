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
using System.Net.Mime;


namespace MyGame;

public class CoinStatus
{

    GraphicsDevice gd_;
    DynamicSpriteFont font;


    public Texture2D coinIcon { get; set; }
    public Vector2 coinIconPosition { get; set; }

    private GameServices GameServices{ get; set; }


    public CoinStatus(GraphicsDevice gd, GameServices gameServices)
    {
        this.gd_ = gd;
        this.GameServices = gameServices;

        coinIconPosition = new Vector2(1550, 40);
        coinIcon = TextureManager.Get("CoinIcon");

        // CODE BELOW IS TEMPORERLY FOR TESTS //
        var system = new FontSystem();
        system.AddFont(File.ReadAllBytes("Content/arial.ttf"));
        font = system.GetFont(24); // Rozmiar czcionki

    }


    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(coinIcon, coinIconPosition, null, Color.White, 0f, Vector2.Zero, 0.15f, SpriteEffects.None, 0f);
        spriteBatch.DrawString(font, "Money:", new Vector2(1400, 60), Color.Black);
        spriteBatch.DrawString(font, GameServices.CoinManager.AmountOfMoney.ToString(), new Vector2(1480, 60), Color.Black);
    }


} 