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

public class Hud
{
    GraphicsDevice gd_;

    HealthBar healthBar_;
    public HealthBar GetHealthBar
    {
        get { return healthBar_; }
    } 

    public CoinStatus CoinStatus { get; set; }

    private Inventory inventory_;


    public Hud(GraphicsDevice gd, GameServices gameServices)
    {
        this.gd_ = gd;
        this.inventory_ = new Inventory();
        this.healthBar_ = new HealthBar(100, gd);
        this.CoinStatus = new CoinStatus(gd, gameServices);
    }


    public void Draw(SpriteBatch spriteBatch)
    {
        healthBar_.Draw(spriteBatch);
        inventory_.Draw(spriteBatch);
        CoinStatus.Draw(spriteBatch);
    }


}