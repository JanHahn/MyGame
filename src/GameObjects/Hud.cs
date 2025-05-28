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
    const int healthRectangleMaxLength = 252;
    DynamicSpriteFont font;


    public Texture2D healthBar { get; set; }
    public Vector2 HealthBarPosition { get; set; }
    private int healthPoints;
    public int HealthPoints
    {
        get { return healthPoints; }
        set { healthPoints = value; }
    }

    public Hud(int healthStartStatus, GraphicsDevice gd)
    {
        this.gd_ = gd;
        this.healthPoints = healthStartStatus;
        HealthBarPosition = new Vector2(40, 40);
        string contentRoot = AppContext.BaseDirectory;
        string healthBarPath = Path.Combine(contentRoot, "Content/HUD/HealthBar.png");
        healthBar = Texture2D.FromFile(gd, healthBarPath);

        // CODE BELOW IS TEMPORERLY FOR TESTS //
        var system = new FontSystem();
        system.AddFont(File.ReadAllBytes("Content/arial.ttf"));
        font = system.GetFont(24); // Rozmiar czcionki
    }


    public void Draw(SpriteBatch spriteBatch)
    {
        //Texture2D healthBarRectangleContent = new Texture2D(gd_, 10, 10);
        Texture2D pixel = new Texture2D(gd_, 1, 1);
        pixel.SetData(new[] { Color.White });
        Rectangle healthBarRectangleContent = new Rectangle((int)HealthBarPosition.X + 40, (int)HealthBarPosition.Y + 10, healthRectangleMaxLength - LackOfHealth, 40); // x, y, szerokość, wysokoś


        spriteBatch.Draw(pixel, healthBarRectangleContent, Color.Red);
        spriteBatch.Draw(healthBar, HealthBarPosition, Color.White);
        spriteBatch.DrawString(font, "Health!", new Vector2(100, 100), Color.Black);
        spriteBatch.DrawString(font, healthPercentageString, new Vector2(200, 60), Color.Black);
    }



    int interval = 100;
    int timeStorage = 0;
    int LackOfHealth = 0;
    string healthPercentageString;
    public void TestUpdate(GameTime gameTime)
    {
        int deltaTime = gameTime.ElapsedGameTime.Milliseconds;
        timeStorage += deltaTime;
        if (timeStorage > interval)
        {
            LackOfHealth++;
            int healthPercentage = (int)((healthRectangleMaxLength - LackOfHealth) / (double)healthRectangleMaxLength * 100);
            healthPercentageString = healthPercentage.ToString() + "%";
            if (LackOfHealth > healthRectangleMaxLength)
            {
                LackOfHealth = 0;
            }
            timeStorage = timeStorage - interval;
        }
    }

}