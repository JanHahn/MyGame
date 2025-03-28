using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MyGame;

public class Game1 : Game
{
    const int SQUERE_SIDE = 50;
    const int BOTTOM_LEVEL = 1080 - SQUERE_SIDE;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _playerTexture;
    private Texture2D _obstacleTexture;
    private Texture2D backgroundTexture;
    private Texture2D mojaTekstura;
    private Vector2 _playerPosition = new Vector2(500, BOTTOM_LEVEL);
    private Vector2 _obstaclePosition = new Vector2(800, BOTTOM_LEVEL-150);
    private Vector2 _kalmarPositon = new Vector2(100, 100);
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
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _playerTexture = new Texture2D(GraphicsDevice, SQUERE_SIDE, SQUERE_SIDE);
        _obstacleTexture = new Texture2D(GraphicsDevice, 50, 200);
        mojaTekstura = Texture2D.FromFile(GraphicsDevice, "/home/jh/my_shit/game_csharp/game/MyGame/Content/batman.png");

        
        backgroundTexture = Texture2D.FromFile(GraphicsDevice, "/home/jh/my_shit/game_csharp/game/MyGame/Content/Village_background.jpg");

        


        Color[] data = new Color[50 * 50];
        for (int i = 0; i < data.Length; i++) data[i] = Color.Red;
        _playerTexture.SetData(data);

        Color[] obstacleData = new Color[50 * 200];
        for (int i = 0; i < obstacleData.Length; i++) obstacleData[i] = Color.Black;
        _obstacleTexture.SetData(obstacleData);
    }

    float acceleration = 5000;
    float speed = 0f; // Piksele na sekundę
    float kalmar_speed = 0f;
    bool flying = false;
    bool kalmar_flying = false;
    protected override void Update(GameTime gameTime)
    {
        
        if (flying == true){
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            speed -= deltaTime * acceleration;
            _playerPosition.Y -= deltaTime * speed;
            if (_playerPosition.Y >= BOTTOM_LEVEL){
                _playerPosition.Y = BOTTOM_LEVEL;
                speed = 0;
            }
        }

        if (kalmar_flying == true){
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            kalmar_speed -= deltaTime * acceleration;
            _kalmarPositon.Y -= deltaTime * kalmar_speed;
            if (_kalmarPositon.Y >= BOTTOM_LEVEL-50){
                _kalmarPositon.Y = BOTTOM_LEVEL-50;
                kalmar_speed = 0;
            }
        }



        var keyboard = Keyboard.GetState();
        if (keyboard.IsKeyDown(Keys.Right)) _playerPosition.X += 10;
        if (keyboard.IsKeyDown(Keys.Left)) _playerPosition.X -= 10;
        if (keyboard.IsKeyDown(Keys.Up) && speed == 0){
            flying = true; 
            speed = 2000;
        } 
 
        if (keyboard.IsKeyDown(Keys.D)) _kalmarPositon.X += 10;
        if (keyboard.IsKeyDown(Keys.A)) _kalmarPositon.X -= 10;
        if (keyboard.IsKeyDown(Keys.W) && kalmar_speed == 0){
            kalmar_flying = true; 
            kalmar_speed = 2000;
        } 
        //if (keyboard.IsKeyDown(Keys.A)){acceleration += 100; Console.WriteLine(acceleration);}
        //if (keyboard.IsKeyDown(Keys.D)){acceleration -= 100; Console.WriteLine(acceleration);}

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.Draw(backgroundTexture, backGroundPosition, Color.White);
        _spriteBatch.Draw(_playerTexture, _playerPosition, Color.White);
        _spriteBatch.Draw(_obstacleTexture, _obstaclePosition, Color.Black);
        _spriteBatch.Draw(mojaTekstura, _kalmarPositon, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
