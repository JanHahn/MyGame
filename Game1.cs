using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Collections.Generic;



namespace MyGame;

public class Game1 : Game
{
    const int BOTTOM_LEVEL = 1080 - 400;

    //ActionsPaths samuraii();

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private HeroGenerator heroGenerator;
    private Hero _myHero;
    private Texture2D backgroundTexture;
    //private Texture2D mojaTekstura;
    private Vector2 _obstaclePosition = new Vector2(800, BOTTOM_LEVEL-150);
    private Vector2 backGroundPosition = new Vector2(0,0);
    private Vector2 backgroundScale;
    private BackGroundSlider backGroundSlider;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.IsFullScreen = false;
        _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        _graphics.ApplyChanges();

        Content.RootDirectory = "Content";
        IsMouseVisible = true;

    }

    Texture2D backgroundTexture2;
    Texture2D backgroundTexture3;
    Texture2D backgroundTexture4;

    protected override void LoadContent()
    {
        heroGenerator = new HeroGenerator();
        heroGenerator.GraphicsDevice_ = GraphicsDevice;
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _myHero = heroGenerator.generateHero();
        string contentRoot = AppContext.BaseDirectory;
        string backgroundPath1 = Path.Combine(contentRoot, "Content/BackGrounds/animated/1.png");
        string backgroundPath2 = Path.Combine(contentRoot, "Content/BackGrounds/animated/2.png");
        string backgroundPath3 = Path.Combine(contentRoot, "Content/BackGrounds/animated/3.png");
        string backgroundPath4 = Path.Combine(contentRoot, "Content/BackGrounds/animated/4.png");
        backgroundTexture = Texture2D.FromFile(GraphicsDevice, backgroundPath1);
        backgroundTexture2 = Texture2D.FromFile(GraphicsDevice, backgroundPath2);
        backgroundTexture3 = Texture2D.FromFile(GraphicsDevice, backgroundPath3);
        backgroundTexture4 = Texture2D.FromFile(GraphicsDevice, backgroundPath4);

        List<Texture2D> backGroundLayers = new List<Texture2D>();
        backGroundLayers.Add(backgroundTexture);
        backGroundLayers.Add(backgroundTexture2);
        backGroundLayers.Add(backgroundTexture3);
        backGroundLayers.Add(backgroundTexture4);

        List<float> speedFactors = new List<float>();
        speedFactors.Add(1);
        speedFactors.Add(0.05f);
        speedFactors.Add(0.12f);
        speedFactors.Add(0.5f);

        backgroundScale = new Vector2(
        (float)_graphics.PreferredBackBufferWidth / backgroundTexture.Width,
        (float)_graphics.PreferredBackBufferHeight / backgroundTexture.Height);

        backGroundSlider = new BackGroundSlider(backGroundLayers, speedFactors, _myHero, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight, backgroundScale);

        // Color[] obstacleData = new Color[50 * 200];
        // for (int i = 0; i < obstacleData.Length; i++) obstacleData[i] = Color.Black;
        // _obstacleTexture.SetData(obstacleData);
    }

    protected override void Update(GameTime gameTime)
    {
        _myHero.Update(gameTime);
        backGroundSlider.Update(gameTime);
        base.Update(gameTime);
    }



    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.Draw(backgroundTexture, backGroundPosition, null,  Color.White, 0f, Vector2.Zero, backgroundScale, SpriteEffects.None, 0f);
        _spriteBatch.Draw(backgroundTexture2, backGroundPosition, null,  Color.White, 0f, Vector2.Zero, backgroundScale, SpriteEffects.None, 0f);
        _spriteBatch.Draw(backgroundTexture3, backGroundPosition, null,  Color.White, 0f, Vector2.Zero, backgroundScale, SpriteEffects.None, 0f);
        //_spriteBatch.Draw(backgroundTexture4, backGroundPosition, null,  Color.White, 0f, Vector2.Zero, backgroundScale, SpriteEffects.None, 0f);
        backGroundSlider.Draw(_spriteBatch);
        _myHero.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
