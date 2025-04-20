using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MyGame;

public struct ActionsPaths {
    string Idle; int Idle_frames;
    string Run; int Run_frames;
    string Jump; int Jump_frames;
    string Walk; int Walk_frames;
}

public class Hero
{
    private Vector2 position_;
    private Texture2D texture_;
    private ActionsPaths actions_;
    public Texture2D Texture { 
        get { return texture_; }
        set { texture_ = value; } 
    }
    public ActionsPaths Actions  { 
        get { return actions_; }
        set { actions_ = value; } 
    }
    public Vector2 Position { 
        get { return position_; }
        set { position_ = value; }
    }

    bool flying = false;
    float acceleration = 5000;
    float speed = 0f; // Piksele na sekundę

    
    int frameWidth;         // Szerokość jednej klatki
    int frameHeight;        // Wysokość jednej klatki
    int currentFrame = 0;   // Aktualna klatka
    float timer = 0f;       // Licznik czasu
    float interval = 0.1f;  // Ile sekund pokazujemy jedną klatkę
    int totalFrames = 8;    // Ile wszystkich klatek masz w pliku
    

    const float BOTTOM_LEVEL = 780f; // <- dodajemy tę stałą

    public Hero(Texture2D texture, Vector2 position)
    {
        texture_ = texture;
        position_ = position;
        frameWidth = texture_.Width/totalFrames;
        frameHeight = texture_.Height;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Rectangle sourceRectangle = new Rectangle(frameWidth * currentFrame, 0, frameWidth, frameHeight);
        spriteBatch.Draw(Texture, Position, sourceRectangle, Color.White);
    }

    public void Update(GameTime gameTime){

        timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (timer >= interval)
        {
            currentFrame = (currentFrame + 1) % totalFrames;
            timer = 0f;
        }

        var keyboard = Keyboard.GetState();
        if (keyboard.IsKeyDown(Keys.Right)) position_.X += 10;
        if (keyboard.IsKeyDown(Keys.Left)) position_.X -= 10;
        if (keyboard.IsKeyDown(Keys.Up) && speed == 0){
            flying = true; 
            speed = 2000;
        }
        else {
            
        }
 

        if (flying){
            
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            speed -= deltaTime * acceleration;

            position_.Y -= deltaTime * speed;

            if (position_.Y >= BOTTOM_LEVEL)
            {
                position_.Y = BOTTOM_LEVEL;
                speed = 0;
            }
            }
        }

        
}