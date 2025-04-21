using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



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

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.IsFullScreen = true;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        heroGenerator = new HeroGenerator();
        heroGenerator.GraphicsDevice_ = GraphicsDevice;
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _myHero = heroGenerator.generateHero();
        backgroundTexture = Texture2D.FromFile(GraphicsDevice, "/home/jh/my_shit/game_csharp/game/MyGame/Content/BackGrounds/DayBasic.png");

        // Color[] obstacleData = new Color[50 * 200];
        // for (int i = 0; i < obstacleData.Length; i++) obstacleData[i] = Color.Black;
        // _obstacleTexture.SetData(obstacleData);
    }

    protected override void Update(GameTime gameTime)
    {
        _myHero.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.Draw(backgroundTexture, backGroundPosition, Color.White);
        _myHero.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
